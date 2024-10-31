using System;
using TMPro;
using UnityEngine;

public class ShowCoin : MonoBehaviour
{
    [Header("Show Params")]
    [SerializeField] private Coin _coin;
    [SerializeField] private TMP_Text _textCoin;

    private void Start()
    {
        _coin.OnTakeCoin += ShowCoinUI;
    }

    private void OnDestroy()
    {
        _coin.OnTakeCoin -= ShowCoinUI; 
    }
    
    private void ShowCoinUI()
    {
        _textCoin.text = $"{_coin.CointCount} Coins";
    }
}
