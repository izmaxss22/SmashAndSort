using UnityEngine;

public class CoinItem : MonoBehaviour
{
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.name != "player")
    //    {
    //        AudioManager.Instance.PlayAudioSource(AudioManager.AudioSourcesIds.GAME_DESTROY_COIN);
    //        Destroy(gameObject);
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name != "player")
        {
            AudioManager.Instance.PlayAudioSource(AudioManager.AudioSourcesIds.GAME_DESTROY_COIN);
            Destroy(gameObject);
        }
    }
}
