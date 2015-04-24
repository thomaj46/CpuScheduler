namespace Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ShortestRemainingTime : Scheduler
    {
        public ShortestRemainingTime(ProcessLoad processLoad)
            : base(processLoad)
        {
            this.ProcessesRunning = new List<Process>();
        }

        public List<Process> ProcessesRunning { get; set; }
        private Process CurrentProcess { get; set; } 

        public override Process GetProcessToRun(int currentTime)
        {
            foreach (var process in this.ProcessLoad.Processes.Where(p => p.ArrivalTime == currentTime))
            {
                this.ProcessesRunning.Add(process);
            }

            if (null != this.CurrentProcess && this.CurrentProcess.IsCompleted)
            {
                this.ProcessesRunning.Remove(this.CurrentProcess);
            }

            this.CurrentProcess = this.ProcessesRunning
                .Where(p => !p.CurrentBurstCycle.CpuBurstIsComplete)
                .OrderBy(p => p.CurrentBurstCycle.CpuBurstTimeRemaining)
                .FirstOrDefault();

            return this.CurrentProcess;
        }

    }
}
