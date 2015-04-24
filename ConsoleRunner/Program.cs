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
            var processLoad = new ProcessLoad();
            var runtime = 9;
            var schedulers = new List<Scheduler>
            {
                new Feedback(processLoad),
                new FirstComeFirstServed(processLoad),
                new RoundRobin(processLoad, runtime),
                new ShortestProcessTime(processLoad),
                new ShortestRemainingTime(processLoad),
            };


        }
    }
}
