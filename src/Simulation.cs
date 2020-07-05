using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BB84_Simulator
{
    public class Simulation
    {
        public double ChannelErrorRate { get; set; }
        public double DetectorErrorRate { get; set; }
        public int EveStrategy { get; set; }
        public int NumberOfPhotons { get; set; }

        public void Start()
        {
            var quantumChannel = new QuantumChannel() { ErrorRate = ChannelErrorRate };
            var detector = new MeasurementService() { ErrorRate = DetectorErrorRate };


            //Alice

            // Wybór baz
            List<Basis> aliceBasis = new List<Basis>();
            for (int i = 0; i < NumberOfPhotons; i++)
                aliceBasis.Add(RandomService.GetRandomBasis());

            // Wybór stanów
            List<State> aliceStates = new List<State>();
            for (int i = 0; i < NumberOfPhotons; i++)
                aliceStates.Add(RandomService.GetRandomState(aliceBasis[i]));

            //Generowanie bitów
            List<char> aliceBinary = new List<char>();
            for (int i = 0; i < NumberOfPhotons; i++)
            {
                var bit = detector.Measure(aliceStates[i], aliceBasis[i]);
                aliceBinary.Add(bit);
            }

            List<State> bobRecStates = aliceStates;


            //Eve
            //Odbiór fotonów
            List<State> eveStates = new List<State>();
            for (int i = 0; i < NumberOfPhotons; i++)
            {
                var state = quantumChannel.Send(aliceStates[i]);
                eveStates.Add(state);
            }

            //Wybór bazy

            List<Basis> eveBasis = new List<Basis>();
            if (EveStrategy == 0)
            {
                for (int i = 0; i < NumberOfPhotons; i++)
                    eveBasis.Add(RandomService.GetRandomBasis());
            }
            if (EveStrategy == 1)
            {
                for (int i = 0; i < NumberOfPhotons; i++)
                    eveBasis.Add(Basis.Rectilinear);
            }
            if (EveStrategy == 2)
            {
                for (int i = 0; i < NumberOfPhotons; i++)
                    eveBasis.Add(Basis.Diagonal);
            }

            List<char> eveBinary = new List<char>();
            List<State> eveNewStates = new List<State>();
            if (EveStrategy != 3)
            {
                //Generowanie bitów
                
                for (int i = 0; i < NumberOfPhotons; i++)
                {
                    var bit = detector.Measure(eveStates[i], eveBasis[i]);
                    eveBinary.Add(bit);
                }
                //Zakodowanie fotonów
                
                for (int i = 0; i < NumberOfPhotons; i++)
                {
                    var state = MeasurementService.EncodeState(eveBinary[i], eveBasis[i]);
                    eveNewStates.Add(state);
                }
                bobRecStates = eveNewStates;
            }
            

            






            //Bob

            //Odbiór fotonów
            List<State> bobStates = new List<State>();
            for (int i = 0; i < NumberOfPhotons; i++)
            {
                var state = quantumChannel.Send(bobRecStates[i]);
                bobStates.Add(state);
            }

            //Wybór bazy
            List<Basis> bobBasis = new List<Basis>();
            for (int i = 0; i < NumberOfPhotons; i++)
                bobBasis.Add(RandomService.GetRandomBasis());

            //Generowanie bitów
            List<char> bobBinary = new List<char>();
            for (int i = 0; i < NumberOfPhotons; i++)
            {
                var bit = detector.Measure(bobStates[i], bobBasis[i]);
                bobBinary.Add(bit);
            }

            //Ustalenie klucza wg Alice
            List<char> aliceKey = new List<char>();
            for (int i = 0; i < NumberOfPhotons; i++)
            {
                if (aliceBasis[i] == bobBasis[i])
                    aliceKey.Add(aliceBinary[i]);
                else
                    aliceKey.Add(' ');
            }


            List<char> eveKey = new List<char>();
            if (EveStrategy != 3)
            {
                //Ustalenie klucza wg Eve
                
                for (int i = 0; i < NumberOfPhotons; i++)
                {
                    if (aliceBasis[i] == eveBasis[i] && eveBasis[i] == bobBasis[i])
                    {
                        eveKey.Add(eveBinary[i]);
                        continue;
                    }
                    if (aliceBasis[i] == bobBasis[i])
                        eveKey.Add('?');
                    else
                        eveKey.Add(' ');
                }
            }
            

            //Ustalenie klucza wg Bob
            List<char> bobKey = new List<char>();
            for (int i = 0; i < NumberOfPhotons; i++)
            {
                if (aliceBasis[i] == bobBasis[i])
                    bobKey.Add(bobBinary[i]);
                else
                    bobKey.Add(' ');
            }
            if (NumberOfPhotons <= 100)
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Alice");
                Console.WriteLine();
                Screen.Print(aliceBasis, "Baza");
                Screen.Print(aliceStates, "Stany");
                Screen.Print(aliceBinary, "Bity");
                if (EveStrategy != 3)
                {
                    Console.WriteLine();
                    Console.WriteLine("Eve");
                    Console.WriteLine();
                    Screen.Print(eveBasis, "Baza");
                    Screen.Print(eveStates, "Stany odebrane");
                    Screen.Print(eveBinary, "Bity");
                    Screen.Print(eveNewStates, "Stany wyslane");
                }
                
                Console.WriteLine();
                Console.WriteLine("Bob");
                Console.WriteLine();
                Screen.Print(bobBasis, "Baza");
                Screen.Print(bobStates, "Stany");
                Screen.Print(bobBinary, "Bity");

                Console.WriteLine();
                Console.WriteLine("Klucze");
                Console.WriteLine();

                Screen.Print(aliceKey, "Alice");
                if(EveStrategy!=3)
                    Screen.Print(eveKey, "Eve");
                Screen.Print(bobKey, "Bob");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Dane nie zostały wyświetlone ze względu na zbyt duża liczbę fotonów");
            }
            

            var counter = 0;
            for (int i = 0; i < aliceKey.Count; i++)
            {
                if (aliceKey[i] == ' ' || bobKey[i] == ' ')
                    continue;
                if (aliceKey[i] != bobKey[i] )
                    counter++;
            }
            Console.WriteLine();
            Console.WriteLine("{0,-18} {1,4:F4}", "Błąd klucza Alice - Bob ", $"{100*counter / (double)aliceKey.Where(s=>s!=' ').Count()} %");
            Console.WriteLine();


        }

    }
}
