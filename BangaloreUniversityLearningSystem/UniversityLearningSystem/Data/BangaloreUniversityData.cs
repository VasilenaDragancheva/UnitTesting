namespace UniversityLearningSystem.Data
{
    using Interfaces;

    using Models;

    public class BangaloreUniversityData : IBangaloreUniversityData
    {
        public BangaloreUniversityData()
        {
            this.Users = new UsersRepository();
            this.Courses = new Repository<Course>();
        }

        public BangaloreUniversityData(UsersRepository users, IRepository<Course> courses)
        {
            this.Users = users;
            this.Courses = courses;
        }

        public UsersRepository Users { get; set; }

        public IRepository<Course> Courses { get; set; }
    }
}