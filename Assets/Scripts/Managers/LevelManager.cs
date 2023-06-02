using UnityEngine;

public class LevelManager : MonoBehaviour {
    [SerializeField] private GameObject[] spawnPoints;
    [SerializeField] private int remainingMobs = 0;

    private static LevelManager singleton;

    public static LevelManager Singleton => singleton;

    private void Awake() {
        singleton = this;
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        foreach (var spawnPoint in spawnPoints) {
            remainingMobs += spawnPoint.GetComponent<SpawnPointManager>().RemainingSpawns;
        }
    }

    public void MobWasKilled() {
        remainingMobs -= 1;
        if (remainingMobs <= 0) {
            Debug.Log("Game Over Man, Game Over!");
        }
    }
}
