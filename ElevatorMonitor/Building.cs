using System;
namespace ElevatorMonitor
{
	public class Building
	{
		

		public Building()
		{
		}

        public string BuildingName { get; set; }
        public int TotalNumberofFloors { get; set; }
        public List<Elevator> Elevators { get; set; }


		public void Monitor()
		{

		}

		public async Task CallElevator( List<Person> elevatorCallers)
		{
			//Elevators.Where(elev => elev.CurrentFloorNumber - elevatorCaller.CurrentFloor == 1);


            var dif = Elevators.Select(el => new { n = el, diff = Math.Abs(el.CurrentFloorNumber - elevatorCallers.First().CurrentFloor) }).ToList();

            var result = dif.Where(x => x.diff == dif.Min(x => x.diff)).FirstOrDefault();


            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"You are on floor number {elevatorCallers.First().CurrentFloor}");
			Console.WriteLine($"Closest Elevator is {result?.n.ElevatorName} on floor number {result?.n.CurrentFloorNumber}\n");

			if(result?.n.PeopleOnBoard.Count() + elevatorCallers.Count() > 10)
			{
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("The max weight of 10 is exceeded, please reduce the number of people boarding");
                Console.WriteLine($"Please note the current weight is {result?.n.PeopleOnBoard.Count()} only {10 - result?.n.PeopleOnBoard.Count()} space left");
                Console.WriteLine($"Please try again with the erecommended numbers");
                return;
			}

			result?.n.PeopleOnBoard.AddRange(elevatorCallers);
			result.n.CurrentFloorNumber = elevatorCallers.First().CurrentFloor;

			await result.n.Move();

            Console.ReadLine();
        }


    }
}

