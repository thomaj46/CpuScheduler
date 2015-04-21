namespace Infrastructure.Schedulers
{
    using Infrastructure.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface ISchedule
    {
        CpuRequest Schedule(CpuRequest cpuRequest);
    }
}
