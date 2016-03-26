namespace UniversityLearningSystem.Data
{
    using System.Collections.Generic;

    public class UsersRepository : Repository<User>
    {
        private readonly Dictionary<string, User> usersByUsername;

        public UsersRepository()
        {
            this.usersByUsername = new Dictionary<string, User>();
        }

        public User GetByUsername(string username)
        {
            if (this.usersByUsername.ContainsKey(username))
            {
                return this.usersByUsername[username];
            }

            return null;
        }
    }
}