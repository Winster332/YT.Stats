using System;
using System.Linq;

namespace YT.Stats.YT.Table
{
	public class TableRenderer
	{
		public Table Table { get; }
		
		public TableRenderer(Table table)
		{
			Table = table;
		}

		public void Render()
		{
			var head = string.Join("\t", Table.Header.Rows.FirstOrDefault().Cells.Select(c => c.Value));
			Console.WriteLine(head);
			Console.WriteLine(new string('-', 120));
			
			foreach (var row in Table.Body.Rows)
			{
				for (var i = 0; i < row.Cells.Count; i++)
				{
					if (row.Cells[i].Value.Length > 35)
					{
						row.Cells[i].Value = $"{row.Cells[i].Value.Substring(0, 35)}...";
					}
				}

				var prevColor = Console.ForegroundColor;
				foreach (var cell in row.Cells)
				{
					Console.ForegroundColor = cell.Color;
					Console.Write($"{cell.Value}\t");
					Console.ForegroundColor = prevColor;
				}
				Console.WriteLine();

				// var body = string.Join("\t", row.Cells.Select(c => c.Value));
				// Console.WriteLine(body);
			}
			
			Console.WriteLine(new string('-', 120));
			
			var prevColorFooter = Console.ForegroundColor;
			foreach (var cell in Table.Footer)
			{
				Console.ForegroundColor = cell.Color;
				Console.Write($"{cell.Value}\t");
				Console.ForegroundColor = prevColorFooter;
				Console.WriteLine();
			}
		}
	}
}