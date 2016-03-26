namespace BuhtigIssueTracker.Models
{
    public class User
    {
        public User(string username, string hashedPassword)
        {
            this.Username = username;
            this.HashedPassword = hashedPassword;
        }

        public string Username { get; set; }

        public string HashedPassword { get; set; }
    }
}