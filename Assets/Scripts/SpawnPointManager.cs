using UnityEngine;
using UnityEngine.Serialization;

public class SpawnPointManager : MonoBehaviour
{
    [SerializeField] private GameObject mobPrefab;
    [SerializeField] private Transform goalPosition;
    [SerializeField] private Transform parentGroup;

    void Start()
    {
        InvokeRepeating(nameof(Spawner), 1, 0.3f);
    }

    void Spawner() {
        float angle = Random.Range(0, 360);
        Vector3 spawnPosition = transform.position + new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));
        GameObject mob = Instantiate(mobPrefab, spawnPosition, Quaternion.identity);
        mob.GetComponent<FindGoal>().SetGoal(goalPosition);
        mob.transform.SetParent(parentGroup);
    }
}
