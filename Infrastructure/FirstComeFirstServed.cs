namespace Infrastructure
{
    using System.Collections.Generic;
    using System.Linq;

    public class FirstComeFirstServed : Scheduler
    {
        public FirstComeFirstServed(ProcessLoad processLoad) : base(processLoad) { }

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
