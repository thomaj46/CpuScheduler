namespace Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Feedback : Scheduler
    {
        public Feedback(ProcessLoad processLoad) : base(processLoad) { }

        public override Process GetProcessToRun(int currentTime)
        {
            return this
                .ProcessLoad
                .Processes
                .Where(p => !p.IsCompleted)
                .Where(p => !p.CurrentBurstCycle.CpuBurstIsComplete)
                .Where(p => p.ArrivalTime <= currentTime)
                .OrderBy(p => p.ArrivalTime)
                .FirstOrDefault();
        }
    }
}
