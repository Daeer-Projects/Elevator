using System.Collections.Generic;

using FluentAssertions;
using Interfaces.Extensions;
using Xunit;

namespace Interfaces.Tests.Extensions
{
	public class QueueExtensionsTests
	{
		[Fact]
		public void Test_organise_with_call_queue_empty_destination_queue_ground_floor_going_up_returns_expected()
		{
			// Arrange.
			var expectedQueue = new Queue<Floor>();
			expectedQueue.Enqueue(Floor.Four);
			expectedQueue.Enqueue(Floor.Six);

			var callQueue = new Queue<Floor>();
			callQueue.Enqueue(Floor.Four);
			callQueue.Enqueue(Floor.Six);

			var directionQueue = new Queue<Floor>();

			const Floor currentFloor = Floor.Ground;
			const Direction direction = Direction.Up;

			// Act.
			var actualQueue = callQueue.OrganiseQueue(directionQueue, currentFloor, direction);

			// Assert.
			actualQueue.Should().Equal(expectedQueue);
		}

		[Fact]
		public void Test_organise_with_call_queue_empty_destination_queue_twelfth_floor_going_down_returns_expected()
		{
			// Arrange.
			var expectedQueue = new Queue<Floor>();
			expectedQueue.Enqueue(Floor.Six);
			expectedQueue.Enqueue(Floor.Four);

			var callQueue = new Queue<Floor>();
			callQueue.Enqueue(Floor.Four);
			callQueue.Enqueue(Floor.Six);

			var directionQueue = new Queue<Floor>();

			const Floor currentFloor = Floor.Twelve;
			const Direction direction = Direction.Down;

			// Act.
			var actualQueue = callQueue.OrganiseQueue(directionQueue, currentFloor, direction);

			// Assert.
			actualQueue.Should().Equal(expectedQueue);
		}

		[Fact]
		public void Test_organise_with_call_queue_destination_queue_ground_floor_going_up_returns_expected()
		{
			// Arrange.
			var expectedQueue = new Queue<Floor>();
			expectedQueue.Enqueue(Floor.Four);
			expectedQueue.Enqueue(Floor.Six);
			expectedQueue.Enqueue(Floor.Eight);
			expectedQueue.Enqueue(Floor.Eleven);

			var callQueue = new Queue<Floor>();
			callQueue.Enqueue(Floor.Four);
			callQueue.Enqueue(Floor.Six);

			var directionQueue = new Queue<Floor>();
			directionQueue.Enqueue(Floor.Eleven);
			directionQueue.Enqueue(Floor.Eight);

			const Floor currentFloor = Floor.Ground;
			const Direction direction = Direction.Up;

			// Act.
			var actualQueue = callQueue.OrganiseQueue(directionQueue, currentFloor, direction);

			// Assert.
			actualQueue.Should().Equal(expectedQueue);
		}

		[Fact]
		public void Test_organise_with_call_queue_destination_queue_twelfth_floor_going_down_returns_expected()
		{
			// Arrange.
			var expectedQueue = new Queue<Floor>();
			expectedQueue.Enqueue(Floor.Eleven);
			expectedQueue.Enqueue(Floor.Eight);
			expectedQueue.Enqueue(Floor.Six);
			expectedQueue.Enqueue(Floor.Four);

			var callQueue = new Queue<Floor>();
			callQueue.Enqueue(Floor.Four);
			callQueue.Enqueue(Floor.Six);

			var directionQueue = new Queue<Floor>();
			directionQueue.Enqueue(Floor.Eleven);
			directionQueue.Enqueue(Floor.Eight);

			const Floor currentFloor = Floor.Twelve;
			const Direction direction = Direction.Down;

			// Act.
			var actualQueue = callQueue.OrganiseQueue(directionQueue, currentFloor, direction);

			// Assert.
			actualQueue.Should().Equal(expectedQueue);
		}

		[Fact]
		public void Test_organise_with_call_queue_destination_queue_fifth_floor_going_up_returns_expected()
		{
			// Arrange.
			var expectedQueue = new Queue<Floor>();
			expectedQueue.Enqueue(Floor.Six);
			expectedQueue.Enqueue(Floor.Eight);
			expectedQueue.Enqueue(Floor.Eleven);
			expectedQueue.Enqueue(Floor.Four);
			expectedQueue.Enqueue(Floor.One);

			var callQueue = new Queue<Floor>();
			callQueue.Enqueue(Floor.Four);
			callQueue.Enqueue(Floor.Six);
			callQueue.Enqueue(Floor.One);

			var directionQueue = new Queue<Floor>();
			directionQueue.Enqueue(Floor.Eleven);
			directionQueue.Enqueue(Floor.Eight);

			const Floor currentFloor = Floor.Five;
			const Direction direction = Direction.Up;

			// Act.
			var actualQueue = callQueue.OrganiseQueue(directionQueue, currentFloor, direction);

			// Assert.
			actualQueue.Should().Equal(expectedQueue);
		}

		[Fact]
		public void Test_organise_with_call_queue_destination_queue_fifth_floor_going_down_returns_expected()
		{
			// Arrange.
			var expectedQueue = new Queue<Floor>();
			expectedQueue.Enqueue(Floor.Four);
			expectedQueue.Enqueue(Floor.One);
			expectedQueue.Enqueue(Floor.Six);
			expectedQueue.Enqueue(Floor.Eight);
			expectedQueue.Enqueue(Floor.Eleven);

			var callQueue = new Queue<Floor>();
			callQueue.Enqueue(Floor.Four);
			callQueue.Enqueue(Floor.Six);
			callQueue.Enqueue(Floor.One);

			var directionQueue = new Queue<Floor>();
			directionQueue.Enqueue(Floor.Eleven);
			directionQueue.Enqueue(Floor.Eight);

			const Floor currentFloor = Floor.Five;
			const Direction direction = Direction.Down;

			// Act.
			var actualQueue = callQueue.OrganiseQueue(directionQueue, currentFloor, direction);

			// Assert.
			actualQueue.Should().Equal(expectedQueue);
		}
	}
}
