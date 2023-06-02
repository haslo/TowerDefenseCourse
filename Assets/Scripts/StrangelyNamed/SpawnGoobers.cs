using UnityEngine;

public class SpawnGoobers : MonoBehaviour {
    [SerializeField] private GameObject goobPrefab;
    [SerializeField] private Transform goalPosition;
    [SerializeField] private Transform parentGroup;

    void Start() {
        InvokeRepeating(nameof(Spawner), 1, 0.3f);
    }

    void Spawner() {
        float angle = Random.Range(0, 360);
        Vector3 spawnPosition = transform.position + new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));
        GameObject goob = Instantiate(goobPrefab, spawnPosition, Quaternion.identity);
        goob.GetComponent<FindGoal>().SetGoal(goalPosition);
        goob.transform.SetParent(parentGroup);
    }
}
