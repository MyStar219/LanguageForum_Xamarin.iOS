using System;
using LanguageForum.Global;

using UIKit;

namespace LanguageForum.ViewControllers
{
	public partial class MainViewController : UIViewController
	{

		protected MainViewController(IntPtr handle) : base(handle)
		{

		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			start60Btn.Layer.CornerRadius = 4;
			start90Btn.Layer.CornerRadius = 4;
			registerBtn.Layer.CornerRadius = 4;



			registerBtn.TouchUpInside += (sender, ea) =>
			{

				UIViewController newViewController = (UIViewController)Storyboard.InstantiateViewController("RegistrationViewController");
				this.NavigationController.PushViewController(newViewController, true);
			};

			start60Btn.TouchUpInside += (sender, ea) =>
			{
                if (SingleData.getInstance().currentLesson == "60min")
				{
                    SingleData.getInstance().lessionCheck = "60min";
					Start60LessionViewController newViewController = (Start60LessionViewController)Storyboard.InstantiateViewController("Start60LessionViewController");
					this.NavigationController.PushViewController(newViewController, true);
				}
                else if (SingleData.getInstance().currentLesson == "90min")
				{

					// display the alert 
					var yesNoalert = UIAlertController.Create("Start new lesson", "Another lesson is active. Start new one?", UIAlertControllerStyle.Alert);
					yesNoalert.AddAction(UIAlertAction.Create("Yes", UIAlertActionStyle.Default, alert => showLessonViewController60()));
					yesNoalert.AddAction(UIAlertAction.Create("No", UIAlertActionStyle.Default, alert => Console.WriteLine("No was Clicked")));
					PresentViewController(yesNoalert, true, null);

				}
				else
				{
					showLessonViewController60();
				}
			};

			start90Btn.TouchUpInside += (sender, ea) =>
			{

                if (SingleData.getInstance().currentLesson == "60min")
				{
                    
					// display the alert
					var yesNoalert = UIAlertController.Create("Start new lesson", "Another lesson is active. Start new one?", UIAlertControllerStyle.Alert);
					yesNoalert.AddAction(UIAlertAction.Create("Yes", UIAlertActionStyle.Default, alert => showLessonViewController90()));
					yesNoalert.AddAction(UIAlertAction.Create("No", UIAlertActionStyle.Default, alert => Console.WriteLine("No was Clicked")));
					PresentViewController(yesNoalert, true, null);

				}
                else if (SingleData.getInstance().currentLesson == "90min")
				{
                    SingleData.getInstance().lessionCheck = "90min";

					Start60LessionViewController newViewController = (Start60LessionViewController)Storyboard.InstantiateViewController("Start60LessionViewController");
					this.NavigationController.PushViewController(newViewController, true);
				}
				else
				{
					showLessonViewController90();
				}

			};

			// Perform any additional setup after loading the view, typically from a nib.
		}

		public void showLessonViewController90()
		{
            SingleData.getInstance().lessionCheck = "90min";
            SingleData.getInstance().startProgress = 0;

			Start60LessionViewController newViewController = (Start60LessionViewController)Storyboard.InstantiateViewController("Start60LessionViewController");
			this.NavigationController.PushViewController(newViewController, true);
		}

		public void showLessonViewController60()
		{
            SingleData.getInstance().lessionCheck = "60min";
            SingleData.getInstance().startProgress = 0;

			Start60LessionViewController newViewController = (Start60LessionViewController)Storyboard.InstantiateViewController("Start60LessionViewController");
			this.NavigationController.PushViewController(newViewController, true);
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}

	}
}

