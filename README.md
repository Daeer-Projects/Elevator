# Elevator

This is an exercise for an interview.

## Goal

The goal of this is exercise is to give us some code to talk about in a face to face interview. We're not looking for a specific approach or functionality. A more complete system doesn't mean it's better designed. There is no pass or fail criteria.

## Task

To write an implementation for an elevator to service requests.

* Create implementations for [`IElevatorStatus`](Interfaces/IElevatorStatus.cs) and [`IElevatorControls`](Interfaces/IElevatorControls.cs)
* Bind the implementations using Ninject (https://github.com/ninject/Ninject/wiki/Dependency-Injection-With-Ninject)

The elevator should now run using the existing code

* When running the console application the elevator should move between floors servicing requests - complete
* Add people to the [list](ElevatorRunner/Program.cs#L23) to test different scenarios - complete
* Modify the runner if required, but try to leave the existing code intact if possible - small changes made

The elevator is considered complete when it can move people from their initial floor to their desired floor

### Changes Made

I added a NuGet package that implements the Event Aggregator Pattern.  It makes it very easy to follow what is publishing the event, and what has subscribed to that event.

## Optional improvements / ideas

* An elevator can serve as many requests in one direction as it can before going the other way - complete
* An elevator can be called to any floor at any time, but it need not immediately service that request - complete
* Requests for the elevator to go in the opposite direction of travel for the elevator can be ignored until the elevator is traveling in that direction - complete
* Any other changes you'd make to the system
* Display waiting people on a floor and/or people in the elevator
