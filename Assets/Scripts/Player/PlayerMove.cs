using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [Header("Input Movement")]
    [SerializeField] InputActionReference _moveInput;
    [SerializeField] InputActionReference _jumpInput;

    [Header("Movement")]
    [SerializeField] private Rigidbody _playerRigidBody;
    [SerializeField] private float _playerSpeed = 5f;
    private Vector3 _realDir;

    private bool _moving = false;
    public bool Moving
    {
        get { return _moving; }
    }

    [Header("Jump")]
    [SerializeField] private float _jumpForce = 12f;
    [SerializeField] private GroundCollision _groundCollision;
    private bool _jumping = false;

    [Header("Animation")]
    [SerializeField] private Animator _playerAnimator;
    private const string ISMOVING = "IsMoving";
    private const string ISJUMPING = "Jumping";

    // Enumerations
    private enum PlayerState
    {
        IDLE,
        WALK,
        JUMP
    }

    private PlayerState _playerState;

    private void Start()
    {
        // Cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        _moveInput.action.started += StartMovevement;
        _moveInput.action.performed += InMovement;
        _moveInput.action.canceled += StopMovement;

        _jumpInput.action.started += StartJump;
        _jumpInput.action.performed += Jumping;
        _jumpInput.action.canceled += StopJump;
    }

    private void OnDestroy()
    {
        _moveInput.action.started -= StartMovevement;
        _moveInput.action.performed -= InMovement;
        _moveInput.action.canceled -= StopMovement;

        _jumpInput.action.started -= StartJump;
        _jumpInput.action.performed -= Jumping;
        _jumpInput.action.canceled -= StopJump;
    }

    private void FixedUpdate()
    {
        if (!_jumping && _moving)
        {
            _playerRigidBody.MovePosition(transform.position + _realDir * _playerSpeed * Time.fixedDeltaTime);
            transform.LookAt(transform.position + _realDir);

            _playerAnimator.SetBool(ISMOVING, true);
        }
        else
        {
            _playerAnimator.SetBool(ISMOVING, false);
        }
    }

    private void StartMovevement(InputAction.CallbackContext obj)
    {
        _moving = true;
        _playerState = PlayerState.WALK;
    }

    private void InMovement(InputAction.CallbackContext obj)
    {
        var initialDir = obj.ReadValue<Vector2>();
        _realDir = new Vector3(initialDir.x, 0, initialDir.y);
        _realDir.Normalize();
    }

    private void StopMovement(InputAction.CallbackContext obj)
    {
        _moving = false;
        _playerState = PlayerState.IDLE;
    }


    // Jump
    private void StartJump(InputAction.CallbackContext context)
    {
        _jumping = true;
        _moving = false;
        _playerState = PlayerState.JUMP;
    }

    private void Jumping(InputAction.CallbackContext obj)
    {
        if (_jumping && _groundCollision.IsGrounded)
        {
            _playerRigidBody.AddForce(Vector2.up * _jumpForce, ForceMode.Impulse);
            _playerAnimator.SetTrigger(ISJUMPING);
        }
    }

    private void StopJump(InputAction.CallbackContext context)
    {
        _jumping = false;
        _playerState = PlayerState.IDLE;
    }
}
