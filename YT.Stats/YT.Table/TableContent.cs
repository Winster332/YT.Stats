using System.Collections.Generic;

namespace YT.Stats.YT.Table
{
	public class TableContent
	{
		public List<TableRow> Rows { get; set; }

		public TableContent()
		{
			Rows = new List<TableRow>();
		}
	}
}