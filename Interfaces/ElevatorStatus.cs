using System.Collections.Generic;
using System.Linq;

using Interfaces.Extensions;

namespace Interfaces
{
	public class ElevatorStatus : IElevatorStatus
	{
		private readonly Queue<Floor> _callQueue;
		private readonly Queue<Floor> _destinationQueue;

		private Queue<Floor> _mainQueue;

		public ElevatorStatus()
		{
			_callQueue = new Queue<Floor>();
			_destinationQueue = new Queue<Floor>();
			_mainQueue = new Queue<Floor>();
			CurrentFloor = Floor.Ground;
		}

		public Direction CurrentDirection { get; set; }

		public Floor CurrentFloor { get; set; }

		public void AddCall(Floor floor)
		{
			_callQueue.Enqueue(floor);
		}

		public void AddDestination(Floor floor)
		{
			_destinationQueue.Enqueue(floor);
		}

		public void MoveToNextFloor()
		{
			SetCurrentDirectionIfAnyCalls();
			
			_mainQueue = _callQueue.OrganiseQueue(_destinationQueue, CurrentFloor, CurrentDirection);
			RemoveCallsOnCurrentFloor();

			switch (CurrentDirection)
			{
				case Direction.Up:
					{
						var anyCallsAbove = _mainQueue.Any(m => m > CurrentFloor);
						if (!anyCallsAbove || CurrentFloor == Floor.Twelve)
						{
							CurrentDirection = Direction.Down;
						}
						else
						{
							CurrentFloor += 1;
						}

						break;
					}
				case Direction.Down:
					{
						var anyCallsBelow = _mainQueue.Any(m => m < CurrentFloor);
						if (!anyCallsBelow || CurrentFloor == Floor.Ground)
						{
							CurrentDirection = Direction.Up;
						}
						else
						{
							CurrentFloor -= 1;
						}

						break;
					}
				default:
					{
						CurrentFloor = CurrentFloor;
						break;
					}
			}
		}

		private void RemoveCallsOnCurrentFloor()
		{
			var anyToRemove = _mainQueue.Any(q => q == CurrentFloor);
			if (anyToRemove)
			{
				while (_mainQueue.Any(q => q == CurrentFloor))
				{
					_mainQueue.Dequeue();
				}
			}
		}

		private void SetCurrentDirectionIfAnyCalls()
		{
			if (CurrentDirection == Direction.None)
			{
				if (_callQueue.Any())
				{
					_callQueue.OrderBy(c => c);
					var firstFloor = _callQueue.First();
					CurrentDirection = CurrentFloor > firstFloor ? Direction.Down : Direction.Up;
				}
			}
		}
	}
}
