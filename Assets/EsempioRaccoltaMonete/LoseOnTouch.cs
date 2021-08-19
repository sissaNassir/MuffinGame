using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class LoseOnTouch : MonoBehaviour
{
public AudioSource oneTimeSoundPrefab;
public AudioClip sound;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if(player!=null)
        {
            player.Die();
            AudioSource source = Instantiate(oneTimeSoundPrefab);
            source.clip = sound;
            source.Play();
            Destroy(source.gameObject, source.clip.length + 0.1f);
         
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            
            player.Die();

        }
    }
}
