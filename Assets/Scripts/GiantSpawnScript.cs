using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantSpawnScript : MonoBehaviour
{
    [SerializeField]
    private GameObject giantPrefab;
    
    private const int maxObjects = 5;
    private List<Transform> spawnPoints = new List<Transform>();

    void Start()
    {
        GameObject points = GameObject.FindGameObjectWithTag("Points");
        foreach (Transform child in points.transform)
        {
            spawnPoints.Add(child);
        }
    }

    void Update()
    {
        if (GameState.GiantCount < maxObjects)
        {
            Instantiate(giantPrefab, spawnPoints[Random.Range(0,spawnPoints.Count)].position, Quaternion.identity);
            GameState.GiantCount++;
        }
    }
}
