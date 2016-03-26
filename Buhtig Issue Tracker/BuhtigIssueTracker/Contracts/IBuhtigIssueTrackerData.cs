namespace BuhtigIssueTracker.Contracts
{
    using System.Collections.Generic;

    using Models;

    using Wintellect.PowerCollections;

    public interface IBuhtigIssueTrackerData
    {
        User LoggedUser { get; set; }

        IDictionary<string, User> UsersByUsername { get; }

        OrderedDictionary<int, Issue> IssueById { get; }

        MultiDictionary<string, Issue> IssuesByUsername { get; }

        MultiDictionary<string, Issue> IssuesByTags { get; }

        MultiDictionary<User, Comment> CommentsByUser { get; }

        void AddIssue(Issue p);

        void RemoveIssue(Issue p);
    }
}