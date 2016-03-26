namespace BuhtigIssueTracker.Core
{
    using System;

    using Contracts;

    using Enum;

    public class CommandDispatcher
    {
        public CommandDispatcher()
            : this(new IssueTrackerController())
        {
        }

        public CommandDispatcher(IIssueTracker tracker)
        {
            this.Tracker = tracker;
        }

        public IIssueTracker Tracker { get; set; }

        public string DispatchAction(IEndpoint endpoint)
        {
            switch (endpoint.CommandName)
            {
                case "RegisterUser":
                    return this.Tracker.RegisterUser(
                        endpoint.CommandParameters["username"], 
                        endpoint.CommandParameters["password"], 
                        endpoint.CommandParameters["confirmPassword"]);
                case "LoginUser":
                    return this.Tracker.LoginUser(
                        endpoint.CommandParameters["username"], 
                        endpoint.CommandParameters["password"]);
                case "LogoutUser":
                    return this.Tracker.LogoutUser();
                case "CreateIssue":
                    return this.Tracker.CreateIssue(
                        endpoint.CommandParameters["title"], 
                        endpoint.CommandParameters["description"], 
                        (IssuePriority)Enum.Parse(typeof(IssuePriority), endpoint.CommandParameters["priority"], true), 
                        endpoint.CommandParameters["tags"].Split('|'));
                case "RemoveIssue":
                    return this.Tracker.RemoveIssue(int.Parse(endpoint.CommandParameters["id"]));
                case "AddComment":
                    return this.Tracker.AddComment(
                        int.Parse(endpoint.CommandParameters["id"]), 
                        endpoint.CommandParameters["text"]);
                case "MyIssues":
                    return this.Tracker.GetMyIssues();
                case "MyComments":
                    return this.Tracker.GetMyComments();
                case "Search":
                    return this.Tracker.SearchForIssues(endpoint.CommandParameters["tags"].Split('|'));
                default:
                    return string.Format("Invalid action: {0}", endpoint.CommandName);
            }
        }
    }
}