namespace ConsoleRunner
{
    using Infrastructure;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(150, 50);
            Console.WriteLine("Enter number of processes to simulate...");
            var totalProcesses = Convert.ToInt32(Console.ReadLine());
            var processLoad = ProcessLoad.Create(totalProcesses);
            var feedbackProcessLoad = processLoad.Clone();
            var firstComeFirstServedProcessLoad = processLoad.Clone();
            var roundRobinProcessLoad = processLoad.Clone();
            var shortestProcessTimeProcessLoad = processLoad.Clone();
            var shortestRemainingTimeProcessLoad = processLoad.Clone();

            var runtime = 9;
            var schedulers = new List<Scheduler>
            {
                //new Feedback(feedbackProcessLoad),
                new FirstComeFirstServed(firstComeFirstServedProcessLoad),
                new RoundRobin(roundRobinProcessLoad, runtime),
                new ShortestProcessTime(shortestProcessTimeProcessLoad),
                new ShortestRemainingTime(shortestRemainingTimeProcessLoad),
            };

            var processDispatcher = new ProcessDispatcher();
            var consoleOutput = new ConsoleOutput();
            foreach (var scheduler in schedulers)
            {
                var cpu = new Cpu();
                var io = new Io();
                processDispatcher.Start(scheduler, cpu, io);
                consoleOutput.PrintHistory(cpu.History, io.History);

            }

        }
    }
}
