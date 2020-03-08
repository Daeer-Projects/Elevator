namespace Interfaces
{
	public interface IElevatorStatus
    {
	    Direction CurrentDirection { get; set; }
		Floor CurrentFloor { get; set; }
	    void AddCall(Floor floor);
	    void AddDestination(Floor floor);

	    void MoveToNextFloor();
    }
}
