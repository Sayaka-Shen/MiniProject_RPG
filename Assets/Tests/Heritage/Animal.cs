using System;

namespace TU_Challenge.Heritage
{
    public class Animal : Animalerie
    {
        private string _name;
        public string Name => _name;

        private bool _isFed;
        public bool IsFed
        {
            get { return _isFed; }
            set { _isFed = value; }
        }

        public bool IsAlive { get; private set; }

        public virtual int Pattes => 0;

        public Animal(string name)
        {
            _name = name;
            IsAlive = true;
            _isFed = false;
        }

        public virtual string Crier() 
        { 
            return ""; 
        }

        public virtual void Die() 
        { 
            IsAlive = false;
        }

        public virtual void Welcome(Animalerie animalerie)
        {
            
        }
    }
}
