namespace Interfaces
{
	public class Rider
	{
		public int RiderNumber { get; set; }
		public Floor StartFloor { get; set; }
		public Floor DestinationFloor { get; set; }
		public bool IsFinished { get; set; }
	}
}
