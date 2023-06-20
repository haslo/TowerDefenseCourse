using UnityEngine;
using UnityEngine.AI;

public class MobBehaviour : MonoBehaviour {
    private NavMeshAgent navMeshAgent;
    private Transform goal;
    private bool isKilled = false;

    [SerializeField] private MobDetails mobDetails;

    private void Awake() {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update() {
        if (!isKilled && navMeshAgent.hasPath) {
            UpdateMovement();
        }
    }

    private void UpdateMovement() {
        navMeshAgent.SetDestination(goal.position);
        if (navMeshAgent.remainingDistance < 2f) {
            Destroy(this.gameObject, 0.1f);
            isKilled = true;
            LevelManager.Singleton.MobWasKilled();
        }
    }

    public void SetGoal(Transform newGoal) {
        goal = newGoal;
        navMeshAgent.SetDestination(goal.position);
    }
}
