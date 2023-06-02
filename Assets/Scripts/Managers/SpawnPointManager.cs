using UnityEngine;

public class SpawnPointManager : MonoBehaviour {
    [SerializeField] private GameObject mobPrefab;
    [SerializeField] private Transform goalPosition;
    [SerializeField] private Transform parentGroup;

    [SerializeField] private int remainingSpawns = 1000;
    [SerializeField] private float spawnRate = 0.3f;
    [SerializeField] private float spawnDelay = 1f;

    public int RemainingSpawns => remainingSpawns;

    private void Start() {
        InvokeRepeating(nameof(DoSpawn), spawnDelay, spawnRate);
    }

    private void DoSpawn() {
        float angle = Random.Range(0, 360);
        Vector3 spawnPosition = transform.position + new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));
        GameObject mob = Instantiate(mobPrefab, spawnPosition, Quaternion.identity);
        var behaviour = mob.GetComponent<MobBehaviour>();
        mob.GetComponent<MobBehaviour>().SetGoal(goalPosition);
        mob.transform.SetParent(parentGroup);
        remainingSpawns -= 1;
        if (remainingSpawns <= 0) {
            CancelInvoke(nameof(DoSpawn));
        }
    }
}
