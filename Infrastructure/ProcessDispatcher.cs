namespace Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ProcessDispatcher
    {
        public ProcessLoad Start(Scheduler scheduler, Cpu cpu, Io io)
        {
            Process process;
            for (var currentTime = 0; scheduler.HasProcessToRun(); currentTime += 1)
            {
                process = scheduler.GetProcessToRun();
                if (null == process)
                {
                    // This is bad... throw exception???
                }

                cpu.Tick(currentTime);
                io.Tick(currentTime);

                if (process.CurrentBurstCycle.CpuBurstIsComplete)
                {
                    if (process.CurrentBurstCycle.IoBurstTime > 0)
                    {
                        io.QueueProcess(process);
                    }
                    else
                    {
                        process.UpdateCurrentBurstCycle(currentTime);
                    }
                }
            }

            return scheduler.ProcessLoad;
        }
    }
}
