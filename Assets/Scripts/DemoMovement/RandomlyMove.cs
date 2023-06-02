using UnityEngine;
using UnityEngine.AI;

public class RandomlyMove : MonoBehaviour {
    private NavMeshAgent navMeshAgent;

    [SerializeField] private float goalDelta = 10f;

    private void Start() {
        navMeshAgent = GetComponent<NavMeshAgent>();
        InvokeRepeating(nameof(DoRandomMove), 1, goalDelta);
    }

    private void DoRandomMove() {
        Vector3 goalPosition = new Vector3(Random.Range(-100f, 100f), 0.5f, Random.Range(-100f, 100f));
        navMeshAgent.SetDestination(goalPosition);
    }
}
