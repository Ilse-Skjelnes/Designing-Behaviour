using UnityEngine;

public class DestroyAfterLifetime : MonoBehaviour
{
    [SerializeField]
    private float lifetime = 1.0f;

    private void Update()
    {
        // count down lifetime every frame and destroy when it runs out
        lifetime -= Time.deltaTime;

        if (lifetime < 0.0f)
        {
            Destroy(gameObject);
        }
    }
}
