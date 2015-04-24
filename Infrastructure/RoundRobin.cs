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
            this.ProcessesToRun = new Queue<Process>();
            this.ProcessRunTime = runtime;

            foreach (var process in processLoad.Processes.OrderBy(p => p.ArrivalTime))
            {
                this.ProcessesToRun.Enqueue(process);
            }
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

            if (!this.ProcessesRunning.Any())
            {
                this.CurrentProcess = null;
            }
            else if (null == this.CurrentProcess)
            {
                this.CurrentProcess = this.ProcessesRunning.FirstOrDefault();
                this.CurrentProcessFinishTime = this.ProcessRunTime + currentTime;
            }
            else if (this.CurrentProcess.IsCompleted)
            {
                this.ProcessesRunning.Dequeue();
                this.CurrentProcess = this.ProcessesRunning.FirstOrDefault();
                this.CurrentProcessFinishTime = this.ProcessRunTime + currentTime;
            }
            else if (currentTime >= this.CurrentProcessFinishTime)
            {
                this.ProcessesRunning.Dequeue();
                this.ProcessesRunning.Enqueue(this.CurrentProcess);
                this.CurrentProcess = this.ProcessesRunning.FirstOrDefault();
            }

            return this.CurrentProcess;
        }

    }
}
