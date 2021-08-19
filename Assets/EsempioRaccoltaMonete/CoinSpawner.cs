using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;

    private GameObject coinInstance;
    bool hasCoinAutoDestoyed = true;
    bool hasBeenSpawnedOnce = false;

    private void Update()
    {

        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);

        if (coinInstance == null)
        {
            if (screenPoint.x > 0 && screenPoint.x < 1 && hasCoinAutoDestoyed)
            {
                coinInstance = Instantiate(coinPrefab, transform);
                coinInstance.transform.position = transform.position;
                hasBeenSpawnedOnce = true;
            }
        }
        else
        {
            if (!(screenPoint.x > 0 && screenPoint.x < 1))
            {
                Destroy(coinInstance);
                hasCoinAutoDestoyed = true;
            }
            else if(hasBeenSpawnedOnce)
            {
                hasCoinAutoDestoyed = false;
            }
        }
    }


}
