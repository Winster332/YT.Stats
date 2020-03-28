namespace YT.Stats.Extensions
{
	public static class DecimalExtensions
	{
		public static decimal AsRouble(this decimal value) => decimal.Round(value, 1);
	}
}