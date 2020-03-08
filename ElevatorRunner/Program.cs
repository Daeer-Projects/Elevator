using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Easy.MessageHub;
using Interfaces;
using Interfaces.Messages;
using Ninject;

namespace ElevatorRunner
{
	class Program
	{
		private static Floor currentFloor;
		private static IElevatorControls elevatorControls;
		private static IMessageHub messageHub;
		private static readonly List<Person> People = new List<Person>();

		static void Main()
		{
			Bootstrap();

			DrawInitialState();

			//some sample calls here. Feel free to add more
			People.Add(new Person { StartingFloor = Floor.Four, Destination = Floor.Twelve, DelayInSeconds = 0 });
			People.Add(new Person { StartingFloor = Floor.Eight, Destination = Floor.Eleven, DelayInSeconds = 10 });
			People.Add(new Person { StartingFloor = Floor.Ground, Destination = Floor.Twelve, DelayInSeconds = 15 });
			People.Add(new Person { StartingFloor = Floor.Eight, Destination = Floor.Ground, DelayInSeconds = 10 });

			//People.Add(new Person { StartingFloor = Floor.Two, Destination = Floor.Five, DelayInSeconds = 15 });
			//People.Add(new Person { StartingFloor = Floor.Eight, Destination = Floor.Ten, DelayInSeconds = 20 });
			//People.Add(new Person { StartingFloor = Floor.Ground, Destination = Floor.Nine, DelayInSeconds = 13 });
			//People.Add(new Person { StartingFloor = Floor.Eight, Destination = Floor.Ground, DelayInSeconds = 13 });

			TestElevator();

			Console.ReadLine();
		}

		private static void TestElevator()
		{
			foreach (Person person in People)
			{
				Task.Delay(person.DelayInSeconds * 1000).ContinueWith(x =>
				{
					person.Status = Status.Waiting;
					elevatorControls.CallElevator(person.StartingFloor, person.Direction);
					Log($"Calling Elevator to floor {person.StartingFloor} in direction {person.Direction}"); });
			}
		}

		private static void Bootstrap()
		{
			IKernel kernel = new StandardKernel();
			//Perform binding here
			//See https://github.com/ninject/Ninject/wiki/Dependency-Injection-With-Ninject for help
			//Hint: You may need to use Bind<A, B>() depending on your implementation
			//Hint: You may need .InSingletonScope()
			kernel.Bind<IMessageHub>().To<MessageHub>().InSingletonScope();
			kernel.Bind<IElevatorStatus>().To<ElevatorStatus>().InSingletonScope();
			kernel.Bind<IElevatorControls>().To<ElevatorControls>().InSingletonScope();

			messageHub = kernel.Get<IMessageHub>();
			elevatorControls = kernel.Get<IElevatorControls>();

			messageHub.Subscribe<FloorChangedMessage>(ElevatorStatusOnFloorChanged);
		}

		private static void DrawInitialState()
		{
			Console.WriteLine("Olmec elevator");
			Console.WriteLine(" G: [|]");
			for (int i = 1; i < 13; i++)
				Console.WriteLine($"{i.ToString(),2}:");
		}

		private static void ClearFloor(int floor) => DrawFloor(floor, "    ");

		private static void DrawElevatorAtFloor(int floor) => DrawFloor(floor, "[|]");

		private static void DrawFloor(int floor, string status)
		{
			Console.SetCursorPosition(0, floor + 1);
			if (floor == 0)
				Console.Write($" G: {status}");
			else
				Console.Write($"{floor.ToString(),2}: {status}");

			Console.SetCursorPosition(0, 14);
		}

		private static void ElevatorStatusOnFloorChanged(FloorChangedMessage message)
		{
			ClearFloor((int)currentFloor);
			currentFloor = message.Floor;
			DrawElevatorAtFloor((int)currentFloor);

			CheckForPeopleEnteringElevator(message.Floor, message.Direction);

			CheckForPeopleLeavingElevator(message.Floor);
		}

		private static void CheckForPeopleLeavingElevator(Floor floor)
		{
			IEnumerable<Person> peopleLeavingAtCurrentFloor =
				People.Where(x => x.Status == Status.Riding && x.Destination == currentFloor);

			foreach (Person person in peopleLeavingAtCurrentFloor)
			{
				person.Status = Status.Complete;
				Log($"Dropping off person at floor {floor}");
			}
		}

		private static void CheckForPeopleEnteringElevator(Floor floor, Direction direction)
		{
			IEnumerable<Person> peopleWaitingAtCurrentFloor = People.Where(x =>
				x.Status == Status.Waiting && x.StartingFloor == currentFloor &&
				(x.Direction == direction || direction == Direction.None));

			foreach (Person person in peopleWaitingAtCurrentFloor)
			{
				person.Status = Status.Riding;
				elevatorControls.SelectDestination(person.Destination);
				Log($"Picking up person at floor {floor}. Destination {person.Destination}");
			}
		}

		private static int lineNumber;
		private static int maxLogLines = 10;
		private static void Log(string s)
		{
			Console.SetCursorPosition(0, 16 + lineNumber);
			ClearCurrentConsoleLine();
			lineNumber++;
			Console.WriteLine(s);
			if (lineNumber == maxLogLines)
				lineNumber = 0;
		}

		private static void ClearCurrentConsoleLine()
		{
			int currentLineCursor = Console.CursorTop;
			Console.SetCursorPosition(0, Console.CursorTop);
			Console.Write(new string(' ', Console.WindowWidth));
			Console.SetCursorPosition(0, currentLineCursor);
		}
	}
}
