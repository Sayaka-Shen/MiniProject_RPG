using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputActionReference _attackInput;

    [Header("Animation")]
    [SerializeField] private Animator _playerAnimator;
    private const string ISATTACKING = "Attacking";

    [Header("Movement")]
    [SerializeField] private PlayerMove _playerMovement;

    // Event on attack
    public UnityEvent _onAttacking;

    private bool _attackingVar = false;
    public bool AttackingVar
    {
        get { return _attackingVar; } 
    }

    private void Start()
    {
        _attackInput.action.started += StartAttack;
        _attackInput.action.performed += Attacking;
        _attackInput.action.canceled += StopAttack;
    }

    private void OnDestroy()
    {
        _attackInput.action.started -= StartAttack;
        _attackInput.action.performed -= Attacking;
        _attackInput.action.canceled -= StopAttack;
    }   

    private void StartAttack(InputAction.CallbackContext obj)
    {
        _attackingVar = true;
    }

    private void Attacking(InputAction.CallbackContext obj)
    {
        _playerAnimator.SetTrigger(ISATTACKING);
        _onAttacking?.Invoke();
    }

    private void StopAttack(InputAction.CallbackContext obj)
    {
        StartCoroutine(WaitBeforeStopAttack());
    }

    IEnumerator WaitBeforeStopAttack()
    {
        yield return new WaitForSeconds(0.2f);

        _attackingVar = false;
    }
}
