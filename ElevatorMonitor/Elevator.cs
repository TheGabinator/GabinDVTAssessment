using System;
using static ElevatorMonitor.Constants;

namespace ElevatorMonitor
{
	public class Elevator
	{
		public string ElevatorName { get; set; }
		public int CurrentFloorNumber { get; set; }
		public bool Moving { get; set; } = false;
		public string Direction { get; set; } = Constants.Direction.Up;
        public int MaxPeopleAllowed { get; set; }
		public int MaxFloors { get; set; }
		public List<Person> PeopleOnBoard { get; set; }
		private List<Person> PeopleBoarding = new List<Person>();

        public async Task Move()
		{
			while(true)
			{
				await Start();
				
				await Stop();
                if (PeopleOnBoard.Count() == 0)
				{
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.WriteLine("Elevator is empty press any key to exit");
					//Console.ReadLine();
					break;
                }   
            }
		}

		public Task Stop()
		{
			var task = Task.Run(() =>
			{
				//Setting the end destination for upward direction
				int destination = 0;
				if (Direction == Constants.Direction.Up)
					destination = PeopleOnBoard.Max(peeps => peeps.TargetFloor);

                //Setting the end destination for upward direction
                if (Direction == Constants.Direction.Down)
					destination = PeopleOnBoard.Min(peeps => peeps.TargetFloor);

				//Set direction as stop if elevator is standing still
				if (destination == CurrentFloorNumber)
					Direction = Constants.Direction.Stop;

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Elevator moving to the next floor, it takes 3 seconds to move between floors, please wait...\n");

				//The delay simulating the time taken for the elevator to move between floors
                Thread.Sleep(3000);
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine($"Status of {this.ElevatorName}: Currently on Floor: {this.CurrentFloorNumber}; destination Floor: {destination}; Motion: {this.Direction}; People aboard: {PeopleOnBoard.Count()}");

				//remove people leaving the elevator from the list 
                int numberOfPeopleLeaving = PeopleOnBoard.RemoveAll(p=> p.TargetFloor == CurrentFloorNumber);

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"{numberOfPeopleLeaving} person(s) left the elvator");

				//The following portion controls the direction of the elevator
				//Complete the trip in one direction then carter for for people going the opposite direction
				if(Direction == Constants.Direction.Up)
				{
					if (PeopleOnBoard.Any(peeps => peeps.TargetFloor > CurrentFloorNumber))
						return;
                }

				Direction = Constants.Direction.Down;

                if (Direction == Constants.Direction.Down)
                {
                    if (PeopleOnBoard.Any(peeps => peeps.TargetFloor < CurrentFloorNumber))
                        return;
                }

                Direction = Constants.Direction.Up;
            });

			return task;
        }

		public Task Start( )
		{
			var task = Task.Run(() =>
			{
				AddPeople();

				//Movement of the elevator from floor to floor
                if (Direction == Constants.Direction.Up && this.CurrentFloorNumber < this.MaxFloors)
                    this.CurrentFloorNumber++;

                if (Direction == Constants.Direction.Down && this.CurrentFloorNumber > 0)
                    this.CurrentFloorNumber--;

				PeopleOnBoard.AddRange(PeopleBoarding);
				PeopleBoarding.Clear();
            });

			return task;
        }

		//Methode to add people to the list of people on board the elevator
		public void AddPeople()
		{
			string userStatus = "done";
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Would you like to add more people to the elevator from this Floor? y/n");
			string addNewPerson = Console.ReadLine() ?? "no";
			if (addNewPerson.ToLower() != Constants.YesNo.Y)
				return;

            while (true)
			{
				var newPeople = ExtensionMethods.NewPerson();
				if(newPeople is null)
				{
					Console.WriteLine("\nNo new person was created please try again");
					continue;
				}

                if (PeopleOnBoard.Count() + newPeople.Count() > MaxPeopleAllowed)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("\nThe max weight of 10 is exceeded, please reduce the number of people boarding");
                    Console.WriteLine($"Please note the current weight is {PeopleOnBoard.Count()} only {10 - PeopleOnBoard.Count()} space left\n");
                    Console.WriteLine($"Please try again with the recommended numbers");
                    continue;
                }

                PeopleBoarding.AddRange(newPeople);

                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Type \"add\" to add more people and press enter or just press enter to continue...");

				userStatus = Console.ReadLine() ?? "done";

				if(userStatus.ToLower() != "add" )
				{
					break;
				}
            }
		}
    }

}

