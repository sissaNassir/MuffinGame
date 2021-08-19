using UnityEngine;

public class Coin : MonoBehaviour {

    public AudioSource oneTimeSoundPrefab;
    public AudioClip sound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
        {
            AudioSource source = Instantiate(oneTimeSoundPrefab);
            source.clip = sound;
            source.Play();
            Destroy(source.gameObject, source.clip.length + 0.1f);
        }
    }
}
