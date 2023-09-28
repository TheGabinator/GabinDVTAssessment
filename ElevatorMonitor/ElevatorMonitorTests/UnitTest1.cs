﻿using ElevatorMonitor;

namespace ElevatorMonitorTest;

public class Tests
{

    [SetUp]
    public void Setup()
    {
        
    }

    [Test]
    public void Test1()
    {
        List<Person> peopleCalling = ExtensionMethods.AddPeopleBoardingElevator(2, 4, 10);

        //Testing to ensure the object returned is not null
        Assert.IsNotNull(peopleCalling);

        //Testing to ensure user select a current floor is equal or less than 10
        Assert.LessOrEqual(peopleCalling.FirstOrDefault().CurrentFloor, 10);

        //Testing to ensure user select a current floor is equal or greater than 0
        Assert.GreaterOrEqual(peopleCalling.FirstOrDefault().CurrentFloor, 0);

        //Testing to ensure the user selects the target floor is equal or less than 10
        Assert.LessOrEqual(peopleCalling.FirstOrDefault().TargetFloor, 10);

        //Testing to ensure the user selects the target floor is equal or greater than 0
        Assert.GreaterOrEqual(peopleCalling.FirstOrDefault().TargetFloor, 0);

        //Testing to ensure the the number of people boarding is not beyond stipulated limit
        Assert.LessOrEqual(peopleCalling.Count(), 10);
    }
}
