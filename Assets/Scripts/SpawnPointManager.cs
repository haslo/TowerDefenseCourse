using UnityEngine;
using UnityEngine.Serialization;

public class SpawnPointManager : MonoBehaviour {
    [SerializeField] private GameObject mobPrefab;
    [SerializeField] private Transform goalPosition;
    [SerializeField] private Transform parentGroup;

    [SerializeField] private int spawnCount = 100;

    public int SpawnCount {
        get => spawnCount;
        set => spawnCount = value;
    }

    [SerializeField] private float spawnRate = 0.3f;
    [SerializeField] private float spawnDelay = 1f;

    [SerializeField] private SpawnPointManager manager;

    void Start() {
        InvokeRepeating(nameof(Spawner), spawnDelay, spawnRate);
    }

    void Spawner() {
        float angle = Random.Range(0, 360);
        Vector3 spawnPosition = transform.position + new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));
        GameObject mob = Instantiate(mobPrefab, spawnPosition, Quaternion.identity);
        mob.GetComponent<FindGoal>().SetGoal(goalPosition);
        mob.transform.SetParent(parentGroup);
    }
}
