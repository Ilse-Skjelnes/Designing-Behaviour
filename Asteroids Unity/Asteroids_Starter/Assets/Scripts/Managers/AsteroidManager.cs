using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    // singleton for easy access throughout the whole project
    private static AsteroidManager instance;
    public static AsteroidManager Instance { get { return instance; } }

    private List<GameObject> asteroids = new List<GameObject>();

    public GameObject asteroidPrefab;
    public float padding = 0.1f;

    public float minSpawnTime = 1;
    public float maxSpawnTime = 3;
    private float asteroidSpawnTimer;

    [SerializeField]
    private int maxAsteroids = 5;

    [SerializeField]
    public float minForceMagnitudeTowardsCenter = 0.5f;

    [SerializeField]
    public float maxForceMagnitudeTowardsCenter = 1f;

    private void Awake()
    {
        // setup singleton
        if (instance != null)
            Destroy(instance.gameObject);
        instance = this;
    }

    private void Start()
    {
        ResetTimer();
    }

    private void Update()
    {
        asteroidSpawnTimer -= Time.deltaTime;

        if (asteroidSpawnTimer < 0 && asteroids.Count < maxAsteroids)
        {
            SpawnAsteroidOffscreen();
            ResetTimer();
        }
    }

    private void SpawnAsteroidOffscreen()
    {
        // instantiate new GO from prefab on position off screen
        GameObject asteroid = Instantiate(asteroidPrefab, GetRandomPositionOffScreen(), Quaternion.identity, transform);
        ApplyRandomForceTowardsCenter(asteroid);

        asteroids.Add(asteroid);
    }

    private void ApplyRandomForceTowardsCenter(GameObject asteroid)
    {
        Rigidbody rigidbody = asteroid.GetComponent<Rigidbody>();

        if (rigidbody == null) 
            return;

        // determine direction vector towards center
        Vector3 direction = -asteroid.transform.position.normalized;

        // pick random magnitude
        float forceMagnitude = Random.Range(minForceMagnitudeTowardsCenter, maxForceMagnitudeTowardsCenter);

        // apply force in given direction with given magnitude 
        rigidbody.AddForce(direction * forceMagnitude, ForceMode.VelocityChange);
    }

private void ResetTimer()
    {
        asteroidSpawnTimer = Random.Range(minSpawnTime, maxSpawnTime);
    }

    private Vector3 GetRandomPositionOffScreen()
    {
        // randomly choose which side to spawn
        int side = Random.Range(0, 4);

        // define padding as percentual screen w/h
        float paddingWidth = Screen.width * padding;
        float paddingHeight = Screen.height * padding;

        // define position vector in screen space
        Vector3 screenPosition = Vector3.zero;

        switch (side)
        {
            case 0: // top
                screenPosition = new Vector3(Random.Range(-paddingWidth, Screen.width + paddingWidth), Screen.height + paddingHeight);
                break;

            case 1: // right
                screenPosition = new Vector3(Screen.width + paddingWidth, Random.Range(-paddingHeight, Screen.height + paddingHeight));
                break;

            case 2: // bottom
                screenPosition = new Vector3(Random.Range(-paddingWidth, Screen.width + paddingWidth), -paddingHeight);
                break;

            case 3: // left
                screenPosition = new Vector3(-paddingWidth, Random.Range(-paddingHeight, Screen.height + paddingHeight));
                break;
        }

        // convert from view port space to world space
        Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        spawnPosition.y = 0;
        return spawnPosition;
    }

    public void NotifyAsteroidInstantiated(GameObject asteroid)
    {
        asteroids.Add(asteroid);
    }

    public void NotifyAsteroidDestroyed(GameObject asteroid)
    {
        asteroids.Remove(asteroid);
    }
}
