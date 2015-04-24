namespace Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ProcessLoad
    {
        public IList<Process> Processes
        {
            get;
            set;
        }

        public ProcessLoad Clone()
        {
            var processes = this.Processes
                .Select(p => new Process(new List<BurstCycle>(p.BurstCycles))
                    {
                        ArrivalTime = p.ArrivalTime,
                        Id = p.Id,
                    })
                .ToList();

            return new ProcessLoad
            {
                Processes = processes,
            };
        }

        public static ProcessLoad Create(int totalProcesses)
        {
            var random = new Random();
            var processes = new List<Process>();
            for (var i = 0; i < totalProcesses; i += 1)
            {
                var totalCycles = random.Next(1, 3);
                var burstCycles = Enumerable
                    .Range(0, totalCycles)
                    .Select(index => new BurstCycle
                    {
                        CpuBurstTime = random.Next(1, 15),
                        IoBurstTime = random.Next(15),
                    })
                    .ToList();



                processes.Add(new Process(burstCycles)
                {
                    ArrivalTime = random.Next(15),
                    Id = i,
                });
            }

            return new ProcessLoad { Processes = processes };
        }

        public SummarizeOutput SummarizeOutput(Cpu cpu, Io io)
        {
            var processes = this.Processes;
            var numberOfProcesses = processes.Count();
            var output = new SummarizeOutput
            {
                AverageTurnaroundTime = processes.Sum(p => p.TurnaroundTime) / numberOfProcesses,
                AverageWaitTime = io.History.Where(h => h.ProcessId == -1).Count() / numberOfProcesses,
                CpuUtilization = (100 * cpu.History.Where(h => h.ProcessId != -1).Count()) / cpu.History.Count(),
            };

            return output;
        }
    }
}
