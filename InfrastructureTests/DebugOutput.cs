namespace InfrastructureTests
{
    using Infrastructure;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    public class DebugOutput
    {
        public void PrintHistory(List<HistoryItem> cpuHistory, List<HistoryItem> ioHistory)
        {
            var history = cpuHistory.Zip(ioHistory, (c, i) => new { Time = c.Time, Cpu = c.ProcessId, Io = i.ProcessId });
            Debug.WriteLine("| {0,-12} | {1,-9:} | {2,-9:} |\n", "Current Time", "Cpu Process", "IO Process");
            foreach (var item in history)
            {
                Debug.WriteLine("| {0,-12:0000} | {1,-11} | {2,-10:G} |", item.Time, item.Cpu, item.Io);
            }
        }
    }
}
