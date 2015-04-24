namespace Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ConsoleOutput
    {
        public void PrintHistory(List<HistoryItem> cpuHistory, List<HistoryItem> ioHistory)
        {
            var history = cpuHistory.Zip(ioHistory, (c, i) => new { Time = c.Time, Cpu = c.ProcessId, Io = i.ProcessId });
            Console.WriteLine("| {0,-12} | {1,-9:} | {2,-9:} |\n", "Current Time", "Cpu Process", "IO Process");
            foreach (var item in history)
            {
                Console.WriteLine("| {0,-12:0000} | {1,-11} | {2,-10:G} |", item.Time, item.Cpu, item.Io);
            }
        }

        public void PrintHistoryToStringBuilder(List<HistoryItem> cpuHistory, List<HistoryItem> ioHistory, StringBuilder stringBuilder)
        {
            var history = cpuHistory.Zip(ioHistory, (c, i) => new { Time = c.Time, Cpu = c.ProcessId, Io = i.ProcessId });
            var header = string.Format("| {0,-12} | {1,-9:} | {2,-9:} |\n", "Current Time", "Cpu Process", "IO Process");
            stringBuilder.AppendLine(header);
            foreach (var item in history)
            {
                var line = string.Format("| {0,-12:0000} | {1,-11} | {2,-10:G} |", item.Time, item.Cpu, item.Io);
                stringBuilder.AppendLine(line);
            }
        }
    }
}
