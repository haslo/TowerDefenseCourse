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
            // turretCore.transform.LookAt(coreAim); // nope, do it with slerp
            var coreRotation = turretCore.transform.rotation;
            turretCore.transform.rotation = Quaternion.Slerp(coreRotation, Quaternion.LookRotation(coreAim - corePosition), Time.deltaTime);
            var gunRotation = turretGun.transform.rotation;
            // turretGun.transform.LookAt(targetPosition); // nope, do it with slerp
            var gunPosition = turretGun.transform.position;
            var distanceToTarget = Vector3.Distance(coreAim, gunPosition);
            var relativeTargetPosition = gunPosition + (turretGun.transform.forward * distanceToTarget);
            var gunAim = new Vector3(relativeTargetPosition.x, targetPosition.y, relativeTargetPosition.z);
            turretGun.transform.rotation = Quaternion.Slerp(gunRotation, Quaternion.LookRotation(gunAim - gunPosition), Time.deltaTime);
        }
    }
}
