using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
    [Header("Item Params")]
    [SerializeField, Tag] private string _targetedTag;
    [SerializeField] private GameObject _item;

    public UnityEvent _onPickingUp;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(_targetedTag))
        {
            ItemEffect();
        }
    }

    // Private: only this class can access it
    // Protected: only this class and his derived class can access it 

    // Abstract: does not have initial implementation, you have to override the method 
    // Virtual: has implementation and the derived class with the option to override it

    // class sealed: close heritage

    protected virtual void ItemEffect() 
    {
        Destroy(_item);
        _onPickingUp?.Invoke();
    }
}
