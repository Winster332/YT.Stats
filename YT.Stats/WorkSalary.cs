using YT.Stats.Extensions;

namespace YT.Stats
{
	public class WorkSalary
	{
		public decimal Salary { get; }
		public int DayOfMonth { get; }
		public int WorkHourOfDay { get; }
		public int NeedWorkHours { get; }
		public decimal PriceOfDay { get; }
		public decimal PriceOfHour { get; }

		public WorkSalary(decimal salary)
		{
			Salary = salary.AsRouble();
			DayOfMonth = 20;
			WorkHourOfDay = 8;
			NeedWorkHours = WorkHourOfDay * DayOfMonth;
			PriceOfDay = (Salary / DayOfMonth).AsRouble();
			PriceOfHour = (PriceOfDay / WorkHourOfDay).AsRouble();
		}
	}
}