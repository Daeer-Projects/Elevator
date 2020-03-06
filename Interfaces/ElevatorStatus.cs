using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
	public class ElevatorStatus : IElevatorStatus
	{
		public event Action<Floor, Direction> FloorChanged;
	}
}
