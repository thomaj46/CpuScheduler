namespace Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Feedback : Scheduler
    {
        public Feedback(ProcessLoad processLoad) : base(processLoad)
        {
            this.LevelOneQueue = new Queue<Process>();
            this.LevelTwoQueue = new Queue<Process>();
            this.LevelThreeQueue = new Queue<Process>();
            this.LevelFourQueue = new List<Process>();
        }
        private Queue<Process> LevelOneQueue { get; set; }
        private Queue<Process> LevelTwoQueue { get; set; }
        private Queue<Process> LevelThreeQueue { get; set; }
        private List<Process> LevelFourQueue { get; set; }
        private int levelOneRuntime = 2;
        private int levelTwoRuntime = 4;
        private int levelThreeRuntime = 8;
        private Process CurrentProcess { get; set; }
        private int CurrentProcessFinishTime { get; set; }

        public override Process GetProcessToRun(int currentTime)
        {
            foreach (var process in this.ProcessLoad.Processes.Where(p => p.ArrivalTime == currentTime))
            {
                this.LevelOneQueue.Enqueue(process);
            }

            this.SetCurrentProcessFromLevelOne(currentTime);
            return this.CurrentProcess;
        }

        private void SetCurrentProcessFromLevelOne(int currentTime)
        {
            if (!this.LevelOneQueue.Any())
            {
                this.SetCurrentProcessFromLevelTwo(currentTime);
            }
            else if (null == this.CurrentProcess)
            {
                this.CurrentProcess = this.LevelOneQueue.FirstOrDefault(p => !p.CurrentBurstCycle.CpuBurstIsComplete);
                this.CurrentProcessFinishTime = this.levelOneRuntime + currentTime;
            }
            else if (this.CurrentProcess.IsCompleted)
            {
                this.LevelOneQueue.Dequeue();
                this.CurrentProcess = null;
                this.SetCurrentProcessFromLevelOne(currentTime);
            }
            else if (currentTime >= this.CurrentProcessFinishTime)
            {
                this.LevelOneQueue.Dequeue();
                this.LevelTwoQueue.Enqueue(this.CurrentProcess);
                this.CurrentProcess = null;
                this.SetCurrentProcessFromLevelOne(currentTime);
            }
            else if (this.CurrentProcess.CurrentBurstCycle.CpuBurstIsComplete)
            {
                this.CurrentProcess = null;
            }
        }

        private void SetCurrentProcessFromLevelTwo(int currentTime)
        {
            if (!this.LevelTwoQueue.Any())
            {
                this.SetCurrentProcessFromLevelThree(currentTime);
            }
            else if (null == this.CurrentProcess)
            {
                this.CurrentProcess = this.LevelTwoQueue.FirstOrDefault(p => !p.CurrentBurstCycle.CpuBurstIsComplete);
                this.CurrentProcessFinishTime = this.levelTwoRuntime + currentTime;
            }
            else if (this.CurrentProcess.IsCompleted)
            {
                this.LevelTwoQueue.Dequeue();
                this.CurrentProcess = null;
                this.SetCurrentProcessFromLevelTwo(currentTime);
            }
            else if (currentTime >= this.CurrentProcessFinishTime)
            {
                this.LevelTwoQueue.Dequeue();
                this.LevelThreeQueue.Enqueue(this.CurrentProcess);
                this.CurrentProcess = null;
                this.SetCurrentProcessFromLevelTwo(currentTime);
            }
            else if (this.CurrentProcess.CurrentBurstCycle.CpuBurstIsComplete)
            {
                this.CurrentProcess = null;
            }
        }

        private void SetCurrentProcessFromLevelThree(int currentTime)
        {
            if (!this.LevelThreeQueue.Any())
            {
                this.SetCurrentProcessFromLevelFour(currentTime);
            }
            else if (null == this.CurrentProcess)
            {
                this.CurrentProcess = this.LevelThreeQueue.FirstOrDefault(p => !p.CurrentBurstCycle.CpuBurstIsComplete);
                this.CurrentProcessFinishTime = this.levelThreeRuntime + currentTime;
            }
            else if (this.CurrentProcess.IsCompleted)
            {
                this.LevelThreeQueue.Dequeue();
                this.CurrentProcess = null;
                this.SetCurrentProcessFromLevelThree(currentTime);
            }
            else if (currentTime >= this.CurrentProcessFinishTime)
            {
                this.LevelThreeQueue.Dequeue();
                this.LevelFourQueue.Add(this.CurrentProcess);
                this.CurrentProcess = null;
                this.SetCurrentProcessFromLevelThree(currentTime);
            }
            else if (this.CurrentProcess.CurrentBurstCycle.CpuBurstIsComplete)
            {
                this.CurrentProcess = null;
            }
        }

        private void SetCurrentProcessFromLevelFour(int currentTime)
        {
            this.CurrentProcess = this.LevelFourQueue
                .Where(p => !p.CurrentBurstCycle.CpuBurstIsComplete)
                .OrderBy(p => p.CurrentBurstCycle.CpuBurstTimeRemaining)
                .FirstOrDefault();
        }
    }
}
