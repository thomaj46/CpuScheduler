using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Infrastructure;

namespace CpuScheduler
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public ProcessDispatcher ProcessDispatcher { get; set; }
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            int numprocs = Convert.ToInt32(txtNumOfProcs.Text);
            var userin = new UserInput();
            List<Process> lp = userin.makeProcesses(numprocs);

            var processLoad = new ProcessLoad
            {
                Processes = userin.makeProcesses(numprocs),
                
              
            };

            int last = numprocs + 1;
            int arraycpu = 0;
            int arrayio =0;

            int time2 = 0;


            var scheduler = new FirstComeFirstServed(processLoad);
            var io = new Io();
            var cpu = new Cpu();
            ProcessDispatcher = new ProcessDispatcher();

            var result = this.ProcessDispatcher.Start(scheduler, cpu, io);
            var cpuHistory = cpu.History;
            var ioHistory = io.History;

          //  var cputime

            var runHistory = cpuHistory.Zip(ioHistory, (c, i) => new { CpuHistory = c, IoHistory = i });

            string header = String.Format("| {0,-12} | {1,-9:} | {2,-9:} |\n",
                                    "Current Time", "Cpu Process", "IO Process");
            Console.WriteLine(header);
            foreach (var historyItem in runHistory)
            {
                var time = historyItem.CpuHistory.Item1;

                time2 = time;

                var cpuProcess = historyItem.CpuHistory.Item2;

                 if (cpuProcess != -1)
                 {
                     arraycpu += 1;
                 }
                         


                var ioProcess = historyItem.IoHistory.Item2;

                if (ioProcess != -1)
                 {
                     arrayio += 1;
                 }

                string data = String.Format("| {0,-12:0000} | {1,-11} | {2,-10:G} |", time, cpuProcess, ioProcess);
                Console.WriteLine(data);

                
            }
            double myballs = Convert.ToDouble(arraycpu) / Convert.ToDouble(time2)*100.0;
            Int32 total = Convert.ToInt32(myballs);
            pfcfs.Value = total;

            int waittime = (time2 - arrayio)/numprocs;
            lfcfswait.Content = waittime;




            
        }
    }
}
