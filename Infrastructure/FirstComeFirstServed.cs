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
                .OrderBy(p => p.ArrivalTime)
                .FirstOrDefault();
        }
    }
}
