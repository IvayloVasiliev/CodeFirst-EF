using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentSystem.Data.Models
{
    public class Student
    {
        public Student(string name, string phoneNumber = null, DateTime? birthday = null)
        {
            this.Name = name;
            this.PhoneNumber = phoneNumber;
            this.RegisteredOn = DateTime.Now;
            this.Birthday = birthday;
            this.HomeworkSubmissions = new List<Homework>();
            this.CourseEnrollments = new List<StudentCourse>();
        }

        [Key]
        public int StudentId { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(100)")]
        public string Name { get; private set; }

        [Column(TypeName = "CHAR(10)")]
        public string PhoneNumber  { get; set; }

        public DateTime RegisteredOn { get; private set; }

        public DateTime? Birthday { get; set; }

        public ICollection<Homework> HomeworkSubmissions { get; set; }

        public ICollection<StudentCourse> CourseEnrollments { get; set; }
    }
}
