﻿namespace Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Cpu
    {
        public Cpu(Scheduler scheduler)
        {
            this.Scheduler = scheduler;
        }

        public Scheduler Scheduler { get; set; }

        public void Tick(int currentTime)
        {
            var process = this.Scheduler.GetProcessToRun(currentTime);
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
