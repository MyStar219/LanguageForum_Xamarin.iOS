using System;

using SQLite.Net.Attributes;

namespace LanguageForum.Model
{
    public class Student
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Street { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }

        public string Telephone { get; set; }
        public string Email { get; set; }

        public string BirthDate { get; set; }
        public string TIN { get; set; }

        public string Language { get; set; }
        public string LearningLanguage { get; set; }

        public DateTime Created { get; set; }

        public bool Sended { get; set; }

        public string Photo { get; set; }
    }
}