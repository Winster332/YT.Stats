using System;

namespace YT.Stats.YT.Issue
{
	public class YTIssue
	{
		public string Id { get; }
		public string Description { get; }
		public string Summary { get; }
		public string ProjectName { get; }
		public string ReporterFullName { get; }
		public DateTime CreatedDateTime { get; }
		public DateTime UpdatedDateTime { get; }
		public StateType State { get; }
		public PriorityType Priority { get; }
		public IssueType Type { get; }
		public decimal SpentTime { get; }

		public YTIssue(
			string id, 
			string description, 
			string summary, 
			string projectName, 
			string reporterFullName,
			DateTime createdDateTime,
			DateTime updatedDateTime,
			StateType state,
			PriorityType priority,
			IssueType type,
			decimal spentTime)
		{
			Id = id;
			Description = description;
			Summary = summary;
			ProjectName = projectName;
			ReporterFullName = reporterFullName;
			CreatedDateTime = createdDateTime;
			UpdatedDateTime = updatedDateTime;
			State = state;
			Priority = priority;
			Type = type;
			SpentTime = spentTime;
		}
	}
}