namespace Infrastructure.Schedulers
{
    using Infrastructure.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class FirstComeFirstServed : ISchedule
    {
        public CpuRequest Schedule(CpuRequest cpuRequest)
        {
            throw new NotImplementedException();
        }
    }
}
