using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WindowsService2
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }
      static  service.Service1 s = new service.Service1();

       
        static System.Timers.Timer t;

        static public void Timer()
        {

            t = new System.Timers.Timer();
            t.Start();
             

            t.AutoReset = false;
            t.Elapsed += new System.Timers.ElapsedEventHandler(t_Elapsed);
            t.Interval = 20000;
            Console.ReadLine();
        }
        static void t_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            t.Start();
            s.noyau();

        }
        protected override void OnStart(string[] args)
        {
            Timer();
        }

        protected override void OnStop()
        {
        }
    }
}
