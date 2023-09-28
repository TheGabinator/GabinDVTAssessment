using System;
namespace ElevatorMonitor
{
	public static class ExtensionMethods
	{
        //Method to get input from console to navigate through the simulation
		public static List<Person> NewPerson(bool caller = false)
		{
            string personName = "";
            int personCurrentFloor = 0;
            int personTargetFloor = 0;
            int numberOfPeopleBoarding = 0;
           
            if(caller)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Please type in the current floor as a number between 0 and 10 inclusively");
                Console.Write("Current floor: ");
                if (!Int32.TryParse(Console.ReadLine(), out personCurrentFloor))
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Please ensure the floor number is a number less than 11 and greater than -1");
                    return null;
                }
            }

            //if personCurrentFloor floor is beyond limits infor user
            if (personCurrentFloor > 10 || personCurrentFloor < 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Please ensure the current floor number is a number less than 11 and greater than -1");
                return null;
            }

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Please type in the target floor as a number between 0 and 10 inclusively");
            Console.Write("Target floor: ");
            if (!Int32.TryParse(Console.ReadLine(), out personTargetFloor))
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Please ensure that the target floor is numeric value less than 10");
                return null;
            }

            //if target floor is beyond limits infor user
            if (personTargetFloor > 10 || personTargetFloor < 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Please ensure the target floor number is a number less than 11 and greater than -1");
                return null;
            }

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Please type in the number of people boarding");
            Console.Write("number of people boarding: ");
            if (!Int32.TryParse(Console.ReadLine(), out numberOfPeopleBoarding))
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Please ensure is a number between 0 and 10 inclusively");
                return null;
            }

            return AddPeopleBoardingElevator(personCurrentFloor, personTargetFloor, numberOfPeopleBoarding);
            

        }

        public static List<Person> AddPeopleBoardingElevator(int currentFloor, int targetFloor, int numberOfPeopleBoarding)
        {
            List<Person> people = new List<Person>();
            for (int x = 0; x < numberOfPeopleBoarding; x++)
            {
                people.Add(new Person { CurrentFloor = currentFloor, TargetFloor = targetFloor });
            }
            return people;
            //Testing
        }
    }
}

