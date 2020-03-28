using System;

namespace YT.Stats.YT.Table
{
	public class TableCell
	{
		public string Value { get; set; }
		public ConsoleColor Color { get; set; }

		public TableCell(string value, ConsoleColor color = ConsoleColor.Gray)
		{
			Value = value;
			Color = color;
		}

		public TableCell()
		{
			Value = string.Empty;
			Color = ConsoleColor.White;
		}
	}
}