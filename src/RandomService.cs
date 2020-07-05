using System;
using System.Collections.Generic;
using System.Text;

namespace BB84_Simulator
{
    public static class RandomService
    {
        public static State GetRandomState(Basis basis)
        {
            var random = new Random();
            if (basis == Basis.Rectilinear)
            {
                return random.Next(0, 2) == 0 ? State._0 : State._90;
            }
            else
            {
                return random.Next(0, 2) == 0 ? State._45 : State._135;
            }
            
        }
        public static Basis GetRandomBasis()
        {
            var random = new Random();
            return (Basis)random.Next(0, 2);
        }
    }
}
