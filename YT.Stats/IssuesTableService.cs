using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using YouTrackSharp.Issues;
using YT.Stats.Extensions;
using YT.Stats.YT.Issue;
using YT.Stats.YT.Table;

namespace YT.Stats
{
	public class IssuesTableService
	{
		public WorkSalary Salary { get; }
		public IssuesTableService(decimal salary)
		{
			Salary = new WorkSalary(salary);
		}

		public void Render(ICollection<Issue> ytIssues)
		{
			var works = ytIssues.GetWorksFromIssues();

			Console.WriteLine($"Salary: {Salary.Salary}р");
			Console.WriteLine($"Price day: {Salary.PriceOfDay}р");
			Console.WriteLine($"Price hour: {Salary.PriceOfHour}р");
			
			foreach (var work in works)
			{
				var table = new Table();
				var renderer = new TableRenderer(table);
				
				table.AddHead("Id", "Type", "\tState", "\tCreated", "\tUpdated",
					"\tReporter", "Spent", "Price", "\tDescription");
				
				Console.WriteLine();
				
				for (var i = 0; i < work.Issues.Length; i++)
				{
					var issue = work.Issues[i];
					var id = issue.Id;
					var type = issue.Type.ToString();
					var state = new TableCell($"{issue.State.ToString()}");
					if (issue.Type != IssueType.UserStory)
					{
						state.Value = $"\t{state.Value}";
					}

					if (issue.State == StateType.Done)
					{
						state.Color = ConsoleColor.DarkGreen;
					}
					if (issue.State == StateType.InProgress)
					{
						state.Color = ConsoleColor.DarkMagenta;
					}
					if (issue.State == StateType.ToVerify)
					{
						state.Color = ConsoleColor.DarkCyan;
					}
					if (issue.State == StateType.Open)
					{
						state.Color = ConsoleColor.DarkYellow;
					}
					
					var created = issue.CreatedDateTime.ToString("MM.dd.yyyy");
					var updated = issue.UpdatedDateTime.ToString("MM.dd.yyyy");
					var formName = issue.ReporterFullName.Split(" ");
					var reporter = $"{formName[0][0]}. {formName[1]}";

					if (issue.State == StateType.Done || issue.State == StateType.Open)
					{
						created = $"\t{created}";
					}

					var summary = issue.Summary;

					var spentTime = new TableCell($"{decimal.Round((decimal) issue.SpentTime, 3)} h", state.Color);
					if ((issue.State == StateType.Done || issue.State == StateType.ToVerify) && issue.SpentTime == 0)
					{
						spentTime.Color = ConsoleColor.DarkRed;
					}
					var price = $"{(Salary.PriceOfHour * issue.SpentTime).AsRouble()}р";
					if (price.Length <= 6)
					{
						summary = $"\t{summary}";
					}

					table.AddBodyRow(new TableCell(id, state.Color), 
						new TableCell(type, state.Color), 
						state,
						new TableCell(created, state.Color), 
						new TableCell(updated, state.Color), 
						new TableCell(reporter, state.Color), 
						spentTime, 
						new TableCell(price, spentTime.Color), 
						new TableCell(summary, state.Color));
				}
				
				var totalSpentTime = work.Issues.Select(i => i.SpentTime).Sum();
				var totalPrice = (Salary.PriceOfHour * totalSpentTime).AsRouble();
				var progress = decimal.Round((100m / Salary.NeedWorkHours) * totalSpentTime, 1);
				var leftProgress = 100m - progress;
				var leftPrice = Salary.Salary - totalPrice;
				var leftSpentTime = Salary.NeedWorkHours - totalSpentTime;
				table.AddFooter($"Необходимо отработать за месяц: {Salary.NeedWorkHours} часов", ConsoleColor.DarkCyan);
				table.AddFooter($"Отработано по времени: {decimal.Round(totalSpentTime, 1)} часов", ConsoleColor.DarkCyan);
				table.AddFooter($"Заработано на текущий момент: {totalPrice}р", ConsoleColor.DarkCyan);
				table.AddFooter($"Прогресс: {progress}%", ConsoleColor.DarkCyan);
				table.AddFooter($"Прогресс отставания: {leftProgress}%", ConsoleColor.DarkRed);
				table.AddFooter($"Осталось заработать: {leftPrice}р", ConsoleColor.DarkRed);
				table.AddFooter($"Осталось отработать: {decimal.Round(leftSpentTime, 1)} часов", ConsoleColor.DarkRed);
				
				renderer.Render();
				
				var openIssuesQuantity = work.Issues.Count(c => c.State == StateType.Open);
				var doneIssuesQuantity = work.Issues.Count(c => c.State == StateType.Done);
				var inProgressIssuesQuantity = work.Issues.Count(c => c.State == StateType.InProgress);
				var verifyIssuesQuantity = work.Issues.Count(c => c.State == StateType.ToVerify);
				
				Console.WriteLine(new string('-', 30));
				Console.WriteLine($@"- {new DateTime(work.Year, work.Month, 1)
					.ToString("MMM", CultureInfo.InvariantCulture)} report");
				Console.WriteLine($"- Total issues: {work.Issues.Length}");
				Console.WriteLine($"- Open: {openIssuesQuantity}");
				Console.WriteLine($"- In Progress: {inProgressIssuesQuantity}");
				Console.WriteLine($"- To Verify: {verifyIssuesQuantity}");
				Console.WriteLine($"- Done: {doneIssuesQuantity}");
			}
		}

		public void ToCsv(ICollection<Issue> ytIssues, string filePath) => TableImporter.ToCsv(Salary, ytIssues, filePath);
		public void ToCsvLastTable(ICollection<Issue> ytIssues, string filePath) => TableImporter.ToCsvByLastTable(Salary, ytIssues, filePath);
		public void ToListFile(ICollection<Issue> ytIssues, string filePath) => TableImporter.ToList(Salary, ytIssues, filePath);
	}
}