namespace BuhtigIssueTracker.Core
{
    using System.Collections.Generic;

    using Contracts;

    using Models;

    using Wintellect.PowerCollections;

    public class BuhtigIssueTrackerData : IBuhtigIssueTrackerData
    {
        private int nextIssueId;

        public BuhtigIssueTrackerData()
        {
            this.nextIssueId = 1;
            this.UsersByUsername = new Dictionary<string, User>();

            this.IssueById = new OrderedDictionary<int, Issue>();
            this.IssuesByUsername = new MultiDictionary<string, Issue>(true);
            this.IssuesByTags = new MultiDictionary<string, Issue>(true);
            this.CommentsByUser = new MultiDictionary<User, Comment>(true);
            this.UsersByUsername = new Dictionary<string, User>();
        }

        public User LoggedUser { get; set; }

        public IDictionary<string, User> UsersByUsername { get; set; }

        public OrderedDictionary<int, Issue> IssueById { get; set; }

        public MultiDictionary<string, Issue> IssuesByUsername { get; set; }

        public MultiDictionary<string, Issue> IssuesByTags { get; set; }

        public MultiDictionary<User, Comment> CommentsByUser { get; set; }

        public void AddIssue(Issue issue)
        {
            issue.Id = this.nextIssueId;
            this.IssueById.Add(issue.Id, issue);
            this.nextIssueId++;
            this.IssuesByUsername[this.LoggedUser.Username].Add(issue);
            foreach (var tag in issue.Tags)
            {
                this.IssuesByTags[tag].Add(issue);
            }
        }

        public void RemoveIssue(Issue issue)
        {
            this.IssuesByUsername[this.LoggedUser.Username].Remove(issue);
            foreach (var tag in issue.Tags)
            {
                this.IssuesByTags[tag].Remove(issue);
            }

            this.IssueById.Remove(issue.Id);
        }
    }
}