using System;

namespace Watchy.WorkerService.Helpers
{
	public class TimerInterval
	{
		private int _amount;

		public int Amount { get => _amount; }

		public TimerInterval(int amount = 0)
		{
			_amount = amount;
		}

		public TimerInterval AddSeconds(int amount)
		{
			int ms = 1000;
			int amountToAdd = ms * amount;
			_amount += Math.Abs(amountToAdd);
			return this;
		}

		public TimerInterval AddMinutes(int amount)
		{
			return AddSeconds(60 * amount);
		}

		public TimerInterval AddHours(int amount)
		{
			return AddMinutes(60 * amount);
		}
	}
}
