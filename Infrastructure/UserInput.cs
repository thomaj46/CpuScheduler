using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class UserInput
    {
        public static void main()
        {

            int numOfProcesses = 0;
            Console.WriteLine("Enter the number of processes: ");
            Console.WriteLine();
            numOfProcesses = Convert.ToInt32(Console.ReadLine());

            //makeProcesses(numOfProcesses);



        }
        public List<Process> makeProcesses(int numOfProcesses)
        {


            var burstCycles = new List<BurstCycle>();
            var ProcList = new List<Process>();

            Random r = new Random();

            for (int y = 0; y < numOfProcesses; y++)
            {
                int numOfBursts = r.Next(1, 4);

                for (int x = 0; x < numOfBursts; x++)
                {

                    // Process service time 
                    int cpuburst = r.Next(1, 10);
                    int ioburst = r.Next(1, 10);

                    var burst = new BurstCycle
                    {
                        CpuBurstTime = cpuburst,
                        IoBurstTime = ioburst,

                    };

                    burstCycles.Add(new BurstCycle
                    {
                        CpuBurstTime = cpuburst,
                        IoBurstTime = ioburst,

                    });

                }


                int arrivalTime = r.Next(0, 10);
                ProcList.Add(new Process(burstCycles)
                {
                    ArrivalTime = arrivalTime,
                    Id = y,
                });

            }


           return ProcList;
        }
    }
}