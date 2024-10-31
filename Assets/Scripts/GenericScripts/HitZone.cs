using System.Collections;
using UnityEngine;

public class HitZone : MonoBehaviour
{
    [Header("Param Hit")]
    [SerializeField] private Health _health;
    [SerializeField] private int _damage;
    
    public void Damage()
    {
        _health.TakeDamage(_damage);
    }
}
