using System;
using System.Collections.Generic;
using System.Text;

namespace BB84_Simulator
{
    public static class Screen
    {
        public static void Print<T>(List<T> list, string description)
        {
            Console.Write("{0,-18}", description);
            for (int i = 0; i < list.Count; i++)
            {
                Console.Write(list[i]);
            }
            Console.WriteLine();
        }
        public static void Print(List<State> states, string description)
        {
            Console.Write("{0,-18}", description);
            for (int i = 0; i < states.Count; i++)
            {
                string value = "";
                if (states[i] == State._0)
                    value = "-";
                if (states[i] == State._90)
                    value = "|";
                if (states[i] == State._45)
                    value = "/";
                if(states[i]==State._135)
                    value="\\";
                Console.Write("{0,-3}",value);
            }
            Console.WriteLine();
        }
        public static void Print(List<Basis> basis, string description)
        {
            Console.Write("{0,-18}", description);
            for (int i = 0; i < basis.Count; i++)
            {
                var value = basis[i] == Basis.Rectilinear ? '+' : 'x';
                Console.Write("{0,-3}",value);
            }
            Console.WriteLine();
        }
        public static void Print(List<char> bits, string description)
        {
            Console.Write("{0,-18}", description);
            for (int i = 0; i < bits.Count; i++)
            {
                Console.Write("{0,-3}", bits[i]);
            }
            Console.WriteLine();
        }
    }
}
