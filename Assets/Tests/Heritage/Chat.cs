using System;
using Unity.IO.LowLevel.Unsafe;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine.Events;

namespace TU_Challenge.Heritage
{
    public class Chat : Animal
    {
        private bool _ateFish;

        // Parameters
        public override int Pattes => 4;

        // Event
        public event Action OnDie;

        public Chat(string name) : base(name)
        {
            
        }

        public override string Crier()
        {
            if (IsFed)
            {
                return "Miaou (c'est bon laisse moi tranquille humain)";
            }
            else
            {
                if(_ateFish)
                {
                    return "Miaou (Le poisson etait bon)";
                }

                return "Miaou (j'ai faim)";
            }
        }

        public override void Welcome(Animalerie animalerie)
        {
            foreach (var animal in animalerie.Animals)
            {
                TryEatFish(animal);
            }

            animalerie.OnAddAnimal += TryEatFish;
        }

        private void TryEatFish(Animal animal)
        {
            if (animal is Poisson)
            {
                animal.Die();
                FeedAll();
                _ateFish = true;
            }
        }

        public override void Die()
        {
            base.Die();

            OnDie?.Invoke();
        }
    }
}
