using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_CollegeWebApplication.Models
{
    public class Student
    {
        public string firstName;
        public string lastName;
        public DateTime birthday;
        public string email;

        public Student(string firstName, string lastName, DateTime birthday, string email)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.birthday = birthday;
            this.email = email;
        }
    }
}