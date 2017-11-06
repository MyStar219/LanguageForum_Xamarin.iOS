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
    [Register ("Start60LessionViewController")]
    partial class Start60LessionViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView activeView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton backBtn { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton cancelLessonBtn { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView forumTView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lessonLbl { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField lessonTField { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView logoImg { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISlider progressSlider { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton scanCodeBtn { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel studentNumberLbl { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView studentTableView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel teacherNameLbl { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (activeView != null) {
                activeView.Dispose ();
                activeView = null;
            }

            if (backBtn != null) {
                backBtn.Dispose ();
                backBtn = null;
            }

            if (cancelLessonBtn != null) {
                cancelLessonBtn.Dispose ();
                cancelLessonBtn = null;
            }

            if (forumTView != null) {
                forumTView.Dispose ();
                forumTView = null;
            }

            if (lessonLbl != null) {
                lessonLbl.Dispose ();
                lessonLbl = null;
            }

            if (lessonTField != null) {
                lessonTField.Dispose ();
                lessonTField = null;
            }

            if (logoImg != null) {
                logoImg.Dispose ();
                logoImg = null;
            }

            if (progressSlider != null) {
                progressSlider.Dispose ();
                progressSlider = null;
            }

            if (scanCodeBtn != null) {
                scanCodeBtn.Dispose ();
                scanCodeBtn = null;
            }

            if (studentNumberLbl != null) {
                studentNumberLbl.Dispose ();
                studentNumberLbl = null;
            }

            if (studentTableView != null) {
                studentTableView.Dispose ();
                studentTableView = null;
            }

            if (teacherNameLbl != null) {
                teacherNameLbl.Dispose ();
                teacherNameLbl = null;
            }
        }
    }
}