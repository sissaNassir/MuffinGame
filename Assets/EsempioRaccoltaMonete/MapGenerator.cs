using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class MapGenerator : MonoBehaviour
{
    public Transform player;
    public Tile tile;
    public int startTiles = 20;
    public float tileSize = 1;
    public GameObject coin;
    public GameObject enemy;
    public GameObject bonusLife;
    public GameObject cangrejo;
    public GameObject[] rocks;
    public Transform rockSpawnHeigh;
    Tilemap tilemap;
    private int lastTileXposition;
    public bool canGenerateRock = true;
    int generatedPlatforms = 0;
    private Coroutine stopRocksRoutine;
    List<GameObject> spawnedRocks;

    private void Awake()
    {
        tilemap = GetComponent<Tilemap>();
        lastTileXposition = (int)(startTiles * tileSize);
        spawnedRocks = new List<GameObject>();
    }

    void Update()
    {
        float distance = lastTileXposition - player.position.x;
        if (distance < 10 * tileSize)
        {
            bool spawnEnemy = Random.Range(0, 100) < 4;
            bool spawnBonusLife = Random.Range(0, 100) < 9;

            //generiamo altri tiles;
            lastTileXposition++;
            generatedPlatforms++;

            if (generatedPlatforms % Random.Range(10, 21) == 0)
            {
                //gbenera piattaforma
                int height = Random.Range(2, 6);
                for (int i = 0; i < Random.Range(5, 10); i++)
                {
                    bool spawnCoin = Random.Range(0, 2) > 0;
                    if (spawnCoin)
                    {
                        Vector3 coinPosition = new Vector3(lastTileXposition + i + 0.5f, height + 1 + 0.5f, 0);
                        Instantiate(coin, coinPosition, Quaternion.identity, null);
                    }
                    spawnEnemy = SpawnPrefab(enemy, spawnEnemy, height);
                    spawnBonusLife = SpawnPrefab(bonusLife, spawnBonusLife, height);

                    tilemap.SetTile(new Vector3Int(lastTileXposition + i, height, 0), tile);

                }

                height = Random.Range(5, 8);
                int shift = Random.Range(0, 3);

                for (int i = 0; i < Random.Range(2, 4); i++)
                {
                    bool spawnCoin = Random.Range(0, 2) > 0;
                    if (spawnCoin)
                    {
                        Vector3 coinPosition = new Vector3(lastTileXposition + i + shift + 0.5f, height + 1 + 0.5f, 0);
                        Instantiate(coin, coinPosition, Quaternion.identity, null);
                    }
                    spawnEnemy = SpawnPrefab(enemy, spawnEnemy, height);
                    spawnBonusLife = SpawnPrefab(bonusLife, spawnBonusLife, height);

                    tilemap.SetTile(new Vector3Int(lastTileXposition + i + shift, height, 0), tile);
                }
            }

            spawnEnemy = SpawnPrefab(enemy, spawnEnemy, 1);

            tilemap.SetTile(Vector3Int.right * lastTileXposition, tile);

            if (canGenerateRock)
            {
                if (Random.Range(0, 100) < 10)
                {
                    //spawn rock
                    GameObject rockInstance = Instantiate(rocks[Random.Range(0, rocks.Length)]);
                    spawnedRocks.Add(rockInstance);
                    Vector3 currentPosition = rockInstance.transform.position;
                    currentPosition.y = rockSpawnHeigh.position.y;
                    currentPosition.z = rockSpawnHeigh.position.z;
                    currentPosition.x = lastTileXposition + 0.5f;
                    rockInstance.transform.position = currentPosition;
                }
            }

            if (Random.Range(0, 100) < 1)
            {
                SpawnPrefab(cangrejo, true, 0);
            }
        }
    }

    public void StopRocksFor(float stoneGenerationPause)
    {
        List<GameObject> toRemove = new List<GameObject>();
        for (int i = 0; i < spawnedRocks.Count; i++)
        {
            if(spawnedRocks[i].GetComponentInChildren<Renderer>().isVisible)
            {
                toRemove.Add(spawnedRocks[i]);
            }
        }

        for (int i = 0; i < toRemove.Count; i++)
        {
            spawnedRocks.Remove(toRemove[i]);
            Destroy(toRemove[i]);
        }

        if (stopRocksRoutine != null)
        {
            StopCoroutine(stopRocksRoutine);
        }
        stopRocksRoutine = StartCoroutine(StopRocksRoutine(stoneGenerationPause));
    }

    private IEnumerator StopRocksRoutine(float stoneGenerationPause)
    {
        canGenerateRock = false;
        yield return new WaitForSeconds(stoneGenerationPause);
        canGenerateRock = true;
        stopRocksRoutine = null;
    }

    private bool SpawnPrefab(GameObject prefab, bool needToSpawn, int height)
    {
        if (needToSpawn)
        {
            Vector3 position = new Vector3(lastTileXposition + 0.5f, height + 0.5f, 0);
            Instantiate(prefab, position, Quaternion.identity, null);
            needToSpawn = false;
        }

        return needToSpawn;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawSphere(Vector3.right * lastTileXposition, 0.3f);
    }
}

