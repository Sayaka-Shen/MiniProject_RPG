using NaughtyAttributes;
using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [Header("Coin Params")]
    [SerializeField, Min(0), MaxValue(150)] private int _maxCoin = 150;

    [ShowNonSerializedField] private int _coinCount = 0;
    public int CointCount
    {
        get { return _coinCount; }
    }

    public event Action OnTakeCoin;

    public void AddCoin()
    {
        if(_coinCount < _maxCoin)
        {
            _coinCount++;
            OnTakeCoin?.Invoke();
        }
    }
}
