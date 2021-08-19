using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeSpawned : MonoBehaviour {
    public List<Transform> spawnPoints = new List<Transform>();
    public GameObject cubePrefab;

    private void Start()
    {
        int index = UnityEngine.Random.Range(0, spawnPoints.Count);
        GameObject cubeInstance = Instantiate(cubePrefab, spawnPoints[index]);
        Cube cube = cubeInstance.GetComponent<Cube>();
        cube.onPlayerEnter += SpawnNewCube;
    }

    private void SpawnNewCube(GameObject oldCube)
    {
        Destroy(oldCube);
        int index = UnityEngine.Random.Range(0, spawnPoints.Count);
        GameObject cubeInstance = Instantiate(cubePrefab, spawnPoints[index]);
        Cube cube = cubeInstance.GetComponent<Cube>();
        cube.onPlayerEnter += SpawnNewCube;
    }
}
