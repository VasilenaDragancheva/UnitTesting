namespace BuhtigIssueTracker.Contracts
{
    using Enum;

    public interface IIssueTracker
    {
        string AddComment(int issueId, string text);

        string CreateIssue(string title, string description, IssuePriority priority, string[] tags);

        string GetMyComments();

        string GetMyIssues();

        string LoginUser(string username, string password);

        string LogoutUser();

        string RegisterUser(string username, string password, string confirmPassword);

        string RemoveIssue(int issueId);

        string SearchForIssues(string[] tags);
    }
}