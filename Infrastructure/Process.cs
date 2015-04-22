﻿namespace Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Process
    {
        public Process(IEnumerable<BurstCycle> burstCycles)
        {
            this.BurstCycles = burstCycles;
            this.BurstCyclesRemaining = new Queue<BurstCycle>(burstCycles);
            this.CurrentBurstCycle = this.BurstCyclesRemaining.Dequeue();
        }

        public int Id { get; set; }
        public int ArrivalTime { get; set; }
        public int StartTime { get; set; }
        public int FinishTime { get; set; }
        public bool IsStarted { get; set; }
        public bool IsCompleted { get; set; }
        public BurstCycle CurrentBurstCycle { get; set; }
        public IEnumerable<BurstCycle> BurstCycles { get; set; }
        private Queue<BurstCycle> BurstCyclesRemaining { get; set; }
        public void UpdateCurrentBurstCycle(int currentTime)
        {
            if (this.BurstCyclesRemaining.Any())
            {
                this.CurrentBurstCycle = this.BurstCyclesRemaining.Dequeue();
                this.ArrivalTime = currentTime;
            }
            else
            {
                this.IsCompleted = this.CurrentBurstCycle.CpuBurstIsComplete && this.CurrentBurstCycle.IoBurstIsComplete;
            }
        }
    }
}
