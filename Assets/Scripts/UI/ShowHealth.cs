using Unity.Plastic.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class ShowHealth : MonoBehaviour
{
    [Header("Show Params")]
    [SerializeField] private Health _health;
    [SerializeField] private Image _imgSlider;

    private void Start()
    {
        // add ShowSlider to event TakeDamage
        _health.OnTakeDamage += ShowSlider;
    }

    private void OnDestroy()
    {
        // delete ShowSlider to event TakeDamage
        _health.OnTakeDamage -= ShowSlider;
    }

    // Methode
    private void ShowSlider()
    {
        _imgSlider.fillAmount = (float)_health.CurrentHealth / _health.MaxHealth;
    }
}
