using System;
using System.Collections.Generic;
using Foundation;
using UIKit;

namespace LanguageForum.Classes
{
    public class StudentsTableViewSource : UITableViewSource
    {
        private List<string> studentTable;

        public StudentsTableViewSource(List<string> studentTable)
        {
            this.studentTable = studentTable;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = new UITableViewCell(UITableViewCellStyle.Default, "cell_student");


            cell.TextLabel.TextColor = UIColor.DarkGray;
            cell.TextLabel.TextAlignment = UITextAlignment.Center;
            cell.TextLabel.Text = studentTable[indexPath.Row];

            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return studentTable.Count;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath) {
            var selectedStudent = studentTable[indexPath.Row];
        }
    }
}
