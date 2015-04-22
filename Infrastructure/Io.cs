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
        }

        private Queue<Process> ProcessLoad { get; set; }

        public void QueueProcess(Process process)
        {
            this.ProcessLoad.Enqueue(process);
        }

        public void Tick(int currentTime)
        {
            var process = this.ProcessLoad.FirstOrDefault();
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
