namespace InfrastructureTests
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;
    using Infrastructure;
    using System.Diagnostics;
    using Process = Infrastructure.Process;

    [TestClass]
    public class IntegrationTests
    {
        public ProcessDispatcher ProcessDispatcher { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            this.ProcessDispatcher = new ProcessDispatcher();
        }

        [TestMethod]
        public void TestFirstComeFirstServed()
        {
            var bursts1 = new List<BurstCycle> { new BurstCycle { CpuBurstTime = 10, IoBurstTime = 15 }, new BurstCycle { CpuBurstTime = 4, IoBurstTime = 6 } };
            var bursts2 = new List<BurstCycle> { new BurstCycle { CpuBurstTime = 8, IoBurstTime = 6 }, new BurstCycle { CpuBurstTime = 7, IoBurstTime = 0 } };
            var bursts3 = new List<BurstCycle> { new BurstCycle { CpuBurstTime = 15, IoBurstTime = 4 }, new BurstCycle { CpuBurstTime = 8, IoBurstTime = 0 } };
            var process1 = new Process(bursts1) { Id = 0, ArrivalTime = 0 };
            var process2 = new Process(bursts2) { Id = 1, ArrivalTime = 3 };
            var process3 = new Process(bursts3) { Id = 2, ArrivalTime = 10 };

            var processLoad = new ProcessLoad
            {
                Processes = new List<Process>
                {
                    process1,
                    process2,
                    process3,
                }
            };

            var scheduler = new FirstComeFirstServed(processLoad);
            var io = new Io();
            var cpu = new Cpu();

            var result = this.ProcessDispatcher.Start(scheduler, cpu, io);
            var cpuHistory = cpu.History;
            var ioHistory = io.History;

            var runHistory = cpuHistory.Zip(ioHistory, (c, i) => new { CpuHistory = c, IoHistory = i });
           
            string header = String.Format("| {0,-12} | {1,-9:} | {2,-9:} |\n",
                                    "Current Time", "Cpu Process", "IO Process");
            Debug.WriteLine(header);
            foreach (var historyItem in runHistory)
            {
                var time = historyItem.CpuHistory.Item1;
                var cpuProcess = historyItem.CpuHistory.Item2;
                var ioProcess = historyItem.IoHistory.Item2;

                
                string data = String.Format("| {0,-12:0000} | {1,-11} | {2,-10:G} |", time, cpuProcess, ioProcess);
                Debug.WriteLine(data);
            }

            var x = 3;
        }

        [TestMethod]
        public void TestRoundRobin()
        {
            var bursts1 = new List<BurstCycle> { new BurstCycle { CpuBurstTime = 10, IoBurstTime = 15 }, new BurstCycle { CpuBurstTime = 4, IoBurstTime = 6 } };
            var bursts2 = new List<BurstCycle> { new BurstCycle { CpuBurstTime = 8, IoBurstTime = 6 }, new BurstCycle { CpuBurstTime = 7, IoBurstTime = 0 } };
            var bursts3 = new List<BurstCycle> { new BurstCycle { CpuBurstTime = 15, IoBurstTime = 4 }, new BurstCycle { CpuBurstTime = 8, IoBurstTime = 0 } };
            var process1 = new Process(bursts1) { ArrivalTime = 0 };
            var process2 = new Process(bursts2) { ArrivalTime = 3 };
            var process3 = new Process(bursts3) { ArrivalTime = 10 };

            var processLoad = new ProcessLoad
            {
                Processes = new List<Process>
                {
                    process1,
                    process2,
                    process3,
                }
            };

            var scheduler = new RoundRobin(processLoad, 4);
            var io = new Io();
            var cpu = new Cpu();

            var result = this.ProcessDispatcher.Start(scheduler, cpu, io);
        }


        [TestMethod]
        public void ShortestJobFirst()
        {
            var bursts1 = new List<BurstCycle> { new BurstCycle { CpuBurstTime = 10, IoBurstTime = 15 }, new BurstCycle { CpuBurstTime = 4, IoBurstTime = 6 } };
            var bursts2 = new List<BurstCycle> { new BurstCycle { CpuBurstTime = 8, IoBurstTime = 6 }, new BurstCycle { CpuBurstTime = 7, IoBurstTime = 0 } };
            var bursts3 = new List<BurstCycle> { new BurstCycle { CpuBurstTime = 15, IoBurstTime = 4 }, new BurstCycle { CpuBurstTime = 8, IoBurstTime = 0 } };
            var process1 = new Process(bursts1) { ArrivalTime = 0 };
            var process2 = new Process(bursts2) { ArrivalTime = 3 };
            var process3 = new Process(bursts3) { ArrivalTime = 10 };

            var processLoad = new ProcessLoad
            {
                Processes = new List<Process>
                {
                    process1,
                    process2,
                    process3,
                }
            };

            var scheduler = new ShortestProcessTime(processLoad);
            var io = new Io();
            var cpu = new Cpu();

            var result = this.ProcessDispatcher.Start(scheduler, cpu, io);
        }


        [TestMethod]
        public void ShortestRemainingTimeFirst()
        {
            var bursts1 = new List<BurstCycle> { new BurstCycle { CpuBurstTime = 2, IoBurstTime = 3 }, new BurstCycle { CpuBurstTime = 4, IoBurstTime = 6 } };
            var bursts2 = new List<BurstCycle> { new BurstCycle { CpuBurstTime = 4, IoBurstTime = 6 }, new BurstCycle { CpuBurstTime = 7, IoBurstTime = 0 } };
            var bursts3 = new List<BurstCycle> { new BurstCycle { CpuBurstTime = 5, IoBurstTime = 4 }, new BurstCycle { CpuBurstTime = 8, IoBurstTime = 0 } };
            var process1 = new Process(bursts1) { ArrivalTime = 0 };
            var process2 = new Process(bursts2) { ArrivalTime = 3 };
            var process3 = new Process(bursts3) { ArrivalTime = 10 };

            var processLoad = new ProcessLoad
            {
                Processes = new List<Process>
                {
                    process1,
                    process2,
                    process3,
                }
            };

            var scheduler = new ShortestRemainingTime(processLoad);
            var io = new Io();
            var cpu = new Cpu();

            var result = this.ProcessDispatcher.Start(scheduler, cpu, io);



        }
    }
}
