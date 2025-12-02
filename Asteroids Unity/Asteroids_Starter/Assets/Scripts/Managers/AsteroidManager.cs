using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    // singleton for easy access throughout the whole project
    private static AsteroidManager instance;
    public static AsteroidManager Instance { get { return instance; } }

    public List<AsteroidData> asteroids = new List<AsteroidData>();

    public GameObject asteroidPrefab;
    public float AstreroidfollowingForce;

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

    public List<GameObject> asteroidString = new List<GameObject>();
    private int stringCount = 2;

    [SerializeField]
    private int maxRotation = 10;
    [SerializeField]
    private int asteroidSpeed = 1;

    private float asteroidMovementTimer;
    [SerializeField]
    private float asteroidMovementTime = 1;

    [SerializeField]
    private int usefulNumber = 50;
    [SerializeField]
    private List<Transform> leadAsteroidTransform = new List<Transform>();

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
        SpawnAsteroidOffscreen();
    }

    private void Update()
    {
        asteroidSpawnTimer -= Time.deltaTime;

        if (asteroidString.Count <= 0)
        {
            leadAsteroidTransform.Clear();
            SpawnAsteroidOffscreen();        
            Grow(stringCount);
            stringCount++;
            
        }

        if (asteroidSpawnTimer <= 0)
        {
            RandomMovement(asteroidString[0].GetComponent<Rigidbody>());
            ResetTimer();
        }
    }

    private void SpawnAsteroidOffscreen()
    {
        // instantiate new GO from prefab on position off screen
        GameObject asteroid = Instantiate(asteroidPrefab, GetRandomPositionOffScreen(), Quaternion.identity, transform);
        //ApplyRandomForceTowardsCenter(asteroid);
        AsteroidData data = asteroid.GetComponent<AsteroidData>();
        if(asteroids.Count >= 1)
        {
            data.SetFollowingTarget(asteroidString[asteroidString.Count - 1].transform);
        }
        asteroids.Add(data);
        asteroidString.Add(asteroid);
        ApplyRandomForceTowardsCenter(data.Rigidbody);
    }

    private void ApplyRandomForceTowardsCenter(Rigidbody rb)
    {
        Rigidbody rigidbody = rb.GetComponent<Rigidbody>();

        if (rigidbody == null) 
            return;

        // determine direction vector towards center
        Vector3 direction = -rb.transform.position.normalized;

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

    public void NotifyAsteroidInstantiated(AsteroidData asteroid)
    {
        asteroids.Add(asteroid);
    }

    public void NotifyAsteroidDestroyed(AsteroidData asteroid)
    {
        asteroids.Remove(asteroid);
        asteroidString.Remove(asteroid.gameObject);
    }

    private void SpawnAtFirstAsteroid(Vector3 firstAsteroidTransform)
    {   
        // instantiate new GO from prefab on position off screen
        GameObject asteroid = Instantiate(asteroidPrefab, firstAsteroidTransform, Quaternion.identity, transform);

        AsteroidData data = asteroid.GetComponent<AsteroidData>();
        if (asteroids.Count >= 1)
        {
            data.SetFollowingTarget(asteroids[asteroids.Count - 1].transform);
        } 
        asteroids.Add(data);
        asteroidString.Add(asteroid);
    }

    private void RandomMovement(Rigidbody rb)
    {
        Rigidbody rigidbody = rb.GetComponent<Rigidbody>();

        if (rigidbody == null)
            return;

        Vector3 direction = new Vector3(Random.Range(-maxRotation, maxRotation), 0, Random.Range(-maxRotation, maxRotation));

        rigidbody.AddForce(direction * asteroidSpeed * Time.deltaTime, ForceMode.Impulse);
    }

    private void Grow(int stringCounter)
    {
        for (int i = 0; i < stringCounter; i++)
        {
            if (i == 0)
                continue;
            float x = asteroidString[i-1].transform.position.x;
            float z = asteroidString[i-1].transform.position.z;
            Vector3 pos = new Vector3(x, 0, z);
            SpawnAtFirstAsteroid(pos);
        }
    }
}
