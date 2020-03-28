using System;
using System.Collections.Generic;

namespace YT.Stats.YT.Table
{
	public class Table
	{
		public TableContent Header { get; set; }
		public TableContent Body { get; set; }
		public List<TableCell> Footer { get; set; }
		
		public Table()
		{
			Header = new TableContent();
			Body = new TableContent();
			Footer = new List<TableCell>();
		}

		public void AddBodyRow(params TableCell[] values)
		{
			var bodyRow = new TableRow();
			
			foreach (var value in values)
			{
				bodyRow.Cells.Add(value);
			}
			
			Body.Rows.Add(bodyRow);
		}

		public void AddHead(params string[] titles)
		{
			var headerRow = new TableRow();
			
			foreach (var title in titles)
			{
				headerRow.AddCell(title);
			}
			
			Header.Rows.Add(headerRow);
		}

		public void AddFooter(string value, ConsoleColor color = ConsoleColor.Gray)
		{
			Footer.Add(new TableCell(value, color));
		}
	}
}