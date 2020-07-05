using System;
using System.Collections.Generic;
using System.Text;

namespace BB84_Simulator
{
    public class QuantumChannel
    {
        public double ErrorRate { get; set; }

        public State Send(State state)
        {
            Random random = new Random();
            var randomValue = random.NextDouble();
            if (state == State._0)
            {
                return randomValue < ErrorRate ? State._90 : State._0;
            }
            else if (state == State._90)
            {
                return randomValue < ErrorRate ? State._0 : State._90;
            }
            else if (state == State._45)
            {
                return randomValue < ErrorRate ? State._135 : State._45;
            }
            else
            {
                return randomValue < ErrorRate ? State._45 : State._135;
            }
        }
    }
}
