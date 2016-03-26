namespace UniversityLearningSystem.Views.Users
{
    using System.Text;

    public class Login : View
    {
        public Login(User user)
            : base(user)
        {
        }

        protected override void BuildViewResult(StringBuilder viewResult)
        {
            viewResult.AppendFormat("User {0} logged in successfully.", (this.Model as User).Username).AppendLine();
        }
    }
}