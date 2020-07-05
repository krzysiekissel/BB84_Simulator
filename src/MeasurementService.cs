using System;
using System.Collections.Generic;
using System.Text;

namespace BB84_Simulator
{
    public  class MeasurementService
    {
        public double ErrorRate { get; set; }

        public  char Measure(State state, Basis basis)
        {
            Random random = new Random();
            var randomValue = random.NextDouble();
            if (basis == Basis.Rectilinear)
            {
                if (state == State._90)
                    return randomValue < ErrorRate ? '0' : '1';
                if (state == State._0)
                    return randomValue < ErrorRate ? '1' : '0';

                return random.Next(0,2) == 1?'1':'0';

            }
            else
            {
                if (state == State._45)
                    return randomValue < ErrorRate ? '1' : '0';
                if (state == State._135)
                    return randomValue < ErrorRate ? '0' : '1';

                return random.Next(0, 2) == 1?'1':'0';
            }
        }
        public static State EncodeState(char bit, Basis basis)
        {
            if (basis == Basis.Rectilinear)
            {
                if (bit == '0')
                    return State._0;
                else
                    return State._90;
            }
            else
            {
                if (bit == '0')
                    return State._45;
                else
                    return State._135;
            }
        }
    }
}
