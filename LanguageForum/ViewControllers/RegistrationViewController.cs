using System;
using UIKit;
using Foundation;
using LanguageForum.Classes;
using LanguageForum.Model;

using System.Linq;



namespace LanguageForum.ViewControllers
{


	public partial class RegistrationViewController : UIViewController
	{
		UIImagePickerController imagePicker = new UIImagePickerController();
		UIImagePickerController cameraPicker = new UIImagePickerController();
		public string languageCheck;

		private SQLDatabase database;

		protected RegistrationViewController(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad()
		{

			InitDatabase();

			datePicker.ValueChanged += (object sender, EventArgs e) =>
			{
				NSDateFormatter dateFormat = new NSDateFormatter();
				dateFormat.DateFormat = "MMM dd, yyyy";
				birthDateLbl.Text = dateFormat.ToString(datePicker.Date);
			};

			base.ViewDidLoad();

			// UI Control

			profileImg.Layer.CornerRadius = 1;
			profileImg.Layer.BorderWidth = 1;
			profileImg.Layer.BorderColor = UIColor.LightGray.CGColor;
			selectPhotoBtn.Layer.CornerRadius = 2;
			cameraPhotoBtn.Layer.CornerRadius = 2;
			firstNameTField.Layer.CornerRadius = 2;
			lastNameTField.Layer.CornerRadius = 2;
			streetTField.Layer.CornerRadius = 2;
			cityTField.Layer.CornerRadius = 2;
			zipcodeTField.Layer.CornerRadius = 2;
			telephoneTField.Layer.CornerRadius = 2;
			emailTField.Layer.CornerRadius = 2;
			tinTField.Layer.CornerRadius = 2;
			birthdateBtn.Layer.CornerRadius = 2;
			languageBtn.Layer.CornerRadius = 2;
			learningLanguageBtn.Layer.CornerRadius = 2;
			dateSelectView.Hidden = true;
			languageSelectView.Hidden = true;
			languageCheck = "";

			NSNotificationCenter.DefaultCenter.AddObserver(UITextField.TextDidBeginEditingNotification, TextChangedEvent);

			// Add the button Click events

			selectPhotoBtn.TouchUpInside += (sender, ea) =>
			{
				imagePicker.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;
				imagePicker.MediaTypes = UIImagePickerController.AvailableMediaTypes(UIImagePickerControllerSourceType.PhotoLibrary);

				imagePicker.FinishedPickingMedia += Handle_FinishedPickingMedia;
				imagePicker.Canceled += Handle_Canceled;

				NavigationController.PresentModalViewController(imagePicker, true);

			};

			cameraPhotoBtn.TouchUpInside += (sender, ea) =>
			{

				imagePicker.SourceType = UIImagePickerControllerSourceType.Camera;
				imagePicker.MediaTypes = UIImagePickerController.AvailableMediaTypes(UIImagePickerControllerSourceType.Camera);

				imagePicker.FinishedPickingMedia += Handle_FinishedPickingMedia;
				imagePicker.Canceled += Handle_Canceled;

				NavigationController.PresentModalViewController(imagePicker, true);

			};

			// Set the birth date

			birthdateBtn.TouchUpInside += (sender, ea) =>
			{
                hideKeyboard();
				dateSelectView.Hidden = false;
				dateSelectView.BackgroundColor = UIColor.LightGray.ColorWithAlpha((System.nfloat)0.5);
				datePicker.Layer.BorderWidth = 1;
				datePicker.Layer.BorderColor = UIColor.DarkGray.CGColor;
				NSDateFormatter dateFormat = new NSDateFormatter();
				dateFormat.DateFormat = "MMM dd, yyyy";
				birthDateLbl.Text = dateFormat.ToString(datePicker.Date);

			};

			setBtn.TouchUpInside += (sender, ea) =>
			{
				dateSelectView.Hidden = true;
				NSDateFormatter dateFormat = new NSDateFormatter();
				dateFormat.DateFormat = "yyyy-MMM-dd";
				birthdateBtn.SetTitle(dateFormat.ToString(datePicker.Date), UIControlState.Normal);
			};

			cancelBtn.TouchUpInside += (sender, ea) =>
			{
				dateSelectView.Hidden = true;
			};


			// Set the learning language and language

			languageBtn.TouchUpInside += (sender, ea) =>
			{
                hideKeyboard();
				languageSelectView.Hidden = false;
				languageSelectView.BackgroundColor = UIColor.LightGray.ColorWithAlpha((System.nfloat)0.5);
				languageCheck = "language";
			};

			learningLanguageBtn.TouchUpInside += (sender, ea) =>
			{
                hideKeyboard();
				languageSelectView.Hidden = false;
				languageSelectView.BackgroundColor = UIColor.LightGray.ColorWithAlpha((System.nfloat)0.5);
				languageCheck = "learning language";
			};

			czBtn.TouchUpInside += (sender, ea) =>
			{
				czBtn.SetTitleColor(UIColor.Blue, UIControlState.Normal);
				enBtn.SetTitleColor(UIColor.White, UIControlState.Normal);
				deBtn.SetTitleColor(UIColor.White, UIControlState.Normal);
				frBtn.SetTitleColor(UIColor.White, UIControlState.Normal);
				itBtn.SetTitleColor(UIColor.White, UIControlState.Normal);
				ruBtn.SetTitleColor(UIColor.White, UIControlState.Normal);
				jaBtn.SetTitleColor(UIColor.White, UIControlState.Normal);
				esBtn.SetTitleColor(UIColor.White, UIControlState.Normal);

				languageSelectView.Hidden = true;
				if (languageCheck == "language")
				{
					languageBtn.SetTitle(czBtn.Title(UIControlState.Normal), UIControlState.Normal);
				}
				else if (languageCheck == "learning language")
				{
					learningLanguageBtn.SetTitle(czBtn.Title(UIControlState.Normal), UIControlState.Normal);
				}
			};

			enBtn.TouchUpInside += (sender, ea) =>
			{
				czBtn.SetTitleColor(UIColor.White, UIControlState.Normal);
				enBtn.SetTitleColor(UIColor.Blue, UIControlState.Normal);
				deBtn.SetTitleColor(UIColor.White, UIControlState.Normal);
				frBtn.SetTitleColor(UIColor.White, UIControlState.Normal);
				itBtn.SetTitleColor(UIColor.White, UIControlState.Normal);
				ruBtn.SetTitleColor(UIColor.White, UIControlState.Normal);
				jaBtn.SetTitleColor(UIColor.White, UIControlState.Normal);
				esBtn.SetTitleColor(UIColor.White, UIControlState.Normal);

				languageSelectView.Hidden = true;
				if (languageCheck == "language")
				{
					languageBtn.SetTitle(enBtn.Title(UIControlState.Normal), UIControlState.Normal);
				}
				else if (languageCheck == "learning language")
				{
					learningLanguageBtn.SetTitle(enBtn.Title(UIControlState.Normal), UIControlState.Normal);
				}
			};

			deBtn.TouchUpInside += (sender, ea) =>
			{
				czBtn.SetTitleColor(UIColor.White, UIControlState.Normal);
				enBtn.SetTitleColor(UIColor.White, UIControlState.Normal);
				deBtn.SetTitleColor(UIColor.Blue, UIControlState.Normal);
				frBtn.SetTitleColor(UIColor.White, UIControlState.Normal);
				itBtn.SetTitleColor(UIColor.White, UIControlState.Normal);
				ruBtn.SetTitleColor(UIColor.White, UIControlState.Normal);
				jaBtn.SetTitleColor(UIColor.White, UIControlState.Normal);
				esBtn.SetTitleColor(UIColor.White, UIControlState.Normal);

				languageSelectView.Hidden = true;
				if (languageCheck == "language")
				{
					languageBtn.SetTitle(deBtn.Title(UIControlState.Normal), UIControlState.Normal);
				}
				else if (languageCheck == "learning language")
				{
					learningLanguageBtn.SetTitle(deBtn.Title(UIControlState.Normal), UIControlState.Normal);
				}
			};

			frBtn.TouchUpInside += (sender, ea) =>
			{
				czBtn.SetTitleColor(UIColor.White, UIControlState.Normal);
				enBtn.SetTitleColor(UIColor.White, UIControlState.Normal);
				deBtn.SetTitleColor(UIColor.White, UIControlState.Normal);
				frBtn.SetTitleColor(UIColor.Blue, UIControlState.Normal);
				itBtn.SetTitleColor(UIColor.White, UIControlState.Normal);
				ruBtn.SetTitleColor(UIColor.White, UIControlState.Normal);
				jaBtn.SetTitleColor(UIColor.White, UIControlState.Normal);
				esBtn.SetTitleColor(UIColor.White, UIControlState.Normal);

				languageSelectView.Hidden = true;
				if (languageCheck == "language")
				{
					languageBtn.SetTitle(frBtn.Title(UIControlState.Normal), UIControlState.Normal);
				}
				else if (languageCheck == "learning language")
				{
					learningLanguageBtn.SetTitle(frBtn.Title(UIControlState.Normal), UIControlState.Normal);
				}
			};

			itBtn.TouchUpInside += (sender, ea) =>
			{
				czBtn.SetTitleColor(UIColor.White, UIControlState.Normal);
				enBtn.SetTitleColor(UIColor.White, UIControlState.Normal);
				deBtn.SetTitleColor(UIColor.White, UIControlState.Normal);
				frBtn.SetTitleColor(UIColor.White, UIControlState.Normal);
				itBtn.SetTitleColor(UIColor.Blue, UIControlState.Normal);
				ruBtn.SetTitleColor(UIColor.White, UIControlState.Normal);
				jaBtn.SetTitleColor(UIColor.White, UIControlState.Normal);
				esBtn.SetTitleColor(UIColor.White, UIControlState.Normal);

				languageSelectView.Hidden = true;
				if (languageCheck == "language")
				{
					languageBtn.SetTitle(itBtn.Title(UIControlState.Normal), UIControlState.Normal);
				}
				else if (languageCheck == "learning language")
				{
					learningLanguageBtn.SetTitle(itBtn.Title(UIControlState.Normal), UIControlState.Normal);
				}
			};

			ruBtn.TouchUpInside += (sender, ea) =>
			{
				czBtn.SetTitleColor(UIColor.White, UIControlState.Normal);
				enBtn.SetTitleColor(UIColor.White, UIControlState.Normal);
				deBtn.SetTitleColor(UIColor.White, UIControlState.Normal);
				frBtn.SetTitleColor(UIColor.White, UIControlState.Normal);
				itBtn.SetTitleColor(UIColor.White, UIControlState.Normal);
				ruBtn.SetTitleColor(UIColor.Blue, UIControlState.Normal);
				jaBtn.SetTitleColor(UIColor.White, UIControlState.Normal);
				esBtn.SetTitleColor(UIColor.White, UIControlState.Normal);

				languageSelectView.Hidden = true;
				if (languageCheck == "language")
				{
					languageBtn.SetTitle(ruBtn.Title(UIControlState.Normal), UIControlState.Normal);
				}
				else if (languageCheck == "learning language")
				{
					learningLanguageBtn.SetTitle(ruBtn.Title(UIControlState.Normal), UIControlState.Normal);
				}
			};

			jaBtn.TouchUpInside += (sender, ea) =>
			{
				czBtn.SetTitleColor(UIColor.White, UIControlState.Normal);
				enBtn.SetTitleColor(UIColor.White, UIControlState.Normal);
				deBtn.SetTitleColor(UIColor.White, UIControlState.Normal);
				frBtn.SetTitleColor(UIColor.White, UIControlState.Normal);
				itBtn.SetTitleColor(UIColor.White, UIControlState.Normal);
				ruBtn.SetTitleColor(UIColor.White, UIControlState.Normal);
				jaBtn.SetTitleColor(UIColor.Blue, UIControlState.Normal);
				esBtn.SetTitleColor(UIColor.White, UIControlState.Normal);

				languageSelectView.Hidden = true;
				if (languageCheck == "language")
				{
					languageBtn.SetTitle(jaBtn.Title(UIControlState.Normal), UIControlState.Normal);
				}
				else if (languageCheck == "learning language")
				{
					learningLanguageBtn.SetTitle(jaBtn.Title(UIControlState.Normal), UIControlState.Normal);
				}
			};

			esBtn.TouchUpInside += (sender, ea) =>
			{
				czBtn.SetTitleColor(UIColor.White, UIControlState.Normal);
				enBtn.SetTitleColor(UIColor.White, UIControlState.Normal);
				deBtn.SetTitleColor(UIColor.White, UIControlState.Normal);
				frBtn.SetTitleColor(UIColor.White, UIControlState.Normal);
				itBtn.SetTitleColor(UIColor.White, UIControlState.Normal);
				ruBtn.SetTitleColor(UIColor.White, UIControlState.Normal);
				jaBtn.SetTitleColor(UIColor.White, UIControlState.Normal);
				esBtn.SetTitleColor(UIColor.Blue, UIControlState.Normal);

				languageSelectView.Hidden = true;
				if (languageCheck == "language")
				{
					languageBtn.SetTitle(esBtn.Title(UIControlState.Normal), UIControlState.Normal);
				}
				else if (languageCheck == "learning language")
				{
					learningLanguageBtn.SetTitle(esBtn.Title(UIControlState.Normal), UIControlState.Normal);
				}
			};


			// Upload the profile data on db

			registerBtn.TouchUpInside += (sender, ea) =>
			{
				var errorMesage = CheckInputs();

#if DEBUG
				errorMesage = String.Empty;
#endif

				if (String.IsNullOrEmpty(errorMesage))
				{
					var student = new Student()
					{
						BirthDate = birthdateBtn.CurrentTitle,
						City = cityTField.Text,
						Email = emailTField.Text,
						FirstName = firstNameTField.Text,
						LastName = lastNameTField.Text,
						Street = streetTField.Text,
						Telephone = telephoneTField.Text,
						TIN = tinTField.Text,
						ZipCode = zipcodeTField.Text,
						Created = DateTime.Now,
						LearningLanguage = learningLanguageBtn.CurrentTitle,
						Language = languageBtn.CurrentTitle,
						Photo = profileImg.Image.AsJPEG(0.5f).GetBase64EncodedData(NSDataBase64EncodingOptions.None).ToString()
					};

#if DEBUG
					//#else
					database.Insert(student);

					RegisterStudent(student);
#endif
					//var alert = new AlertDialog.Builder(this);
					//alert.SetTitle("Registration completed");
					//alert.SetMessage("Welcome to the language forum. Your registration was completed. Are you ready to learn?");
					//alert.SetNeutralButton("Yes", delegate
					//{
					//  alert.Dispose();
					//  alert = null;
					//  Finish();
					//});
					//alert.Show();

					//HideAlert(alert, 5000);


				}
				else
				{

					UIAlertView alert = new UIAlertView()
					{
						Title = "Validation error",
						Message = errorMesage
					};
					alert.AddButton("OK");
					alert.Show();

					//new AlertDialog.Builder(this).SetTitle("Validation error")
					//.SetMessage(errorMesage)
					//.SetCancelable(false)
					//.SetPositiveButton("OK", delegate { })
					//.Show();

				}
			};


			backBtn.TouchUpInside += (sender, ea) =>
			{
				this.NavigationController.PopViewController(true);
			};
			// Perform any additional setup after loading the view, typically from a nib.
		}

		private void InitDatabase()
		{
			database = new SQLDatabase();
		}

        private void hideKeyboard() {
            UIApplication.SharedApplication.KeyWindow.EndEditing(true);
        }

		private string CheckInputs()
		{
			var rtn = String.Empty;

			rtn += CheckInput(firstNameTField, lblFirstName);
			rtn += CheckInput(lastNameTField, lblLastName);
			rtn += CheckInput(streetTField, lblStreet);
			rtn += CheckInput(cityTField, lblCity);
			rtn += CheckInput(zipcodeTField, lblZipCode);
			rtn += CheckInput(telephoneTField, lblPhone);
			rtn += CheckInput(emailTField, lblEmail);
			//rtn += CheckEmail(emailTField, lblEmail);

			return rtn;
		}

		private string CheckInput(UITextField txt, UILabel lbl)
		{
			var rtn = String.Empty;

			if (String.IsNullOrEmpty(txt.Text))
			{
				rtn = lbl.Text + " is required.\n";
			}

			return rtn;
		}

		//private string CheckEmail(UITextField txt, UILabel lbl)
		//{
		//  var rtn = String.Empty;

		//  if (!iOS.Util.Patterns.EmailAddress.Matcher(txt.Text).Matches())
		//  {
		//      rtn = lbl.Text + " is not in correct format.\n";
		//  }

		//  return rtn;
		//}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}

		private void RegisterNewStudent()
		{
			var list = database.GetReadyToSendStudents();
			foreach (var student in list)
			{
				var sendResult = RegisterStudent(student);
				if (sendResult)
				{
					student.Sended = true;
					database.Update(student);
				}
			}

			// Neni potreba promazavat, pokud je seznam ke zpracovani prazdny
			if (list.Count() > 0)
				database.DeleteSendedStudents();
		}

		private bool RegisterStudent(Student student)
		{
			var rtn = false;

			try
			{
				DateTime birthdate = DateTime.MaxValue;
				try
				{
					birthdate = DateTime.ParseExact(student.BirthDate, "yyyy-MM-dd", null);
				}
				catch { }
				string photo = student.Photo;
				var result = ManagementService.AndroidService.RegisterNewStudent(student.FirstName, student.LastName, birthdate, student.Email, student.Telephone, student.City, student.Language, student.LearningLanguage, student.Street, student.ZipCode, student.TIN, photo);
				if (result == true)
				{
					rtn = true;
					showAlert();
				}

			}
			catch (Exception) { }

			return rtn;
		}

		protected void showAlert()
		{

			UIAlertView alert = new UIAlertView()
			{
				Title = "Registration completed",
				Message = "Welcome to the language forum. Your registration was completed. Are you ready to learn?"
			};
			alert.AddButton("Yes");
			alert.Show();
		}

		// Select the profile image from photo library.

		protected void Handle_FinishedPickingMedia(object sender, UIImagePickerMediaPickedEventArgs e)
		{
			// determine what was selected, video or image
			bool isImage = false;
			switch (e.Info[UIImagePickerController.MediaType].ToString())
			{
				case "public.image":
					Console.WriteLine("Image selected");
					isImage = true;
					break;
				case "public.video":
					Console.WriteLine("Video selected");
					break;
			}

			// get common info (shared between images and video)
			NSUrl referenceURL = e.Info[new NSString("UIImagePickerControllerReferenceURL")] as NSUrl;
			if (referenceURL != null)
				Console.WriteLine("Url:" + referenceURL.ToString());

			// if it was an image, get the other image info
			if (isImage)
			{
				// get the original image
				UIImage originalImage = e.Info[UIImagePickerController.OriginalImage] as UIImage;
				if (originalImage != null)
				{
					// do something with the image
					Console.WriteLine("got the original image");
					profileImg.Image = originalImage; // display
				}
			}
			else
			{ // if it's a video
			  // get video url
				NSUrl mediaURL = e.Info[UIImagePickerController.MediaURL] as NSUrl;
				if (mediaURL != null)
				{
					Console.WriteLine(mediaURL.ToString());
				}
			}
			// dismiss the picker
			imagePicker.DismissModalViewController(true);
		}

		void Handle_Canceled(object sender, EventArgs e)
		{
			imagePicker.DismissModalViewController(true);
		}

		// Detect the event of when edit the textfield

		private void TextChangedEvent(NSNotification notification)
		{
			UITextField field = (UITextField)notification.Object;

			if (notification.Object == firstNameTField)
			{

				firstNameTField.Layer.BorderWidth = 1;
				firstNameTField.Layer.BorderColor = UIColor.LightGray.CGColor;

			}
			else if (notification.Object == lastNameTField)
			{

				lastNameTField.Layer.BorderWidth = 1;
				lastNameTField.Layer.BorderColor = UIColor.LightGray.CGColor;

			}
			else if (notification.Object == streetTField)
			{

				streetTField.Layer.BorderWidth = 1;
				streetTField.Layer.BorderColor = UIColor.LightGray.CGColor;

			}
			else if (notification.Object == cityTField)
			{

				cityTField.Layer.BorderWidth = 1;
				cityTField.Layer.BorderColor = UIColor.LightGray.CGColor;

			}
			else if (notification.Object == zipcodeTField)
			{

				zipcodeTField.Layer.BorderWidth = 1;
				zipcodeTField.Layer.BorderColor = UIColor.LightGray.CGColor;

			}
			else if (notification.Object == telephoneTField)
			{

				telephoneTField.Layer.BorderWidth = 1;
				telephoneTField.Layer.BorderColor = UIColor.LightGray.CGColor;

			}
			else if (notification.Object == emailTField)
			{

				emailTField.Layer.BorderWidth = 1;
				emailTField.Layer.BorderColor = UIColor.LightGray.CGColor;

			}
			else if (notification.Object == tinTField)
			{

				tinTField.Layer.BorderWidth = 1;
				tinTField.Layer.BorderColor = UIColor.LightGray.CGColor;

			}
		}
	}

}

