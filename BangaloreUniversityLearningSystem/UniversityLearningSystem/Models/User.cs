namespace UniversityLearningSystem
{
    using System;
    using System.Collections.Generic;

    using Models;

    using Utilities;

    public class User
    {
        private string username;

        private string passwordHash;

        public User(string username, string password, Role role)
        {
            this.Username = username;
            this.PasswordHashed = password;
            this.Role = role;
            this.Courses = new List<Course>();
        }

        public string Username
        {
            get
            {
                return this.username;
            }

            set
            {
                if (string.IsNullOrEmpty(value) || value.Length < 5)
                {
                    string message = "The username must be at least 5 symbols long.";
                    throw new ArgumentException(message);
                }

                this.username = value;
            }
        }

        public string PasswordHashed
        {
            get
            {
                return this.passwordHash;
            }

            set
            {
                if (string.IsNullOrEmpty(value) || value.Length < 5)
                {
                    string message = "The password must be at least 5 symbols long.";
                    throw new ArgumentException(message);
                }

                this.passwordHash = HashUtilities.HashPassword(value);
            }
        }

        public Role Role { get; private set; }

        public IList<Course> Courses { get; private set; }
    }
}