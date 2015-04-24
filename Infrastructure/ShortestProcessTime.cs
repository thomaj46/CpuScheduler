namespace Infrastructure
{
    using System.Collections.Generic;
    using System.Linq;

    public class ShortestProcessTime : Scheduler
    {
        public ShortestProcessTime(ProcessLoad processLoad) : base(processLoad)
        {
            this.ProcessesRunning = new List<Process>();
            this.ProcessesToRun = new Queue<Process>();

            foreach (var process in processLoad.Processes.OrderBy(p => p.ArrivalTime))
            {
                this.ProcessesToRun.Enqueue(process);
            }
        }

        public List<Process> ProcessesRunning { get; set; }
        private Process CurrentProcess { get; set; }
        public Queue<Process> ProcessesToRun { get; set; }

        public override Process GetProcessToRun(int currentTime)
        {
            while (this.ProcessesToRun.Any() && this.ProcessesToRun.Peek().ArrivalTime <= currentTime)
            {
                this.ProcessesRunning.Add(this.ProcessesToRun.Dequeue());

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
