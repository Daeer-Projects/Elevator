using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IElevatorStatus
    {
	    Direction CurrentDirection { get; set; }
	    Floor CurrentDestination { get; set; }
		Floor CurrentFloor { get; set; }
	    ConcurrentBag<Rider> Riders { get; set; }
	    Floor GetIdleFloor();
	    void Enqueue(Floor floor);

	    void MoveToNextFloor();
    }
}
