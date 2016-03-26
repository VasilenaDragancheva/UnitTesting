namespace UniversityLearningSystem.Models
{
    using System;
    using System.Collections.Generic;

    public class Course
    {
        private string name;

        public Course(string name)
        {
            this.Lectures = new List<Lecture>();
        }

        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                if (string.IsNullOrEmpty(value) || value.Length < 5)
                {
                    string message = "The course value must be at least 5 symbols long.";
                    throw new ArgumentException(message);
                }

                this.name = value;
            }
        }

        public IList<Lecture> Lectures { get; set; }

        public IList<User> Students { get; set; }

        public void AddLecture(Lecture lecture)
        {
            this.Lectures.Add(lecture);
        }

        public void AddStudent(User student)
        {
            this.Students.Add(student);
            student.Courses.Add(this);
        }
    }
}