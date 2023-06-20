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
            var targetPosition = currentTarget.transform.position;
            var corePosition = turretCore.transform.position;

            var coreAim = new Vector3(targetPosition.x, corePosition.y, targetPosition.z);
            
            turretCore.transform.LookAt(coreAim);
            turretGun.transform.LookAt(targetPosition);
        }
    }
}
