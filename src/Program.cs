using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

namespace BB84_Simulator
{
    class Program
    {
        static void Main(string[] args)
        {

            while (true)
            {
                Console.WriteLine("Symulator BB84");
                Console.WriteLine();
                Console.WriteLine("Wybierz liczbę fotonów:");
                var numberOfPhotons = Convert.ToInt32( Console.ReadLine());
                Console.WriteLine();
                Console.WriteLine("Strategia Eve 0 - losowy dobór bazy, 1 - baza prosta, 2 - baza skośna, 3 - brak podsłuchu:");
                var eveStrategy = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();
                Console.WriteLine("Błąd kanału kwantowego [0:1]:");
                var channelErrorRate = Convert.ToDouble(Console.ReadLine().Replace(".",","));
                Console.WriteLine();
                Console.WriteLine("Błąd detektora [0:1]:");
                var detectorErrorRate = Convert.ToDouble(Console.ReadLine().Replace(".", ","));


                var simulation = new Simulation() { NumberOfPhotons = numberOfPhotons, ChannelErrorRate = channelErrorRate, DetectorErrorRate = detectorErrorRate,EveStrategy = eveStrategy };
                simulation.Start();
                Console.ReadLine();



            }


            

            ////Screen.Print<char>(aliceBinary, "Alice");
            ////Screen.Print<char>(eveBinary, "Eve");
            ////Screen.Print<char>(bobBinary, "Bob");

            ////Screen.Print<char>(aliceKey, "Alice");
            ////Screen.Print<char>(eveKey, "Eve");
            ////Screen.Print<char>(bobKey, "Bob");

            
            //Screen.Print(aliceBasis, "Alice");
            //Screen.Print(aliceStates, "");
            //Screen.Print(aliceBinary, "");
            //Console.WriteLine();
            //Screen.Print(eveBasis, "Eve");
            //Screen.Print(eveStates, "");
            //Screen.Print(eveBinary, "");
            //Screen.Print(eveNewStates, "");
            //Console.WriteLine();
            //Screen.Print(bobBasis, "Bob");
            //Screen.Print(bobStates, "");
            //Screen.Print(bobBinary, "");

            //var errors = 0;

            //for (int i = 0; i < aliceKey.Count; i++)
            //{
            //    if (aliceKey[i] != bobKey[i])
            //        errors++;
            //}
            //Console.WriteLine(errors / (double)aliceKey.Count);








        }
    }
}
