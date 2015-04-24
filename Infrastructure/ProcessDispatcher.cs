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
                process = scheduler.GetProcessToRun(currentTime);
                cpu.Tick(currentTime, process);
                io.Tick(currentTime);

                if (null != process)
                {
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

                    //if (process.IsCompleted)
                    //{
                    //    process.FinishTime = currentTime;
                    //}
                }
            }

            return scheduler.ProcessLoad;
        }
    }
}
