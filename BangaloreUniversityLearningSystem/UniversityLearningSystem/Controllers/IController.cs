namespace UniversityLearningSystem.Controllers
{
    using Interfaces;

    public interface IController
    {
        User User { get; set; }

        bool HasCurrentUser { get; }

        IView View(object model);
    }
}