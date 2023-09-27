using System;
namespace ElevatorMonitor
{
    public class MonitoringUnitService : IMonitoringUnitService
    {

        public MonitoringUnitService()
        {
        }

        public async Task initiate()
        {

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Hello DVT, welcome to our elevator simulator, enjoy the ride.\n");

            //The list Elevators being used
            List<Elevator> elevators = new List<Elevator>
            {
                new Elevator()
                {   MaxFloors = 10,
                    CurrentFloorNumber = 10,
                    ElevatorName = "Elevator1",
                    MaxPeopleAllowed = 10,
                    Direction = Constants.Direction.Down,
                    PeopleOnBoard = new List<Person>()
                },
                    new Elevator()
                {   MaxFloors = 10,
                    CurrentFloorNumber = 0,
                    ElevatorName = "Elevator2",
                    MaxPeopleAllowed = 10,
                    Direction = Constants.Direction.Up,
                    PeopleOnBoard = new List<Person>()
                },
                    new Elevator()
                {   MaxFloors = 10,
                    CurrentFloorNumber = 5,
                    ElevatorName = "Elevator3",
                    MaxPeopleAllowed = 10,
                    Direction = Constants.Direction.Up,
                    PeopleOnBoard = new List<Person>()
                },
            };


            //The building instance that manages elevators
            Building newBuilding = new Building()
            {
                BuildingName = "Top Building",
                TotalNumberofFloors = 10,
                Elevators = elevators
            };

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Location of all elevators");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                var el1 = elevators.Where(el => el.ElevatorName == "Elevator1").FirstOrDefault();
                Console.Write($"{el1?.ElevatorName} is on Floor {el1?.CurrentFloorNumber}  ***  ");

                var el2 = elevators.Where(el => el.ElevatorName == "Elevator2").FirstOrDefault();
                Console.Write($"{el2?.ElevatorName} is on Floor {el2?.CurrentFloorNumber}  ***  ");

                var el3 = elevators.Where(el => el.ElevatorName == "Elevator3").FirstOrDefault();
                Console.Write($"{el3?.ElevatorName} is on Floor {el3?.CurrentFloorNumber}\n\n");

                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Would you like to call an elevator y/n");
                string callElevator = Console.ReadLine() ?? "no";
                if (callElevator.ToLower() != Constants.YesNo.Y)
                    continue;

                var newPeople = ExtensionMethods.NewPerson(true);
                if (newPeople is null)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("No new person was created, please try again\n");
                    continue;
                }

                if (newPeople.Count() == 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("No one entered the elevator, please add the number of people entering the elevator\n");
                }


                //Starting the elevator movement process
                await newBuilding.CallElevator(newPeople);
            }


        }
    }
}

