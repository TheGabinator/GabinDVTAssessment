namespace ElevatorMonitor;

class Program
{
    static async Task Main(string[] args)
    {
        IMonitoringUnitService monitoringUnitService = new MonitoringUnitService();
        await monitoringUnitService.initiate();

    }
}

