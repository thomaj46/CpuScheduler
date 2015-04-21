namespace Infrastructure.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class CpuRequest
    {
        public IEnumerable<ProcessRequest> ProcessRequests
        {
            get;
            set;
        }
    }
}
