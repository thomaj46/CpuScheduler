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
            this.History = new List<HistoryItem>();
        }

        public List<HistoryItem> History
        {
            get;
            set;
        }

        public void Tick(int currentTime, Process process)
        {
            if (null == process)
            {
                this.History.Add(new HistoryItem { Time = currentTime, ProcessId = -1 });
                return;
            }

            this.History.Add(new HistoryItem { Time = currentTime, ProcessId = process.Id });
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
