using UnityEngine;

public class AsteroidDestructionHandler : MonoBehaviour
{
    [SerializeField]
    private int pointsScored = 1;

    [SerializeField]
    private GameObject asteroidToSpawn;

    [SerializeField]
    private int numberOfAsteroidsToSpawn;

    [SerializeField]
    public float minForceMagnitudeSpawnedAsteroid = 0.5f;

    [SerializeField]
    public float maxForceMagnitudeSpawnedAsteroid = 1f;

    private void HandleDeath()
    {
        GameManager.Instance.AddScore(pointsScored);

        AsteroidManager.Instance.NotifyAsteroidDestroyed(gameObject);
        
        // spawn asteroids on death
        if (asteroidToSpawn != null)
        {
            for (int i = 0; i < numberOfAsteroidsToSpawn; i++)
            {
                GameObject asteroid = GameObject.Instantiate<GameObject>(asteroidToSpawn, transform.position, transform.rotation);
                AsteroidManager.Instance.NotifyAsteroidInstantiated(asteroid);   
                LaunchInRandomDirection(asteroid);
            }
        }
    }

    private void LaunchInRandomDirection(GameObject asteroid)
    {
        // get random direction on x and z axis
        Vector3 direction = Random.insideUnitSphere;
        direction.y = 0.0f;
        direction.Normalize();

        // pick random magnitude
        float forceMagnitude = Random.Range(minForceMagnitudeSpawnedAsteroid, maxForceMagnitudeSpawnedAsteroid);

        // apply force in given direction with given magnitude 
        asteroid.GetComponent<Rigidbody>().AddForce(direction * forceMagnitude, ForceMode.VelocityChange);
    }
}
