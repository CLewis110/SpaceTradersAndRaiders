using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] asteroids;
    public GameObject[] spawnPositions;

    void Update()
    {
        Spawn();
    }

    private void Spawn()
    {
        Vector2 position = spawnPositions[Random.Range(0, spawnPositions.Length)].transform.position;
        GameObject asteroidClone = Instantiate(asteroids[Random.Range(0, asteroids.Length)], new Vector2(position.x, position.y), transform.rotation);
        asteroidClone.SetActive(true);
    }
}
