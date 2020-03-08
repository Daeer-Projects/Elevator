using System.Threading;

using Easy.MessageHub;
using Interfaces.Messages;
using NSubstitute;
using Xunit;

namespace Interfaces.Tests
{
	public class ElevatorControlsTests
	{
		private IElevatorControls _elevatorControls;
		private IElevatorStatus _elevatorStatus;
		private IMessageHub _messageHub;

		public ElevatorControlsTests()
		{
			_elevatorStatus = Substitute.For<IElevatorStatus>();
			_messageHub = Substitute.For<IMessageHub>();
			_elevatorControls = new ElevatorControls(_messageHub, _elevatorStatus);
		}

		[Fact]
		public void Test_call_lift_then_calls_status()
		{
			// Arrange.
			// Act.
			_elevatorControls.CallElevator(Floor.Five, Direction.Up);

			// Assert.
			_elevatorStatus.Received(1).AddCall(Floor.Five);
		}

		[Fact]
		public void Test_select_destination_then_calls_status()
		{
			// Arrange.
			// Act.
			_elevatorControls.SelectDestination(Floor.Five);

			// Assert.
			_elevatorStatus.Received(1).AddDestination(Floor.Five);
		}

		[Fact]
		public void Test_message_is_called_with_floor_change()
		{
			// Arrange.
			_elevatorStatus.CurrentFloor.Returns(Floor.Five);
			_elevatorStatus.CurrentDirection.Returns(Direction.Up);

			// Act.
			_elevatorControls.CallElevator(Floor.Five, Direction.Up);

			// Pause for the lift to move between floors - four seconds.
			Thread.Sleep(4000);

			// Assert.
			_messageHub.Received(1).Publish(Arg.Any<FloorChangedMessage>());
		}
	}
}
