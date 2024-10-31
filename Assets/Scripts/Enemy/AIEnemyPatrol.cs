using UnityEngine;
using UnityEngine.AI;

public class AIEnemyPatrol : MonoBehaviour
{
    [Header("Object Needed")]
    [SerializeField] private GameObject _player;
    [SerializeField] private NavMeshAgent _agent;

    [Header("Layer Params")]
    [SerializeField] private LayerMask _groundLayer, _playerLayer;

    [Header("Patrol Params")]
    [SerializeField] private float _walkRange;
    [SerializeField] private bool _activateWalkRangeGizmos;
    private Vector3 _destPoint;
    private bool _walkPointSet;

    [Header("Chase Params")]
    [SerializeField] private float _sightRange;
    [SerializeField] private bool _activateSightRangeGizmos;
    private bool _playerInSight;

    [Header("Attack Params")]
    [SerializeField] private float _attackRange;
    [SerializeField] private bool _activateAttackRangeGizmos;
    private bool _playerInAttackRange;


    private void Update()
    {
        _playerInSight = Physics.CheckSphere(transform.position, _sightRange, _playerLayer);
        _playerInAttackRange = Physics.CheckSphere(transform.position, _attackRange, _playerLayer);

        if (!_playerInSight && !_playerInAttackRange)
        {
            Patrol();
        }

        if(_playerInSight && !_playerInAttackRange)
        {
            Chase();
        }

        if(_playerInSight && _playerInAttackRange)
        {
            Attack();
        }

    }

    private void Chase()
    {
        _agent.SetDestination(_player.transform.position);
    }

    private void Attack()
    {

    }

    private void Patrol()
    {
        if(!_walkPointSet)
        {
            SearchForDestination();
        }

        if(_walkPointSet)
        {
            _agent.SetDestination(_destPoint);
        }

        if(Vector3.Distance(transform.position, _destPoint) < 10)
        {
            _walkPointSet = false;
        }
    }

    private void SearchForDestination()
    {
        float z = Random.Range(-_walkRange, _walkRange);
        float x = Random.Range(-_walkRange, _walkRange);

        // Set a destPoint based on the random of z and x
        _destPoint = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);

        // Return true if raycast find anything directly below destPoint = set groundLayer -> navMeshLayer
        if(Physics.Raycast(_destPoint, Vector3.down, _groundLayer))
        {
            _walkPointSet = true;
        }
    }

    private void OnDrawGizmos()
    {
        if (_activateWalkRangeGizmos)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, _walkRange);
        }

        if(_activateSightRangeGizmos)
        { 
            Gizmos.DrawSphere(transform.position, _sightRange);       
        }

        if(_activateAttackRangeGizmos)
        {
            Gizmos.DrawSphere(transform.position, _attackRange);
        }
    }
}
