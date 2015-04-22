namespace Infrastructure
{
    using System.Collections.Generic;
    using System.Linq;

    public abstract class Scheduler
    {
        public abstract Process GetProcessToRun(int currentTime);

        public Scheduler(ProcessLoad processLoad)
        {
            this.ProcessLoad = processLoad.Clone();
        }

        public ProcessLoad ProcessLoad
        {
            get;
            set;
        }

        public bool HasProcessToRun()
        {
            return this.ProcessLoad.Processes.Any(p => !p.IsCompleted);
        }
    }
}
