using System;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public Action<GameObject> onPlayerEnter;

    private void OnTriggerEnter(Collider other)
    {
        onPlayerEnter.Invoke(gameObject);
    }
}
