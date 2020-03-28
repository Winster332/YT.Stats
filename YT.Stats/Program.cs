using System;
using System.Threading.Tasks;
using YouTrackSharp;
using YouTrackSharp.Issues;

namespace YT.Stats
{
	class Program
	{
		static async Task Main(string[] args)
		{
			var appSettings = AppSettings.Load("appsettings.json");
			
			var connection = new BearerTokenConnection(appSettings.ServerUrl, appSettings.Token);
			var issuesService = new IssuesService(connection);
			var ytIssues = await issuesService.GetIssues("Исполнитель: me sort by: State asc", null, int.MaxValue);
			
			var tableService = new IssuesTableService(appSettings.FixedSalary);

			if (appSettings.Mode == "all-csv")
			{
				tableService.ToCsv(ytIssues, appSettings.FilePath);
			}
			else if (appSettings.Mode == "last-csv")
			{
				tableService.ToCsvLastTable(ytIssues, appSettings.FilePath);
			}
			else if (appSettings.Mode == "last-list")
			{
				tableService.ToListFile(ytIssues, appSettings.FilePath);
			}
			else
			{
				tableService.Render(ytIssues);
			}

			Console.ReadKey();
		}
	}
}