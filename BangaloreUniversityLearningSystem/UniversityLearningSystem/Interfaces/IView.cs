namespace UniversityLearningSystem.Interfaces
{
    public interface IView
    {
        object Model { get; }

        string Display();
    }
}