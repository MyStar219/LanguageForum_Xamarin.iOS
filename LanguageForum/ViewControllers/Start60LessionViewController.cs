using System;
using System.Timers;
using Foundation;
using UIKit;

using ZXing.Mobile;

using System.Collections.Generic;
using LanguageForum.Model;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using LanguageForum.Classes;

using LanguageForum.Global;

#if __UNIFIED__

using CoreGraphics;
#else
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using CGRect = System.Drawing.RectangleF;
#endif


namespace LanguageForum.ViewControllers
{
    public partial class Start60LessionViewController : UIViewController
	{


		public int currentProgress;

		private float scroll_amount = 0.0f;    // amount to scroll 
		private float bottom = 0.0f;           // bottom point
		private float offset = 10.0f;          // extra offset
		private bool moveViewUp = false;           // which direction are we moving

		private SQLDatabase database;
		private Lesson actualLesson;
		private LessonType actualLessonType;
        private MobileBarcodeScanner scanner;

        float Accuracy = 1;


		private ObservableCollection<QRCodeItem> students = new ObservableCollection<QRCodeItem>();
        List<String> studentNames = new List<string>
        {
        };

		private int lessonType;

		Timer timer = new System.Timers.Timer();

		protected Start60LessionViewController(IntPtr handle) : base(handle)
		{

		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

            currentProgress = SingleData.getInstance().startProgress;



			lessonTField.Layer.CornerRadius = 2;
			lessonTField.Layer.BorderWidth = 2;
			lessonTField.Layer.BorderColor = UIColor.DarkGray.CGColor;

			progressSlider.MultipleTouchEnabled = false;

			progressSlider.SetThumbImage(UIImage.FromFile("slider.png"), UIControlState.Normal);

            if (SingleData.getInstance().lessionCheck == "60min")
			{
                progressSlider.Value = currentProgress;
				progressSlider.MaxValue = 3600;
				lessonType = 1;

			}
            else if (SingleData.getInstance().lessionCheck == "90min")
			{

                progressSlider.Value = currentProgress;
				progressSlider.MaxValue = 5400;

				lessonType = 2;
			}


			timer.Interval = 1000;
			timer.Enabled = true;
			timer.Elapsed += Timer_Elapsed;
			timer.Start();

			InitDatabase();
			InitLesson((LessonType)lessonType);

			// Keyboard popup
			NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.DidShowNotification, KeyBoardUpNotification);

			// Keyboard Down
			NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillHideNotification, KeyBoardDownNotification);

			lessonTField.ShouldReturn = (textField) => {
				textField.ResignFirstResponder();
				return true;
			};

			var g = new UITapGestureRecognizer(() => View.EndEditing(true));
			g.CancelsTouchesInView = false;
			View.AddGestureRecognizer(g);

			cancelLessonBtn.TouchUpInside += (sender, ea) =>
			{
                SingleData.getInstance().currentLesson = "";
                SingleData.getInstance().startProgress = 0;
				EndLesson(actualLesson.Id, true);
			};

			scanCodeBtn.TouchUpInside += (sender, ea) =>
			{

				if (scanner == null)
                    scanner = new MobileBarcodeScanner();

				scanner.AutoFocus();
				scanner.TopText = "You can scan teacher or student QR code.";
				scanner.BottomText = "Scan QRCode.";

                var opt = new MobileBarcodeScanningOptions();
                opt.DelayBetweenContinuousScans = 3000;

                //Start scanning
                scanner.ScanContinuously(opt, true, HandleScanResult);

			};



			backBtn.TouchUpInside += (sender, ea) =>
			{
				if (lessonLbl.Text == "60 minutes lesson")
				{
                    SingleData.getInstance().currentLesson = "60min";
				}
				else if (lessonLbl.Text == "90 minutes lesson")
				{
                    SingleData.getInstance().currentLesson = "90min";
				}
                SingleData.getInstance().startProgress = currentProgress;

				MainViewController newViewController = (MainViewController)Storyboard.InstantiateViewController("MainViewController");
				this.NavigationController.PushViewController(newViewController, true);
			};

			// Perform any additional setup after loading the view, typically from a nib.
		}


		void Timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			InvokeOnMainThread(() => {
                currentProgress = currentProgress + 1;
                progressSlider.Value = currentProgress;
				if (lessonLbl.Text == "60 minutes lesson")
				{
                    if (currentProgress > 3600)
					{

						timer.Stop();
                        SingleData.getInstance().currentLesson = "";
                        SingleData.getInstance().startProgress = 0;
                        EndLesson(actualLesson.Id);

					}
				}
				else if (lessonLbl.Text == "90 minutes lesson")
				{
                    if (currentProgress > 5400)
					{

						timer.Stop();
                        SingleData.getInstance().currentLesson = "";
                        SingleData.getInstance().startProgress = 0;
						EndLesson(actualLesson.Id);
					}
				}
			});
		}

		// Process the scan code

        private async void HandleScanResult(ZXing.Result code)

		{
			if (code != null)
			{
				var identification = code.Text.Substring(0, 1).ToUpper();
				var type = identification == "T" ? QRCodeUserType.Teacher : identification == "S" ? QRCodeUserType.Student : QRCodeUserType.Unnknown;

                LocationManager locationManger = new LocationManager();

				switch (type)
				{
					case QRCodeUserType.Student:
						InvokeOnMainThread(() =>
						{

							if (students.FirstOrDefault(sq => sq.Code.ToLower() == code.Text.Substring(1).ToLower()) == null)
							{
								var student = new QRCodeItem()
								{
									Type = QRCodeUserType.Student,
									Code = code.Text.Substring(1),
									Lesson = actualLesson.Id,
									PersonInfo = code.Text
								};

                                if (SingleData.getInstance().currentLng.ToString() != null && SingleData.getInstance().currentLat.ToString() != null)
								{
                                    student.LocationDescription = SingleData.getInstance().locationDesc;
								    student.Accuracy = Accuracy;
                                    student.Altitude = null;
                                    student.Bearing = null;
                                    student.ElapsedRealtimeNanos = null;
                                    student.Latitude = SingleData.getInstance().currentLat;
                                    student.Longitude = SingleData.getInstance().currentLng;
                                    student.Provider = null;
                                    student.Speed = null;
                                    student.Time = null;
								}

								database.Insert(student);

								if (!students.Any(s => s.Code == student.Code))
								{
									students.Add(student);
                                }

								//player.Start();
							}
						});
						break;

					case QRCodeUserType.Teacher:
						var teacher = new QRCodeItem()
						{
							Type = QRCodeUserType.Teacher,
							Code = code.Text.Substring(1),
							Lesson = actualLesson.Id,
							PersonInfo = code.Text
						};

                        if (SingleData.getInstance().currentLng.ToString() != null && SingleData.getInstance().currentLat.ToString() != null)
						{
                            teacher.LocationDescription = SingleData.getInstance().locationDesc;
						    teacher.Accuracy = Accuracy;
                            teacher.Altitude = null;
                            teacher.Bearing = null;
                            teacher.ElapsedRealtimeNanos = null;
                            teacher.Latitude = SingleData.getInstance().currentLat;
                            teacher.Longitude = SingleData.getInstance().currentLng;
                            teacher.Provider = null;
                            teacher.Speed = null;
                            teacher.Time = null;
						}

						database.Insert(teacher);
						//player.Start();

						InvokeOnMainThread(() =>
						{
							teacherNameLbl.Text = "Teacher: " + teacher.PersonInfo;
						});
						break;
				}

				LoadQRWithoutPersonInfo();

				InvokeOnMainThread(() => UpdateStudentsCount());
                //scanner.Cancel();
            } else {
                Console.WriteLine("Barcode Null");
            }
		}

		private void UpdateStudentsCount()
		{
			studentNumberLbl.Text = "Student" + (students.Count > 1 ? "s" : "") + " (" + students.Count + ")";
		}

		private async void LoadQRWithoutPersonInfo()
		{
			await UpdateStudentAndTeacherPersonInfo();
			//await UpdateTeacherPersonInfo();
			//await UpdateStudentsPersonInfo();

			InvokeOnMainThread(() =>
			{
				LoadDataFromActualLesson();
                RefreshStudentList();
			});
		}

		private async Task UpdateStudentAndTeacherPersonInfo()
		{
			var list = database.GetQRWithoutPersonInfo(actualLesson.Id);
			foreach (var qr in list)
			{
				UpdatePersonInfo(qr);
				database.Update(qr);
			}

		}

		private async Task UpdateTeacherPersonInfo()
		{
			var teacher = database.GetTeacherWithoutPersonInfo(actualLesson.Id);
			if (teacher != null)
			{
				UpdatePersonInfo(teacher);
				InvokeOnMainThread(() =>
				{
					teacherNameLbl.Text = "Teacher: " + teacher.PersonInfo;
				});
			}
		}

		private async Task UpdateStudentsPersonInfo()
		{
			var list = students.Where(p => !p.PersonInfoUpdated && p.Type == QRCodeUserType.Student).ToList();
			foreach (var qr in list)
			{
				UpdatePersonInfo(qr);
			}

			InvokeOnMainThread(() => RefreshStudentList());
		}

		protected void LoadDataFromActualLesson()
		{
			var teacher = database.GetTeacher(actualLesson.Id);
			if (teacher != null)
			{
				teacherNameLbl.Text = "Teacher: " + teacher.PersonInfo;
			}
			else
			{

				teacherNameLbl.Text = "Teacher";
			}

			students.Clear();
			foreach (var student in database.GetStudents(actualLesson.Id).ToList())
			{
				students.Add(student);
                studentNames.Add(student.PersonInfo);

            }

            lessonTField.Text = actualLesson.Description;
            UpdateStudentsCount();


            var studentsString = new String[studentNames.Count];
            for (int i = 0; i < studentNames.Count; i++) {
                studentsString[i] = studentNames[i];
            }

            if (studentNames.Count != 0 && teacher != null) {
                RegisterLesson(teacher.Latitude, teacher.Longitude, teacher.Accuracy, 0, teacher.LocationDescription, teacher.PersonInfo, studentsString, DateTime.Now, lessonTField.Text, (int)progressSlider.MaxValue, "", false);
            }

		}

		private void UpdatePersonInfo(QRCodeItem qr)
		{
			try
			{
				qr.PersonInfo = ManagementService.AndroidService.GetPersonDetail(qr.PersonInfo ?? "");
				qr.PersonInfoUpdated = true;

				//database.Update(qr);
			}
			catch (Exception ex)
			{

				UIAlertView alert = new UIAlertView()
				{
					Title = "Error",
					Message = ex.ToString()
				};
				alert.AddButton("OK");

				InvokeOnMainThread(() => alert.Show());
			}
		}

		private void RefreshStudentList()
		{
            if (studentNames.Count != 0) {


                for (int i = studentNames.Count - 1; i > -1; i --)
                {

                    string temp = studentNames[i];
                    for (int j = 0; j < i; j ++) {

                        string temp1 = studentNames[j];
                        if(temp.Equals(temp1)) {
                            studentNames.RemoveAt(i);

                            break;
                        }

                    }
                }

                studentTableView.Source = new StudentsTableViewSource(studentNames);
            }
			
        }

        public bool RegisterLesson(double? latitude, double? longitude, float? locationAccuracy, long? locationAge, string locationDescription, string teacher, string[] students, DateTime? lessonStart, string lessonDescription, decimal? lessonDuration, string description = "", bool canceled = false)
            {
                var rtn = false;

            try
            {
                var result = ManagementService.AndroidService.RegisterLesson(teacher, latitude, longitude, students, locationDescription ?? "", lessonStart, description ?? "", lessonDuration, canceled);
                if (result == "OK")
                    rtn = true;
            }
            catch (Exception) { }

            return rtn;
        }

        private void RegisterCompletedLessons()
        {
            var list = database.GetReadyToSendLessons();
            foreach (var lesson in list)
            {
                var lessonScanned = database.GetLessonScanInfo(lesson.Id);
                var lessonTeacher = database.GetTeacher(lesson.Id);
                var lessonStudents = database.GetStudents(lesson.Id);

                // Kontrola, zda ma lekce narok na registraci (ucitel musi byt zadan)
                if (lessonTeacher != null)
                {
                    var sendResult = RegisterLesson(lessonTeacher?.Latitude,
                                                    lessonTeacher?.Longitude,
                                                    lessonTeacher?.Accuracy,
                                                    lessonTeacher?.ElapsedRealtimeNanos,
                                                    lessonTeacher?.LocationDescription,
                                                    lessonTeacher?.Code,
                                                    lessonStudents?.Select(t => t.Code).ToArray(),
                                                    lesson.Created,
                                                    Enum.GetName(typeof(LessonType), lesson.LessonType),
                                                    (int)lesson.LessonType,
                                                    lesson.Description,
                                                    lesson.Canceled
                                                    );

                    // Odeslana ma byt, ale odpoved serveru neni OK
                    if (!sendResult) continue;

                }

                lesson.Sended = true;
                database.Update(lesson);
            }

            // Neni potreba promazavat, pokud je seznam ke zpracovani prazdny
            if (list.Count() > 0)
                database.DeleteSendedLessons();
        }

		private void InitDatabase()
		{
			database = new SQLDatabase();
		}

		private void InitLesson(LessonType lessonType)
		{
			actualLesson = database.GetLastLesson();
			if (actualLesson == null)
			{
				// zalozime novou
				var lesson = new Lesson()
				{
					LessonType = lessonType,
					Created = DateTime.Now
				};

				database.Insert(lesson);

				actualLesson = lesson;
			}
			else if (actualLesson.LessonType != lessonType)
			{
				//EndLesson(actualLesson.Id);

				// zalozime novou
				var lesson = new Lesson()
				{
					LessonType = lessonType,
					Created = DateTime.Now
				};

				database.Insert(lesson);

				actualLesson = lesson;
			}

			actualLessonType = lessonType;

			lessonLbl.Text = actualLessonType == LessonType.Lesson60minutes ? "60 minutes lesson" : actualLessonType == LessonType.Lesson90minutes ? "90 minutes lesson" : "";

			LoadQRWithoutPersonInfo();
		}


		private void EndLesson(Guid lessonId, bool canceled = false)
		{
			var lesson = database.GetLesson(lessonId);
			lesson.Canceled = canceled;
			lesson.Closed = DateTime.Now;
			database.Update(lesson);
			this.NavigationController.PopViewController(true);
            RegisterCompletedLessons();
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}



		private void KeyBoardUpNotification(NSNotification notification)
		{
			// get the keyboard size
			CoreGraphics.CGRect r = UIKeyboard.BoundsFromNotification(notification);

			// Find what opened the keyboard
			foreach (UIView view in this.View.Subviews)
			{
				if (view.IsFirstResponder)
					activeView = view;
			}

			if (activeView != null)
			{
				// Bottom of the controller = initial position + height + offset      
				bottom = ((float)(activeView.Frame.Y + activeView.Frame.Height + offset));

				// Calculate how far we need to scroll
				scroll_amount = ((float)(r.Height - (View.Frame.Size.Height - bottom)));

				// Perform the scrolling
				if (scroll_amount > 0)
				{
					moveViewUp = true;
					ScrollTheView(moveViewUp);
				}
				else
				{
					moveViewUp = false;
				}
			}

		}

		private void KeyBoardDownNotification(NSNotification notification)
		{
			if (moveViewUp) { ScrollTheView(false); }
		}

		private void ScrollTheView(bool move)
		{

			// scroll the view up or down
			UIView.BeginAnimations(string.Empty, System.IntPtr.Zero);
			UIView.SetAnimationDuration(0.1);

			CoreGraphics.CGRect frame = View.Frame;

			if (move)
			{
				frame.Y -= scroll_amount;
			}
			else
			{
				frame.Y += scroll_amount;
				scroll_amount = 0;
			}

			View.Frame = frame;

			UIView.CommitAnimations();
		}
	}
}

