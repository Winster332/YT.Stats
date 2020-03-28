using System;
using System.Collections.Generic;

namespace YT.Stats.YT.Table
{
	public class TableRow
	{
		public List<TableCell> Cells { get; set; }

		public TableRow()
		{
			Cells = new List<TableCell>();
		}

		public void AddCell(string value, ConsoleColor color = ConsoleColor.White)
		{
			Cells.Add(new TableCell
			{
				Value = value,
				Color = color
			});
		}
	}
}