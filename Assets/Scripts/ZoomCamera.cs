using UnityEngine;
using System.Collections;

public class ZoomCamera : MonoBehaviour {
    [SerializeField] private float minFov = 20f;
    [SerializeField] private float maxFov = 80f;
    [SerializeField] private float initialFov = 60f;
    [SerializeField] private float changeInterval = 20f;

    private new Camera camera;
    private float targetFov;
    private float fovChangeSpeed;
    private float lerpTime;

    void Start() {
        camera = GetComponent<Camera>();
        camera.fieldOfView = initialFov;
        SetRandomTargetFOV();
        StartCoroutine(ChangeFOVPeriodically());
        lerpTime = 0;
    }

    void Update() {
        if (Mathf.Abs(camera.fieldOfView - targetFov) > 0.01f) {
            lerpTime += Time.deltaTime * fovChangeSpeed;
            camera.fieldOfView = Mathf.SmoothStep(camera.fieldOfView, targetFov, lerpTime);
        }
    }

    private void SetRandomTargetFOV() {
        targetFov = Random.Range(minFov, maxFov);
        fovChangeSpeed = 1f / changeInterval;
        lerpTime = 0;
    }

    private IEnumerator ChangeFOVPeriodically() {
        while (true) {
            yield return new WaitForSeconds(changeInterval);
            SetRandomTargetFOV();
        }
    }
}
