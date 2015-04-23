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
    }
}
