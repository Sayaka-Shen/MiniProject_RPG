using PlasticGui.WorkspaceWindow.CodeReview;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageLoop : MonoBehaviour
{
    [Header("Damage Params")]
    [SerializeField] private float _damageInterval;

    Dictionary<Collider, Coroutine> _colliderDamageCoroutines;

    private void Awake()
    {
        _colliderDamageCoroutines = new Dictionary<Collider, Coroutine>();   
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out HitZone hz))
        {
            Coroutine coroutine = StartCoroutine(DamageRoutine(hz));

            _colliderDamageCoroutines.Add(other, coroutine);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (_colliderDamageCoroutines.ContainsKey(other))
        {
            Coroutine coroutine = _colliderDamageCoroutines[other];
            StopCoroutine(coroutine);
            _colliderDamageCoroutines.Remove(other);
        }
    }

    IEnumerator DamageRoutine(HitZone hz)
    {
        while (true)
        {
            hz.Damage();
            yield return new WaitForSeconds(_damageInterval);
        }
    }
}
