using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel;
using SQLite.Net.Attributes;

namespace LanguageForum.Model
{
    public class Lesson 
        //: INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        public Guid Id { get; set; }

        public LessonType LessonType { get; set; }

        public string Description { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Closed { get; set; }
        public bool Sended { get; set; }

        public bool Canceled { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}