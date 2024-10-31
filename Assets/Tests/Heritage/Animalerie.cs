using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace TU_Challenge.Heritage
{
    public class Animalerie
    {
        private List<Animal> _animals;

        public List<Animal> Animals => _animals; 

        // Constructor
        public Animalerie()
        {
            _animals = new();
        }

        // Event 
        public event Action<Animal> OnAddAnimal;

        public void AddAnimal(Animal animal)
        {
            _animals.Add(animal);
            OnAddAnimal?.Invoke(animal);
            animal.Welcome(this);
        }

        public bool Contains(Animal animal)
        {
            return _animals.Contains(animal);
        }

        public Animal GetAnimal(int index)
        {
            return _animals[index];
        }
    
        public void FeedAll()
        {
            foreach (var animal in _animals)
            {
                animal.IsFed = true;
            }
        }
    }
}
