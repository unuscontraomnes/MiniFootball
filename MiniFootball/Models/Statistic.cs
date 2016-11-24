namespace MiniFootball.Models
{
	public class Statistic
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string LastName { get; set; }
		public int Games { get; set; }
		public int Points { get; set; }
		public decimal Rating { get; set; }
	}
}