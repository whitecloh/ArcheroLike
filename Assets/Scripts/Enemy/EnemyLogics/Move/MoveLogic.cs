using UnityEngine;
using UnityEngine.AI;

    public class MoveLogic : ScriptableObject, IMove
    {
    protected Transform _target;
    public float Speed { get; set; }
    public Transform Enemy { protected get; set; }
    private NavMeshAgent _agent;


    public void SetTarget(Transform target)
        {
            _target = target;
        }

    public void SetTransform(Transform transform)
        {
            Enemy = transform;
            _agent = Enemy.GetComponent<NavMeshAgent>();
    }

    public void Move()
    {
        if (_agent == null) return;

            _agent.SetDestination(_target.position);
    }

    /*private Vector3 RandomPointOnNavMesh(Vector3 startPos, float radius)
    {
        Vector3 direction = UnityEngine.Random.insideUnitSphere * radius;
        direction += startPos;
        NavMeshHit hit;
        Vector3 finalPos = Vector3.zero;
        if (NavMesh.SamplePosition(direction, out hit, radius, 1))
        {
            finalPos = hit.position;
        }
        return finalPos;
    }*/
}
