using System.Collections.Generic;
using System.Linq;

namespace Interfaces.Extensions
{
	public static class QueueExtensions
	{
		public static Queue<Floor> OrganiseQueue(this Queue<Floor> callQueue,
			Queue<Floor> destinationQueue,
			Floor currentFloor,
			Direction direction)
		{
			Queue<Floor> result;
			var tempAscList = new List<Floor>();
			var tempDescList = new List<Floor>();

			if (direction == Direction.Up)
			{
				PopulateTempLists(callQueue, destinationQueue, currentFloor, tempAscList, tempDescList);

				var ascendingList = tempAscList.OrderBy(x => x).ToList();
				var descendingList = tempDescList.OrderByDescending(x => x).ToList();

				ascendingList.AddRange(descendingList);

				result = new Queue<Floor>(ascendingList);
			}
			else
			{
				PopulateTempLists(callQueue, destinationQueue, currentFloor, tempAscList, tempDescList);

				var ascendingList = tempAscList.OrderBy(x => x).ToList();
				var descendingList = tempDescList.OrderByDescending(x => x).ToList();

				descendingList.AddRange(ascendingList);

				result = new Queue<Floor>(descendingList);
			}

			return result;
		}

		private static void PopulateTempLists(Queue<Floor> callQueue,
			Queue<Floor> destinationQueue,
			Floor currentFloor,
			List<Floor> tempAscList,
			List<Floor> tempDescList)
		{
			var aboveCall = callQueue.Where(c => c > currentFloor).Select(c => c);
			var aboveDest = destinationQueue.Where(d => d > currentFloor).Select(d => d);
			var belowCall = callQueue.Where(c => c < currentFloor).Select(c => c);
			var belowDest = destinationQueue.Where(d => d < currentFloor).Select(d => d);

			tempAscList.AddRange(aboveCall);
			tempAscList.AddRange(aboveDest);
			tempDescList.AddRange(belowCall);
			tempDescList.AddRange(belowDest);
		}
	}
}