namespace Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Cpu
    {
        public Cpu()
        {
            this.History = new List<Tuple<int, int>>();
        }

        public List<Tuple<int, int>> History
        {
            get;
            set;
        }

        public void Tick(int currentTime, Process process)
        {
            var processId = (null == process) ? -1 : process.Id;
            this.History.Add(new Tuple<int, int>(currentTime, processId));
            
            if (null == process)
            {
                return;
            }

            if (!process.IsStarted)
            {
                process.IsStarted = true;
                process.StartTime = currentTime;
            }

            if (!process.CurrentBurstCycle.CpuBurstTimeIsStarted)
            {
                process.CurrentBurstCycle.CpuBurstTimeIsStarted = true;
                process.CurrentBurstCycle.CpuBurstTimeStart = currentTime;
            }

            process.CurrentBurstCycle.CpuBurstTimeElapsed += 1;

            if (process.CurrentBurstCycle.CpuBurstIsComplete)
            {
                process.CurrentBurstCycle.CpuBurstTimeFinished = currentTime;
            }
        }

    }
}
