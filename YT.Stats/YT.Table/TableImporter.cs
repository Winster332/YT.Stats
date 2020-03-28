using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using YT.Stats.Extensions;
using YT.Stats.YT.Issue;

namespace YT.Stats.YT.Table
{
	public static class TableImporter
	{
		public static void ToCsvByLastTable(WorkSalary salary, ICollection<YouTrackSharp.Issues.Issue> ytIssues, string filePath)
		{
			Console.WriteLine($"Begin export to: {filePath}");
			
			var tables = BuildForExport(salary, ytIssues);
			var header = string.Join(',', tables
				.FirstOrDefault().Header.Rows
				.FirstOrDefault().Cells
				.Select(c => c.Value));
			var rows = string.Join('\n', tables
				.FirstOrDefault().Body.Rows
				.Select(r => string.Join(',', r.Cells.Select(c => c.Value))));
			
			SaveToFile(filePath, $"{header}\n{rows}");
			
			Console.WriteLine("Done");
		}
		
		public static void ToCsv(WorkSalary salary, ICollection<YouTrackSharp.Issues.Issue> ytIssues, string filePath)
		{
			Console.WriteLine($"Begin export to: {filePath}");
			
			var tables = BuildForExport(salary, ytIssues);
			var header = string.Join(',', tables.FirstOrDefault().Header.Rows.FirstOrDefault().Cells.Select(c => c.Value));
			var rows = string.Join('\n', tables
				.SelectMany(t => t.Body.Rows.Select(r => string.Join(',', r.Cells.Select(c => c.Value)))));
			
			SaveToFile(filePath, $"{header}\n{rows}");
			
			Console.WriteLine("Done");
		}
		
		public static void ToList(WorkSalary salary, ICollection<YouTrackSharp.Issues.Issue> ytIssues, string filePath)
		{
			Console.WriteLine($"Begin export to: {filePath}");
			
			var descriptionIndex = 8;
			var rows = BuildForExport(salary, ytIssues).FirstOrDefault()?.Body.Rows;
			var stringRows = new List<string>();

			for (var i = 0; i < rows.Count; i++)
			{
				var number = i + 1;
				var description = rows[i].Cells[descriptionIndex].Value.Replace("\t", "");
				stringRows.Add($"{number}. {description}");
			}

			var sourceFile = string.Join('\n', stringRows);
			SaveToFile(filePath, $"{sourceFile}");
			
			Console.WriteLine("Done");
		}
		
		private static List<Table> BuildForExport(WorkSalary salary, ICollection<YouTrackSharp.Issues.Issue> ytIssues)
		{
			var tables = new List<Table>();
			var works = ytIssues.GetWorksFromIssues();

			foreach (var work in works)
			{
				var table = new Table();
				
				table.AddHead("Id", "Type", "State", "Created", "Updated",
					"Reporter", "Spent", "Price", "Description");
				
				for (var i = 0; i < work.Issues.Length; i++)
				{
					var issue = work.Issues[i];
					var id = issue.Id;
					var type = issue.Type.ToString();
					var state = new TableCell($"{issue.State.ToString()}");

					if (issue.State == StateType.Done)
					{
						state.Color = ConsoleColor.Green;
					}
					if (issue.State == StateType.InProgress)
					{
						state.Color = ConsoleColor.Blue;
					}
					if (issue.State == StateType.ToVerify)
					{
						state.Color = ConsoleColor.Cyan;
					}
					if (issue.State == StateType.Open)
					{
						state.Color = ConsoleColor.Yellow;
					}
					
					var created = issue.CreatedDateTime.ToString("MM.dd.yyyy");
					var updated = issue.UpdatedDateTime.ToString("MM.dd.yyyy");
					var formName = issue.ReporterFullName.Split(" ");
					var reporter = $"{formName[0][0]}. {formName[1]}";

					var summary = issue.Summary;

					var spentTime = $"{decimal.Round((decimal) issue.SpentTime, 3)} h";
					var price = $"{(salary.PriceOfHour * issue.SpentTime).AsRouble()}р";

					table.AddBodyRow(new TableCell(id), 
						new TableCell(type), 
						state,
						new TableCell(created), 
						new TableCell(updated), 
						new TableCell(reporter), 
						new TableCell(spentTime), 
						new TableCell(price), 
						new TableCell(summary));
				}
				
				var totalSpentTime = work.Issues.Select(i => i.SpentTime).Sum();
				var totalPrice = (salary.PriceOfHour * totalSpentTime).AsRouble();
				var progress = decimal.Round((100m / salary.NeedWorkHours) * (decimal)totalSpentTime, 1);
				var leftProgress = 100m - progress;
				var leftPrice = (salary.Salary - totalPrice).AsRouble();
				var leftSpentTime = salary.NeedWorkHours - totalSpentTime;
				var footer = $"Spent time: {totalSpentTime}\nTotal price: {totalPrice}р\nNeed work hours: {salary.NeedWorkHours} h\nProgress: {progress}%\nLeft progress: {leftProgress}%\nLeft salary: {leftPrice}р\nLeft spent time: {leftSpentTime} h";
				table.AddFooter(footer);
				
				tables.Add(table);
			}

			return tables;
		}
		
		private static void SaveToFile(string path, string content)
		{
			if (File.Exists(path))
			{
				File.Delete(path);
			}

			using (var stream = new FileStream(path, FileMode.CreateNew))
			{
				using (var writer = new StreamWriter(stream))
				{
					writer.WriteLine(content);
				}
			}
		}
	}
}