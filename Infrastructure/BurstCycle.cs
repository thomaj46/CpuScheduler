namespace Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class BurstCycle
    {
        public int CpuBurstTime { get; set; }
        public int CpuBurstTimeElapsed { get; set; }
        public int CpuBurstTimeRemaining
        {
            get
            {
                return this.CpuBurstTime - this.CpuBurstTimeElapsed;
            }
        }
        public int CpuBurstTimeStart { get; set; }
        public int CpuBurstTimeFinished { get; set; }
        public bool CpuBurstTimeIsStarted { get; set; }
        public bool CpuBurstIsComplete
        {
            get
            {
                return this.CpuBurstTimeElapsed >= this.CpuBurstTime;
            }
        }


        public int IoBurstTime { get; set; }
        public int IoBurstTimeElapsed { get; set; }
        public int IoBurstTimeStart { get; set; }
        public int IoBurstTimeFinished { get; set; }
        public bool IoBurstTimeIsStarted { get; set; }
        public bool IoBurstIsComplete
        {
            get
            {
                return this.IoBurstTimeElapsed >= this.IoBurstTime;
            }
        }

    }
}
