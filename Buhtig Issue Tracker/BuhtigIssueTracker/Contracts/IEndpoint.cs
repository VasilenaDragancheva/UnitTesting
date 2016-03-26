namespace BuhtigIssueTracker.Contracts
{
    using System.Collections.Generic;

    public interface IEndpoint
    {
        string CommandName { get; }

        IDictionary<string, string> CommandParameters { get; }
    }
}