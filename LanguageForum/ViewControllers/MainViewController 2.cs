using System;

using UIKit;

namespace LanguageForum.ViewControllers
{
    public partial class MainViewController : UIViewController
    {
        public MainViewController() : base("MainViewController", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            registerBtn.TouchUpInside += (sender, ea) =>
            {
                UIViewController newViewController = (UIViewController)Storyboard.InstantiateViewController("RegistrationViewController");
                this.NavigationController.PushViewController(newViewController, true);
            };

            start60Btn.TouchUpInside += (sender, ea) =>
            {

            };

            start90Btn.TouchUpInside += (sender, ea) =>
			{

			};

            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

