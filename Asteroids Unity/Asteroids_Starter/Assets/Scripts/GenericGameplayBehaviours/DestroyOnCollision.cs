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
    }

    private void DestroySelf()
    {
        // instantiate new object before dying (usually for VFX)
        if (objectToSpawnOnCollision != null)
            GameObject.Instantiate<GameObject>(objectToSpawnOnCollision, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}
