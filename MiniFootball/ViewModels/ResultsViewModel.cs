using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MiniFootball.Models;

namespace MiniFootball.ViewModels
{
	public class ResultsViewModel
	{
		public List<Result> Results { get; set; }
		public bool IsActiveGame { get; set; }

		public ResultsViewModel()
		{
			Results = new List<Result>();
		}
	}
}