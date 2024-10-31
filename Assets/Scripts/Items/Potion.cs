using UnityEngine;

public class Potion : Item
{
    [Header("Potion Param")]
    [SerializeField] private Health _playerHealth;
    [SerializeField] private int _regenPoint;

    protected override void ItemEffect()
    {
        base.ItemEffect();
        _playerHealth.Regeneraction(_regenPoint);
    }
}
