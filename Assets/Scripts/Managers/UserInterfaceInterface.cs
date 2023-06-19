using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UserInterfaceInterface : MonoBehaviour {
    [SerializeField] private Transform turretParentGroup;

    private GameObject newTurret;
    private GameObject menuTurret;

    [SerializeField] private GameObject rocketTurretPrefab;
    [SerializeField] private GameObject gatlingTurretPrefab;
    [SerializeField] private GameObject flamerTurretPrefab;

    [SerializeField] private GameObject turretPopup;
    private Dictionary<GameObject, GameObject> turretBases;

    private Camera mainCamera;

    private void Start() {
        mainCamera = Camera.main;
        turretBases = new Dictionary<GameObject, GameObject>();
        turretPopup.SetActive(false);
    }

    public void CreateRocketTurret() {
        CreateTurret(rocketTurretPrefab);
    }

    public void CreateGatlingTurret() {
        CreateTurret(gatlingTurretPrefab);
    }

    public void CreateFlamerTurret() {
        CreateTurret(flamerTurretPrefab);
    }

    private void CreateTurret(GameObject prefab) {
        var ray = mainCamera.ScreenPointToRay(Input.mousePosition); // Input.GetTouch(0).position
        if (!Physics.Raycast(ray, out var hit)) {
            return;
        }
        newTurret = Instantiate(prefab, hit.point, prefab.transform.rotation);
        var focusColliders = newTurret.GetComponentsInChildren<Collider>();
        foreach (var focusCollider in focusColliders) {
            focusCollider.enabled = false;
        }
    }

    public void TurretPopupUpgrade() {
        // TODO
    }

    public void TurretPopupDelete() {
        turretBases[menuTurret].tag = "EmptyPlatform";
        turretBases.Remove(menuTurret);
        Destroy(menuTurret);
        turretPopup.SetActive(false);
    }

    public void TurretPopupClose() {
        turretPopup.SetActive(false);
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) { // if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
            if (turretPopup.activeSelf) {
                return;
            }
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition); // Input.GetTouch(0).position
            if (!Physics.Raycast(ray, out var hit)) {
                return;
            }
            var collidedObject = hit.collider.gameObject;
            if (collidedObject.CompareTag("Turret")) {
                menuTurret = collidedObject;
                turretPopup.SetActive(true);
            }
        } else if (newTurret && Input.GetMouseButton(0)) { // if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out var hit)) {
                return;
            }
            newTurret.transform.position = hit.point;
        } else if (newTurret && Input.GetMouseButtonUp(0)) { // if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out var hit))
                return;
            var collidedObject = hit.collider.gameObject;
            if (collidedObject.CompareTag("EmptyPlatform") && hit.normal == Vector3.up) {
                var collidedPosition = collidedObject.transform.position;
                newTurret.transform.position = new Vector3(collidedPosition.x, newTurret.transform.position.y, collidedPosition.z);
                collidedObject.tag = "OccupiedPlatform";
                turretBases[newTurret] = collidedObject;
                if (turretParentGroup) {
                    newTurret.transform.SetParent(turretParentGroup);
                }
                var focusColliders = newTurret.GetComponentsInChildren<Collider>();
                foreach (var focusCollider in focusColliders) {
                    focusCollider.enabled = true;
                }
            } else {
                Destroy(newTurret);
            }
            newTurret = null;
        }
    }
}
