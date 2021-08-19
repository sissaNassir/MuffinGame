using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CangrejoController : MonoBehaviour
{
    public float stoneGenerationPause = 10f;
    private MapGenerator mapGenerator;
    private RocksTimer rocksTimer;


    private void Awake()
    {
        mapGenerator = GameObject.FindObjectOfType<MapGenerator>();
        rocksTimer = GameObject.FindObjectOfType<RocksTimer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>()!=null)
        {
            rocksTimer.StartTimer(stoneGenerationPause);
            mapGenerator.StopRocksFor(stoneGenerationPause);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
        {
            rocksTimer.StartTimer(stoneGenerationPause);
            mapGenerator.StopRocksFor(stoneGenerationPause);
            Destroy(gameObject);
        }
    }


}
