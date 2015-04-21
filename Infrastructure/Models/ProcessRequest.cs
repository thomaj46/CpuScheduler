namespace Infrastructure.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ProcessRequest
    {
        public int ProcessId
        {
            get;
            set;
        }

        public int ArrivalTime
        {
            get;
            set;
        }

        public List<WorkRequest> WorkRequests
        {
            get;
            set;
        }

        public double TurnaroundTime
        {
            get;
            set;
        }

        public double NormalizedTurnaroundTime
        {
            get;
            set;
        }

        public double CpuUtilization
        {
            get;
            set;
        }

        public double WaitingTime
        {
            get;
            set;
        }

        public double AverageWaitingTime
        {
            get;
            set;
        }
    }
}
