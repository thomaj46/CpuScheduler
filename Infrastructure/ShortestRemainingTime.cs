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
            this.ProcessesToRun = new Queue<Process>(processLoad.Processes.OrderBy(p => p.ArrivalTime));
        }

        public Queue<Process> ProcessesToRun { get; set; }
        public List<Process> ProcessesRunning { get; set; }
        private Process CurrentProcess { get; set; } 

        public override Process GetProcessToRun(int currentTime)
        {
            while(this.ProcessesToRun.Any() && this.ProcessesToRun.Peek().ArrivalTime <= currentTime)
            {
                this.ProcessesRunning.Add(this.ProcessesToRun.Dequeue());
            }

            if (null != this.CurrentProcess && this.CurrentProcess.BurstCycles.All(c => c.CpuBurstIsComplete))
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
