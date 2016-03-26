namespace BuhtigIssueTracker.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    using Contracts;

    using Enum;

    using Models;

    public class IssueTrackerController : IIssueTracker
    {
        public IssueTrackerController()
            : this(new BuhtigIssueTrackerData())
        {
        }

        public IssueTrackerController(IBuhtigIssueTrackerData data)
        {
            this.Data = data;
        }

        public IBuhtigIssueTrackerData Data { get; set; }

        public string AddComment(int issueId, string text)
        {
            if (this.Data.LoggedUser == null)
            {
                return "There is no currently logged in user";
            }

            if (!this.Data.IssueById.ContainsKey(issueId))
            {
                return string.Format("There is no issue with ID {0}", issueId);
            }

            var issue = this.Data.IssueById[issueId];
            var comment = new Comment(this.Data.LoggedUser, text);
            issue.AddComment(comment);
            this.Data.CommentsByUser[this.Data.LoggedUser].Add(comment);

            return string.Format("Comment added successfully to issue {0}", issue.Id);
        }

        public string CreateIssue(string title, string description, IssuePriority priority, string[] tags)
        {
            if (this.Data.LoggedUser == null)
            {
                return "There is no currently logged in user";
            }

            var issue = new Issue(title, description, priority, tags.Distinct().ToList());
            this.Data.AddIssue(issue);
            return string.Format("Issue {0} created successfully", issue.Id);
        }

        public string GetMyComments()
        {
            if (this.Data.LoggedUser == null)
            {
                return "There is no currently logged in user";
            }

            // Bottle neck
            var comments = this.Data.CommentsByUser[this.Data.LoggedUser];
            if (!comments.Any())
            {
                return "No comments";
            }

            return string.Join(Environment.NewLine, comments);
        }

        public string GetMyIssues()
        {
            if (this.Data.LoggedUser == null)
            {
                return "There is no currently logged in user";
            }

            var issues = this.Data.IssuesByUsername[this.Data.LoggedUser.Username];

            if (!issues.Any())
            {
                // Bottle neck
                return "No issues";
            }

            return string.Join(Environment.NewLine, issues.OrderByDescending(x => x.Priority).ThenBy(x => x.Title));
        }

        public string LoginUser(string username, string password)
        {
            if (this.Data.LoggedUser != null)
            {
                return "There is already a logged in user";
            }

            if (!this.Data.UsersByUsername.ContainsKey(username))
            {
                return string.Format("A user with username {0} does not exist", username);
            }

            var user = this.Data.UsersByUsername[username];
            if (user.HashedPassword != this.HashPassword(password))
            {
                return string.Format("The password is invalid for user {0}", username);
            }

            this.Data.LoggedUser = user;

            return string.Format("User {0} logged in successfully", username);
        }

        public string LogoutUser()
        {
            if (this.Data.LoggedUser == null)
            {
                return "There is no currently logged in user";
            }

            string username = this.Data.LoggedUser.Username;
            this.Data.LoggedUser = null;
            return string.Format("User {0} logged out successfully", username);
        }

        public string RegisterUser(string username, string password, string confirmPassword)
        {
            if (this.Data.LoggedUser != null)
            {
                return "There is already a logged in user";
            }

            if (password != confirmPassword)
            {
                return string.Format("The provided passwords do not match", username);
            }

            if (this.Data.UsersByUsername.ContainsKey(username))
            {
                return string.Format("A user with username {0} already exists", username);
            }

            var hashedPassword = this.HashPassword(password);
            var user = new User(username, hashedPassword);
            this.Data.UsersByUsername.Add(username, user);
            return string.Format("User {0} registered successfully", username);
        }

        public string RemoveIssue(int issueId)
        {
            if (this.Data.LoggedUser == null)
            {
                return "There is no currently logged in user";
            }

            if (!this.Data.IssueById.ContainsKey(issueId))
            {
                return string.Format("There is no issue with ID {0}", issueId);
            }

            var issue = this.Data.IssueById[issueId];
            if (!this.Data.IssuesByUsername[this.Data.LoggedUser.Username].Contains(issue))
            {
                return string.Format(
                    "The issue with ID {0} does not belong to user {1}", 
                    issueId, 
                    this.Data.LoggedUser.Username);
            }

            this.Data.RemoveIssue(issue);
            return string.Format("Issue {0} removed", issueId);
        }

        public string SearchForIssues(string[] tags)
        {
            if (tags.Length == 0)
            {
                return "There are no tags provided";
            }

            var issues = new List<Issue>();
            foreach (var tag in tags)
            {
                issues.AddRange(this.Data.IssuesByTags[tag]);
            }

            if (!issues.Any())
            {
                return "There are no issues matching the tags provided";
            }

            issues = issues.Distinct().ToList();
            var result = string.Join(
                Environment.NewLine, 
                issues.OrderByDescending(x => x.Priority).ThenBy(x => x.Title));
            return result;
        }

        private string HashPassword(string password)
        {
            return string.Join(
                string.Empty, 
                SHA1.Create().ComputeHash(Encoding.Default.GetBytes(password)).Select(x => x.ToString()));
        }
    }
}