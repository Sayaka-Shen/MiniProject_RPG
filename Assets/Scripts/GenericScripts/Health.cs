using NaughtyAttributes;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    // Fields
    [SerializeField, Min(0), MaxValue(150)] int _maxHealth = 100;
    [ShowNonSerializedField] private int _currentHealth = 0;

    // UnityEvent (comme "_onDie") = pour les GD -> pour qu'il place des event dans Unity au endroit voulu
    // C# Event = plus performant et pour les dev
    [SerializeField] private UnityEvent OnDie;

    // Events declared
    // int ajoute un param de type int (ça peut être n'importe quoi)
    // jusqu'à 16 types dispo
    public event Action OnTakeDamage;

    // Delegate type qui représente un evenement (plus besoin)

    // Properties
    public int CurrentHealth
    {
        get { return _currentHealth; }
        set 
        {
            if (value < 0)
            {
                Debug.LogError("La vie ne peut pas être mise inférieur à 0 !");
                return;
            }

            _currentHealth = value; 
        }
    }

    public int MaxHealth
    {
        get { return _maxHealth; }
    }


    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    private bool ValidateMaxHealth() => _maxHealth > 0;

    // Method
    [Button]
    private void TestTakeDamage()
    {
        TakeDamage(10);
    }

    [Button]
    private void TestTakeDamageError()
    {
        TakeDamage(-10);
    }

    public void TakeDamage(int damage)
    {
        // Guard
        if (this == null || damage <= 0)
        {
            Debug.LogWarning("Damage attempt on destroyed object or non-positive damage.");
            return;
        }

        _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, _maxHealth);

        OnTakeDamage?.Invoke(); // C# Event

        if(_currentHealth <= 0)
        {
            Die();
        }
    }

    public void Regeneraction(int regenPoint)
    {
        if (CurrentHealth < 100)
        {
            CurrentHealth += regenPoint;
            OnTakeDamage?.Invoke();
        }
    }

    public void Die()
    {
        IEnumerator DieRoutine()
        {
            OnDie?.Invoke();

            yield return new WaitForSeconds(0.5f);

            Destroy(gameObject);
        }

        Coroutine coroutine1 = StartCoroutine(DieRoutine());
    }
}