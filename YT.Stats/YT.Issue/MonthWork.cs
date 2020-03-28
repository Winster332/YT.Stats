using System.Linq;

namespace YT.Stats.YT.Issue
{
	public class MonthWork
	{
		public int Month { get; }
		public int Year { get; }
		public YTIssue[] Issues { get; }

		public MonthWork(int year, int month, YTIssue[] issues)
		{
			Year = year;
			Month = month;
			Issues = issues;
		}

		public YTIssue[] GetByState(StateType state) 
			=> Issues.Where(c => c.State == state).ToArray();
	}
}