using UnityEngine;

public class ShooterBehaviour : MonoBehaviour {
    private GameObject currentTarget;
    private MobBehaviour currentMobBehaviour;
    private bool hasTarget;

    [SerializeField] private GameObject turretCore;
    [SerializeField] private GameObject turretGun;
    [SerializeField] private int shotPower = 1;

    private void Start() {
        SetTarget(null);
    }

    private void OnTriggerEnter(Collider other) {
        if (!hasTarget && other.gameObject.CompareTag("Mob")) {
            SetTarget(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject == currentTarget) {
            SetTarget(null);
        }
    }

    private void SetTarget(GameObject newTarget) {
        currentTarget = newTarget;
        if (currentTarget) {
            currentMobBehaviour = currentTarget.GetComponent<MobBehaviour>();
            hasTarget = true;
        } else {
            currentMobBehaviour = null;
            hasTarget = false;
        }
    }

    private void Update() {
        if (hasTarget) {
            SlerpyLookAt(currentTarget.transform.position);
        } else {
            SlerpyLookAt(Vector3.forward * 100);
        }
    }

    private void ShootTarget() {
        if (hasTarget) {
            currentMobBehaviour.Hit(shotPower);
        }
    }

    private void SlerpyLookAt(Vector3 targetPosition) {
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
