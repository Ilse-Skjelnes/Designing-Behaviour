using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{
    [SerializeField]
    private GameObject objectToSpawnOnCollision = null;

    public AudioSource source;
    public List<AudioClip> clips = new List<AudioClip>();

    private void OnCollisionEnter(Collision collision)
    {
        DestroySelf();
    }

    private void OnTriggerEnter(Collider other)
    {
        DestroySelf();

        if (other.tag == "Asteroid")
        {
            PlaySound();
            AsteroidManager.Instance.asteroidCount--;
            Debug.Log("Asteroid Destroyed");
        }
    }

    private void DestroySelf()
    {
        // instantiate new object before dying (usually for VFX)
        if (objectToSpawnOnCollision != null)
            GameObject.Instantiate<GameObject>(objectToSpawnOnCollision, transform.position, transform.rotation);

        Destroy(gameObject);
    }

    private void PlaySound()
    {
        int i = Random.Range(0, clips.Count + 1);
        source.clip = clips[i];
        source.Play();
    }
}
