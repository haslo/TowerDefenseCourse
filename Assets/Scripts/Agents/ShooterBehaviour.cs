using UnityEngine;

public class ShooterBehaviour : MonoBehaviour {
    private GameObject currentTarget;

    private void OnTriggerEnter(Collider other) {
        if (currentTarget == null && other.gameObject.CompareTag("Mob")) {
            currentTarget = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject == currentTarget) {
            currentTarget = null;
        }
    }

    private void Update() {
        if (currentTarget != null) {
            this.transform.LookAt(currentTarget.transform.position);
        }
    }
}
