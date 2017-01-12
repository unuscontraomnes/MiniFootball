using System.Collections.Generic;
using MiniFootball.Models;

namespace MiniFootball.ViewModels
{
	public class StatsViewModel
	{
		public List<Statistic> Statistics { get; set; }

		public StatsViewModel()
		{
			Statistics = new List<Statistic>();
		}
	}
}