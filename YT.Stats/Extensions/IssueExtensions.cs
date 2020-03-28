using System.Collections.Generic;
using System.Linq;
using YouTrackSharp.Issues;
using YT.Stats.YT.Issue;

namespace YT.Stats.Extensions
{
	public static class IssueExtensions
	{
		public static IEnumerable<MonthWork> GetWorksFromIssues(this IEnumerable<Issue> ytIssues)
			=> ytIssues.Select(YTConverter.ConvertFromIssue)
				.GroupBy(i => new
				{
					Year = i.CreatedDateTime.Year,
					Month = i.CreatedDateTime.Month
				})
				.Select(c => new MonthWork(c.Key.Year, c.Key.Month, c.ToArray()))
				.OrderByDescending(c => c.Month)
				.ToArray();
	}
}