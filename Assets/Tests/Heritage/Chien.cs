using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TU_Challenge.Heritage
{
    public class Chien : Animal
    {
        public Chien(string name) : base(name)
        {
        }

        public override string Crier()
        {

            if (IsFed)
            {
                return "Ouaf (viens on joue ?)";
            }
            else
            {
                return "Ouaf (j'ai faim)";
            }
        }

    }
}
