namespace Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class RoundRobin : Scheduler
    {
        public RoundRobin(ProcessLoad processLoad, int runtime) : base(processLoad)
        {
            this.ProcessesRunning = new Queue<Process>();
            this.ProcessesToRun = new Queue<Process>(processLoad.Processes.OrderBy(p => p.ArrivalTime));
            this.ProcessRunTime = runtime;
        }

        public Queue<Process> ProcessesToRun { get; set; }
        public Queue<Process> ProcessesRunning { get; set; }
        private int ProcessRunTime { get; set; }
        private Process CurrentProcess { get; set; } 
        private int CurrentProcessFinishTime { get; set; }


        public override Process GetProcessToRun(int currentTime)
        {
            while(this.ProcessesToRun.Any() && this.ProcessesToRun.Peek().ArrivalTime <= currentTime)
            {
                this.ProcessesRunning.Enqueue(this.ProcessesToRun.Dequeue());
            }
            
            // Null check?
            if (null != this.CurrentProcess)
            {
                if (this.CurrentProcess.CurrentBurstCycle.CpuBurstIsComplete)
                {
                    this.CurrentProcess = (this.ProcessesRunning.Any()) ? this.ProcessesRunning.Dequeue() : null;
                    this.CurrentProcessFinishTime = currentTime + this.ProcessRunTime;
                }
                else if (currentTime >= this.CurrentProcessFinishTime)
                {
                    this.ProcessesRunning.Enqueue(this.CurrentProcess);
                    this.CurrentProcess = this.ProcessesRunning.Dequeue();
                    this.CurrentProcessFinishTime = currentTime + this.ProcessRunTime;
                }
            }
            else
            {
                if (this.ProcessesRunning.Any())
                {
                    this.CurrentProcess = this.ProcessesRunning.Dequeue();
                    this.CurrentProcessFinishTime = currentTime + this.ProcessRunTime;
                }
                else
                {
                    this.CurrentProcess = null;
                }    
            }

            return this.CurrentProcess;
        }

    }
}
