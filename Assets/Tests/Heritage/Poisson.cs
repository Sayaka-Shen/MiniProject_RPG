using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TU_Challenge.Heritage
{
    public class Poisson : Animal
    {
        public override int Pattes => 0;

        public Poisson(string name) : base(name)
        {
        }

        public override string Crier()
        {
            return "Bloup";
        }

        public override void Die()
        {
            base.Die();

        }
    }
}
