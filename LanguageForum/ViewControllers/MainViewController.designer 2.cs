// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace LanguageForum.ViewControllers
{
    [Register ("MainViewController")]
    partial class MainViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView logoImg { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton registerBtn { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton start60Btn { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton start90Btn { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (logoImg != null) {
                logoImg.Dispose ();
                logoImg = null;
            }

            if (registerBtn != null) {
                registerBtn.Dispose ();
                registerBtn = null;
            }

            if (start60Btn != null) {
                start60Btn.Dispose ();
                start60Btn = null;
            }

            if (start90Btn != null) {
                start90Btn.Dispose ();
                start90Btn = null;
            }
        }
    }
}