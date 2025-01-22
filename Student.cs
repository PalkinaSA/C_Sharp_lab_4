using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CSharp_lab_4
{
    [Serializable]
    public class Student
    {
        private string surname;
        private string name;
        private int schoolNumber;
        private int score;

        public string Surname { 
            get { return surname; } 
            set { if (!string.IsNullOrWhiteSpace(value) && value.Length <= 30) surname = value; } 
        }

        public string Name { 
            get { return name; }
            set { if (!string.IsNullOrWhiteSpace(value) && value.Length <= 20) name = value; }
        }

        public int SchoolNumber { 
            get { return schoolNumber; }
            set { if (value >= 1 && value <= 99) schoolNumber = value; }
        }

        public int Score { 
            get { return score; }
            set { if (value >= 1 && value <= 100) score = value; }
        }

        public Student() { }

        public Student(string surname, string name, int schoolNumber, int score)
        {
            if (string.IsNullOrWhiteSpace(surname) || surname.Length > 30)
                throw new ArgumentException("Invalid last name");
            if (string.IsNullOrWhiteSpace(name) || name.Length > 20)
                throw new ArgumentException("Invalid first name");
            if (schoolNumber < 1 || schoolNumber > 99)
                throw new ArgumentException("Invalid school number");
            if (score < 1 || score > 100)
                throw new ArgumentException("Invalid score");

            this.surname = surname;
            this.name = name;
            this.schoolNumber = schoolNumber;
            this.score = score;
        }
    }
}