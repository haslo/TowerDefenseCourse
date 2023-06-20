using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MobBehaviour : MonoBehaviour {
    private NavMeshAgent navMeshAgent;
    private Transform goal;
    private bool isKilled = false;
    private int currentHealth;
    private Slider healthBar;

    [SerializeField] private GameObject healthBarPrefab;
    [SerializeField] private MobDetails mobDetails;
    private Camera mainCamera;

    private void Awake() {
        mainCamera = Camera.main;
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = mobDetails.Speed;
        currentHealth = mobDetails.MaxHealth;
        var healthBarGameObject = Instantiate(healthBarPrefab, this.transform.position, Quaternion.identity);
        healthBar = healthBarGameObject.GetComponent<Slider>();
        healthBar.transform.SetParent(GameObject.Find("HealthBars").transform); // TODO find better way of handling this, without that find
        healthBar.maxValue = mobDetails.MaxHealth;
        healthBar.value = currentHealth;
        UpdateHealth();
        healthBarGameObject.SetActive(true);
    }

    private void Update() {
        if (!isKilled && navMeshAgent.hasPath) {
            UpdateMovement();
            UpdateHealth();
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
            healthBar.transform.position = mainCamera.WorldToScreenPoint(transform.position + (Vector3.up * 2f));
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
        Destroy(healthBar.gameObject, 0.1f);
        isKilled = true;
        LevelManager.Singleton.MobWasKilled();    }
}
