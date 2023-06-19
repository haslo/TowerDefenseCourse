using UnityEngine;

public class ShooterBehaviour : MonoBehaviour {
    private GameObject currentTarget;
    private bool hasTarget;

    [SerializeField] private GameObject turretCore;
    [SerializeField] private GameObject turretGun;

    private void Start() {
        hasTarget = false;
    }

    private void OnTriggerEnter(Collider other) {
        if (!hasTarget && other.gameObject.CompareTag("Mob")) {
            currentTarget = other.gameObject;
            hasTarget = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject == currentTarget) {
            currentTarget = null;
            hasTarget = false;
        }
    }

    private void Update() {
        if (hasTarget) {
            this.transform.LookAt(currentTarget.transform.position);
        }
    }
}
