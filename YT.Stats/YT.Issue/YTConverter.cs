using System.Collections.Generic;
using System.Linq;

namespace YT.Stats.YT.Issue
{
	public class YTConverter
	{
		public static YTIssue ConvertFromIssue(YouTrackSharp.Issues.Issue issue)
			=> new YTIssue(
				issue.Id,
				issue.Description,
				issue.Summary,
				issue.GetField("projectShortName").AsString(),
				issue.GetField("reporterFullName").AsString(),
				issue.GetField("created").AsDateTime(),
				issue.GetField("updated").AsDateTime(),
				GetStateFromString(issue.GetField("State").AsCollection().FirstOrDefault()),
				GetPriorityFromString(issue.GetField("Приоритет").AsCollection().FirstOrDefault()),
				GetTypeFromString(issue.GetField("Type").AsCollection().FirstOrDefault()),
				GetSpentTime(issue.GetField("Затраченное время")?.AsCollection()?.Select(decimal.Parse)
					?.ToArray()));
		
		private static StateType GetStateFromString(string state)
		{
			switch (state)
			{
				case "To Verify": return StateType.ToVerify;
				case "In Progress": return StateType.InProgress;
				case "Done": return StateType.Done;
				default: return StateType.Open;
			}
		}

		private static IssueType GetTypeFromString(string type)
		{
			switch (type)
			{
				case "Bug": return IssueType.Bug;
				case "Epic": return IssueType.Epic;
				case "User Story": return IssueType.UserStory;
				default: return IssueType.Task;
			}
		}

		private static PriorityType GetPriorityFromString(string type)
		{
			switch (type)
			{
				case "Minor": return PriorityType.Minor;
				case "Show-stopper": return PriorityType.ShowStopper;
				case "Key Business Target": return PriorityType.KeyBusinessTarget;
				default: return PriorityType.Normal;
			}
		}

		private static decimal GetSpentTime(IEnumerable<decimal> minutes)
			=> minutes?.Sum() / 60 ?? 0;
	}
}