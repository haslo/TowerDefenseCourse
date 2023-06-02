using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    [SerializeField] private GameObject[] spawnPoints;

    void Start() {
        spawnPoints = GameObject.FindGameObjectsWithTag("spawn");
        foreach (var spawnPoint in spawnPoints) {
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
