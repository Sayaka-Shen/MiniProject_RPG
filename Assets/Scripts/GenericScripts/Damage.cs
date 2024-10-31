using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private PlayerAttack _playerAttack;

    [Header("Damage Params")]
    [SerializeField] private float _checkColliderInterval;

    private List<Collider> _colliders;
    private Coroutine _coroutine;

    private void Awake()
    {
        _colliders = new List<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out HitZone hz))
        {
            _colliders.Add(other);
            _coroutine = StartCoroutine(PermanentDetection(hz));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _colliders.Remove(other);
        
        if(_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
        
        _coroutine = null;
    }

    IEnumerator PermanentDetection(HitZone hz)
    {
        while (true)
        {
            foreach (var collider in _colliders)
            {
                if(_playerAttack.AttackingVar && collider != null)
                {
                    hz.Damage();
                }
            }

            yield return new WaitForSeconds(_checkColliderInterval);
        }
    }
}
