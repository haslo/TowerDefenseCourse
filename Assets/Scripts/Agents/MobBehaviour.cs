using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MobBehaviour : MonoBehaviour {
    private NavMeshAgent navMeshAgent;
    private Transform goal;
    private bool isKilled = false;
    private int currentHealth;
    private Slider healthBar;

    [SerializeField] private Slider healthBarPrefab;
    [SerializeField] private MobDetails mobDetails;
    private Camera mainCamera;

    private void Awake() {
        mainCamera = Camera.main;
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = mobDetails.Speed;
        currentHealth = mobDetails.MaxHealth;
        healthBar = Instantiate(healthBarPrefab, this.transform.position, Quaternion.identity);
        healthBar.transform.SetParent(GameObject.Find("Canvas").transform); // TODO find better way of handling this, without that find
        healthBar.maxValue = mobDetails.MaxHealth;
        healthBar.value = currentHealth;
    }

    private void Update() {
        if (!isKilled && navMeshAgent.hasPath) {
            UpdateMovement();
            UpdateHealth();
        }
        if (healthBar) {
            healthBar.transform.position = mainCamera.WorldToScreenPoint(transform.position + (Vector3.up * 2f));
        }
    }

    private void UpdateMovement() {
        navMeshAgent.SetDestination(goal.position);
        if (navMeshAgent.remainingDistance < 2f) {
            Die();
        }
    }

    private void UpdateHealth() {
        if (healthBar) {
            healthBar.value = currentHealth;
        }
    }

    public void Hit(int power) {
        currentHealth -= power;
        if (currentHealth <= 0) {
            Die();
        }
    }

    public void SetGoal(Transform newGoal) {
        goal = newGoal;
        navMeshAgent.SetDestination(goal.position);
    }

    public void Die() {
        if (isKilled) return;
        Destroy(gameObject, 0.1f);
        Destroy(healthBar, 0.1f);
        isKilled = true;
        LevelManager.Singleton.MobWasKilled();    }
}
