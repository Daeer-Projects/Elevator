using System;
using System.Threading.Tasks;
using Easy.MessageHub;
using Interfaces.Messages;

namespace Interfaces
{
	public	class ElevatorControls : IElevatorControls
	{
		private readonly IMessageHub _messageHub;
		private readonly IElevatorStatus _elevatorStatus;

		// How long does it take the lift to move between floors?
		private const int TimeBetweenFloors = 4;

		public ElevatorControls(IMessageHub hub, IElevatorStatus elevatorStatus)
		{
			_messageHub = hub;
			_elevatorStatus = elevatorStatus;
			Task.Run(MoveElevator);
		}

		public void CallElevator(Floor floor, Direction direction)
		{
			// The person is calling for the lift on floor with a direction.
			if (_elevatorStatus.CurrentFloor < floor && (_elevatorStatus.CurrentDirection == Direction.Up || _elevatorStatus.CurrentDirection == Direction.None))
			{
				// The lift is below the requested floor and the lift is moving up, or idle.
				_elevatorStatus.Enqueue(floor);
			}
		}

		public void SelectDestination(Floor floor)
		{
			// The person who has just got on, is setting their required destination.
			// Where is this stored?
			_elevatorStatus.Riders.Add(new Rider
			{
				RiderNumber = _elevatorStatus.Riders.Count + 1,
				DestinationFloor = floor,
				StartFloor = _elevatorStatus.CurrentFloor,
				IsFinished = false
			});
		}

		private async Task MoveElevator()
		{
			while (true)
			{
				await Task.Delay(TimeBetweenFloors * 1000).ConfigureAwait(false);
				await Task.Run(() => { _elevatorStatus.MoveToNextFloor(); }).ConfigureAwait(false);
				_messageHub.Publish(new FloorChangedMessage
				{
					Direction = _elevatorStatus.CurrentDirection,
					Floor = _elevatorStatus.CurrentFloor
				});
			}
		}
	}
}
