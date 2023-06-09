using System;
using UnityEngine;

public class UIInterface : MonoBehaviour {
    private GameObject focusObject;
    [SerializeField] private GameObject cubeTurretPrefab;
    private Camera mainCamera;

    private void Start() {
        mainCamera = Camera.main;
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out var hit)) {
                return;
            }

            focusObject = Instantiate(cubeTurretPrefab, hit.point, cubeTurretPrefab.transform.rotation);
            focusObject.GetComponentInChildren<Collider>().enabled = false;
        } else if (focusObject && Input.GetMouseButton(0)) {
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out var hit)) {
                return;
            }

            focusObject.transform.position = hit.point;
        } else if (focusObject && Input.GetMouseButtonUp(0)) {
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out var hit))
                return;
            var collidedObject = hit.collider.gameObject;
            if (collidedObject.CompareTag("EmptyPlatform")) {
                var collidedPosition = collidedObject.transform.position;
                focusObject.transform.position = new Vector3(collidedPosition.x, collidedPosition.y + 0.5f, collidedPosition.z);
                collidedObject.tag = "OccupiedPlatform";
            } else {
                Destroy(focusObject);
            }

            focusObject = null;
        }
    }
}
