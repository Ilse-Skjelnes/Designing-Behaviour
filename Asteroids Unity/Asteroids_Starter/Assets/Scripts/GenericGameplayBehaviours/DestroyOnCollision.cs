using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{
    [SerializeField]
    private GameObject objectToSpawnOnCollision = null;


    private void OnCollisionEnter(Collision collision)
    {
        DestroySelf();
    }

    private void OnTriggerEnter(Collider other)
    {
        DestroySelf();

        if (other.tag == "Asteroid")
        {
            
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

}
