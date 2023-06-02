using UnityEngine;
using UnityEngine.AI;

public class FindGoal : MonoBehaviour {
    private NavMeshAgent navMeshAgent;
    private Transform goal;

    void Start() {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update() {
        navMeshAgent.SetDestination(goal.position);
        if (navMeshAgent.remainingDistance < 2f && navMeshAgent.hasPath) {
            Destroy(this.gameObject, 0.1f);
        }
    }

    public void SetGoal(Transform newGoal) {
        goal = newGoal;
    }
}
