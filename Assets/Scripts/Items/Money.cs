using UnityEngine;

public class Money : Item
{
    [Header("Money Params")]
    [SerializeField] private Coin _playerCoin;
        
    protected override void ItemEffect()
    {
        base.ItemEffect();

        _playerCoin.AddCoin();
    }
}
