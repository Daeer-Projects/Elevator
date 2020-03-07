using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
	public class ElevatorStatus : IElevatorStatus
	{
		private readonly Dictionary<int, Floor> _floorQueue;

		public ElevatorStatus()
		{
			_floorQueue = new Dictionary<int, Floor>();
			Riders = new ConcurrentBag<Rider>();
		}


		public Direction CurrentDirection { get; set; }

		public Floor CurrentDestination { get; set; }


		public Floor CurrentFloor { get; set; }

		public ConcurrentBag<Rider> Riders { get; set; }

		public Floor GetIdleFloor()
		{
			return Riders.Count > 0 ? Floor.Six : Floor.Ground;
		}

		public void Enqueue(Floor floor)
		{
			// So what is happening here?

			if (!_floorQueue.Any())
			{
				_floorQueue.Add(1, floor);
			}
		}

		public void MoveToNextFloor()
		{

			switch (CurrentDirection)
			{
				case Direction.Up:
					CurrentFloor -= 1;
					break;
				case Direction.Down:
					CurrentFloor += 1;
					break;
				default:
					CurrentFloor = CurrentFloor;
					break;
			}
		}
	}
}
