namespace ConsoleRunner
{
    using Infrastructure;
    using System;
    using System.Collections.Generic;
    using System.IO;
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
            var feedbackProcessLoad = processLoad.DeepCopy();
            var firstComeFirstServedProcessLoad = processLoad.DeepCopy();
            var roundRobinProcessLoad = processLoad.DeepCopy();
            var shortestProcessTimeProcessLoad = processLoad.DeepCopy();
            var shortestRemainingTimeProcessLoad = processLoad.DeepCopy();

            var runtime = 9;
            var schedulers = new List<Scheduler>
            {
                new Feedback(feedbackProcessLoad),
                new FirstComeFirstServed(firstComeFirstServedProcessLoad),
                new RoundRobin(roundRobinProcessLoad, runtime),
                new ShortestProcessTime(shortestProcessTimeProcessLoad),
                new ShortestRemainingTime(shortestRemainingTimeProcessLoad),
            };

            var processDispatcher = new ProcessDispatcher();
            var consoleOutput = new ConsoleOutput();
            var stringBuilder = new StringBuilder();
            foreach (var scheduler in schedulers)
            {
                var cpu = new Cpu();
                var io = new Io();
                processDispatcher.Start(scheduler, cpu, io);
                var summary = scheduler.ProcessLoad.SummarizeOutput(cpu, io);
                stringBuilder.AppendLine(string.Format("Average Turnaround: {0}", summary.AverageTurnaroundTime));
                stringBuilder.AppendLine(string.Format("Average Wait: {0}", summary.AverageWaitTime));
                stringBuilder.AppendLine(string.Format("Cpu Utilization: {0}", summary.CpuUtilization));
                Console.WriteLine("Average Turnaround: {0}", summary.AverageTurnaroundTime);
                Console.WriteLine("Average Wait: {0}", summary.AverageWaitTime);
                Console.WriteLine("Cpu Utilization: {0}", summary.CpuUtilization);
                consoleOutput.PrintHistory(cpu.History, io.History);
                consoleOutput.PrintHistoryToStringBuilder(cpu.History, io.History, stringBuilder);
            }

            using(TextWriter writer = File.CreateText(@"c:\temp\output.txt"))
            {
                writer.Write(stringBuilder.ToString());
            }

        }
    }
}
