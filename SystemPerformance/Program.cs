using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.Speech.Synthesis;

namespace Performance
{
    class Program
    {
        static void Main(string[] args)
        {
            // This will greet the user in defualt voice
            SpeechSynthesizer synth = new SpeechSynthesizer();
            synth.Speak("Welcome to Tim's performance monitor, version one point oh!");

            #region My Performance Count
            // This will pull the current CPU load in percentage
            PerformanceCounter perfCpuCount = new PerformanceCounter("Processor Information", "% Processor Time", "_total");

            // This will pull the current available memory (in MegaBytes)
            PerformanceCounter perfMemCount = new PerformanceCounter("Memory", "Available MBytes");

            // This will get us the system uptime (in seconds)
            PerformanceCounter perfUptimeCount = new PerformanceCounter("System", "System Up Time");
            perfMemCount.NextValue();

            #endregion

            TimeSpan uptimeSpan = TimeSpan.FromSeconds(perfUptimeCount.NextValue());
            string systemUptimeMessage = string.Format("The current system up time is {0} days {1} hours {2} minutes {3} seconds",
                (int)uptimeSpan.TotalDays,
                (int)uptimeSpan.Hours,
                (int)uptimeSpan.Minutes,
                (int)uptimeSpan.Seconds
                );

            // Tell the user what the current sytem uptime is.
            synth.Speak(systemUptimeMessage);

           

            // Infinite WHile Loop
            while(true)
            {
                // get the current performance counter value
                int currentCpuPercentage = (int)perfCpuCount.NextValue();
                int currentAvailableMemory = (int)perfMemCount.NextValue();

                // every 1 second print the CPU load in percentage to the screen
                Console.WriteLine("CPU Load        : {0}%", currentCpuPercentage);
                Console.WriteLine("Available Memory: {0}MB", currentAvailableMemory);

                // Only tell us when the cpu is above 80% usage
                if (currentCpuPercentage > 80)
                {
                    if (currentCpuPercentage == 100)
                    {
                        synth.SelectVoiceByHints(VoiceGender.Female);
                        string cpuLoadVoacalMessage = String.Format("WARNING: Holy crap your CPU is about to catch fire!");
                        synth.Speak(cpuLoadVoacalMessage);
                    }
                    else
                    {
                        synth.SelectVoiceByHints(VoiceGender.Male);
                        string cpuLoadVoacalMessage = String.Format("The current CPU load is {0} percent", currentCpuPercentage);
                        synth.Speak(cpuLoadVoacalMessage);
                    }
                }
                // Only tell us when memory is below one gigabyte
                if (currentAvailableMemory < 1024)
                {
                    synth.SelectVoiceByHints(VoiceGender.Male);
                    string memAvailableVocalMessage = String.Format("You currently have {0} megabytes of memeory available", currentAvailableMemory);
                    synth.Speak(memAvailableVocalMessage);
                }                
                Thread.Sleep(1000);
            }  // END OF LOOP
        }
    }
}
