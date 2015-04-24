namespace Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class SummarizeOutput
    {
        public int AverageTurnaroundTime { get; set; }
        public int AverageWaitTime { get; set; }
        public decimal CpuUtilization { get; set; }
    }
}
