namespace Infrastructure
{
    using System.Collections.Generic;
    using System.Linq;

    public class ShortestProcessTime : Scheduler
    {
        public ShortestProcessTime(ProcessLoad processLoad) : base(processLoad)
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

            if (null == this.CurrentProcess)
            {
                this.CurrentProcess = this.ProcessesRunning.OrderBy(p => p.CurrentBurstCycle.CpuBurstTime).FirstOrDefault();
                
            }
            else if (this.CurrentProcess.IsCompleted)
            {
                 this.ProcessesRunning.Remove(this.CurrentProcess);
                 this.CurrentProcess = this.ProcessesRunning.OrderBy(p => p.CurrentBurstCycle.CpuBurstTime).FirstOrDefault();
            } 
            else if (this.CurrentProcess.CurrentBurstCycle.CpuBurstIsComplete)
            {
                this.CurrentProcess = null;
            }

            return this.CurrentProcess;
        }
    }
}
