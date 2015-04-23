namespace Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Io
    {
        public Io()
        {
            this.ProcessLoad = new Queue<Process>();
            this.History = new List<Tuple<int, int>>();
        }

        private Queue<Process> ProcessLoad { get; set; }

        public List<Tuple<int, int>> History
        {
            get;
            set;
        }

        public void QueueProcess(Process process)
        {
            this.ProcessLoad.Enqueue(process);
        }

        public void Tick(int currentTime)
        {
            var process = this.ProcessLoad.FirstOrDefault();
            var processId = (null == process) ? -1 : process.Id;
            this.History.Add(new Tuple<int, int>(currentTime, processId));


            if (null == process)
            {
                return;
            }

            if (!process.CurrentBurstCycle.IoBurstTimeIsStarted)
            {
                process.CurrentBurstCycle.IoBurstTimeIsStarted = true;
                process.CurrentBurstCycle.IoBurstTimeStart = currentTime;
            }

            process.CurrentBurstCycle.IoBurstTimeElapsed += 1;
        
            if (process.CurrentBurstCycle.IoBurstIsComplete)
            {
                this.ProcessLoad.Dequeue();
                process.CurrentBurstCycle.IoBurstTimeFinished = currentTime;
                process.UpdateCurrentBurstCycle(currentTime);
            }
        }
    }
}
