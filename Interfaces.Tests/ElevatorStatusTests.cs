using FluentAssertions;
using Xunit;

namespace Interfaces.Tests
{
	public class ElevatorStatusTests
	{
		private readonly IElevatorStatus _elevatorStatus;

		public ElevatorStatusTests()
		{
			_elevatorStatus = new ElevatorStatus();
		}

		[Fact]
		public void Test_move_next_with_none_direction_and_calls_above_sets_direction_to_up()
		{
			// Arrange.
			var expectedDirection = Direction.Up;
			_elevatorStatus.CurrentDirection = Direction.None;
			_elevatorStatus.AddCall(Floor.Four);

			// Act.
			_elevatorStatus.MoveToNextFloor();

			// Assert.
			_elevatorStatus.CurrentDirection.Should().Be(expectedDirection);
		}

		[Fact]
		public void Test_move_next_with_up_direction_and_current_floor_is_max_sets_direction_to_down()
		{
			// Arrange.
			var expectedDirection = Direction.Down;
			_elevatorStatus.CurrentDirection = Direction.Up;
			_elevatorStatus.AddCall(Floor.Four);
			_elevatorStatus.CurrentFloor = Floor.Twelve;

			// Act.
			_elevatorStatus.MoveToNextFloor();

			// Assert.
			_elevatorStatus.CurrentDirection.Should().Be(expectedDirection);
		}

		[Fact]
		public void Test_move_next_with_down_direction_and_current_floor_is_ground_sets_direction_to_up()
		{
			// Arrange.
			var expectedDirection = Direction.Up;
			_elevatorStatus.CurrentDirection = Direction.Down;
			_elevatorStatus.AddCall(Floor.Four);
			_elevatorStatus.CurrentFloor = Floor.Ground;

			// Act.
			_elevatorStatus.MoveToNextFloor();

			// Assert.
			_elevatorStatus.CurrentDirection.Should().Be(expectedDirection);
		}

		[Fact]
		public void Test_move_next_with_up_direction_and_current_floor_eight_and_destination_below_sets_direction_to_down()
		{
			// Arrange.
			var expectedDirection = Direction.Down;
			_elevatorStatus.CurrentDirection = Direction.Up;
			_elevatorStatus.AddDestination(Floor.Four);
			_elevatorStatus.CurrentFloor = Floor.Eight;

			// Act.
			_elevatorStatus.MoveToNextFloor();

			// Assert.
			_elevatorStatus.CurrentDirection.Should().Be(expectedDirection);
		}

		[Fact]
		public void Test_move_next_with_down_direction_and_current_floor_six_and_destination_above_sets_direction_to_up()
		{
			// Arrange.
			var expectedDirection = Direction.Up;
			_elevatorStatus.CurrentDirection = Direction.Down;
			_elevatorStatus.AddDestination(Floor.Eleven);
			_elevatorStatus.CurrentFloor = Floor.Six;

			// Act.
			_elevatorStatus.MoveToNextFloor();

			// Assert.
			_elevatorStatus.CurrentDirection.Should().Be(expectedDirection);
		}

		[Fact]
		public void Test_move_next_with_up_direction_and_current_floor_six_sets_current_floor_to_seven()
		{
			// Arrange.
			var expectedFloor = Floor.Seven;
			_elevatorStatus.CurrentDirection = Direction.Up;
			_elevatorStatus.AddDestination(Floor.Eleven);
			_elevatorStatus.CurrentFloor = Floor.Six;

			// Act.
			_elevatorStatus.MoveToNextFloor();

			// Assert.
			_elevatorStatus.CurrentFloor.Should().Be(expectedFloor);
		}

		[Fact]
		public void Test_move_next_with_down_direction_and_current_floor_six_sets_current_floor_to_five()
		{
			// Arrange.
			var expectedFloor = Floor.Five;
			_elevatorStatus.CurrentDirection = Direction.Down;
			_elevatorStatus.AddDestination(Floor.Two);
			_elevatorStatus.CurrentFloor = Floor.Six;

			// Act.
			_elevatorStatus.MoveToNextFloor();

			// Assert.
			_elevatorStatus.CurrentFloor.Should().Be(expectedFloor);
		}
		
		[Fact]
		public void Test_move_next_with_none_direction_and_no_calls_keeps_direction_as_none()
		{
			// Arrange.
			var expectedDirection = Direction.None;
			_elevatorStatus.CurrentDirection = Direction.None;

			// Act.
			_elevatorStatus.MoveToNextFloor();

			// Assert.
			_elevatorStatus.CurrentDirection.Should().Be(expectedDirection);
		}
	}
}
