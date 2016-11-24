using System;
using System.Collections.Generic;
using System.Linq;
using MiniFootball.Models;

namespace MiniFootball.Helpers
{
	public static class TeamGenerator
	{
		public static Dictionary<int, List<Statistic>> Generate(List<Statistic> sortedPlayers)
		{
			if (sortedPlayers.Count <= 10)
			{
				return RandomizeTeams(sortedPlayers, 2);
			}

			if (sortedPlayers.Count <= 14 && sortedPlayers.Count >= 11)
			{
				return RandomizeTeams(sortedPlayers, 3);
			}

			if (sortedPlayers.Count <= 19 && sortedPlayers.Count >= 15)
			{
				return RandomizeTeams(sortedPlayers, 4);
			}

			return null;
		}

		private static Dictionary<int, List<Statistic>> RandomizeTeams(IReadOnlyCollection<Statistic> sortedPlayers, int teamsAmount)
		{
			var teams = new Dictionary<int, List<Statistic>>();

			for (var i = 1; i < teamsAmount + 1; i++)
			{
				teams.Add(i, new List<Statistic>());
			}

			var rnd = new Random();

			var amountOfBrackets = sortedPlayers.Count / teamsAmount;

			if (sortedPlayers.Count % teamsAmount != 0)
			{
				amountOfBrackets++;
			}

			for (var i = 0; i < amountOfBrackets; i++)
			{
				var bracket = sortedPlayers.Skip(i * teamsAmount).Take(teamsAmount).ToList();

				for (var j = 1; j < teamsAmount + 1; j++)
				{
					if (!bracket.Any()) continue;
					var randomPlayerIndex = rnd.Next(bracket.Count);
					var randomPlayer = bracket[randomPlayerIndex];
					teams[j].Add(randomPlayer);
					bracket.Remove(randomPlayer);
				}
			}

			return teams;
		}
	}
}