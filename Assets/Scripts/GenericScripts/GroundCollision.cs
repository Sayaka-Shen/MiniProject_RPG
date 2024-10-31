using Codice.Foreign;
using UnityEngine;

public class GroundCollision : MonoBehaviour
{
    [Header("Check Ground")]
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundCheckRadius = 0.5f;

    public bool IsGrounded => Physics.CheckSphere(_groundCheck.position, _groundCheckRadius, _layerMask);

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(_groundCheck.position, _groundCheckRadius);
    }
}
