using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAM
{
    class Program
    {
       static  public float cpu;
       static  public float ram;
 
        static void Main(string[] args)
        {
            PerformanceCounter cpuCounter;
            PerformanceCounter ramCounter;

            cpuCounter = new PerformanceCounter();

            cpuCounter.CategoryName = "Processor";
            cpuCounter.CounterName = "% Processor Time";
            cpuCounter.InstanceName = "_Total";
            ramCounter = new PerformanceCounter("Memory", "Available MBytes");
            Console.WriteLine("CPU");
            cpu = cpuCounter.NextValue();
            Console.WriteLine(cpu + "%");
            Console.WriteLine("RAM");
            ram = ramCounter.NextValue();
            Console.WriteLine(ram+ "MB");
             DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                Console.WriteLine(drive.Name);
                if (drive.IsReady)
                    //d = drive.TotalFreeSpace;
                    Console.WriteLine("Size  :" + drive.TotalSize + " KB ,Free space  :" + drive.TotalFreeSpace + " KB");

            }
            



        }
    }
}