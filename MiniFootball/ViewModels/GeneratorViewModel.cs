using System.Collections.Generic;
using MiniFootball.Models;

namespace MiniFootball.ViewModels
{
	public class GeneratorViewModel
	{
		public List<CheckedPlayers> CheckedPlayers { get; set; }

		public GeneratorViewModel()
		{
			CheckedPlayers = new List<CheckedPlayers>();
		}
	}
}