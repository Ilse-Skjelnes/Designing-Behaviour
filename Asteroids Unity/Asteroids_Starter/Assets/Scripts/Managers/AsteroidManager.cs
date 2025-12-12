using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    // singleton for easy access throughout the whole project
    private static AsteroidManager instance;
    public static AsteroidManager Instance { get { return instance; } }

    public GameObject asteroidPrefab;
    public float AstreroidfollowingForce;

    public float padding = 0.1f;
    public float minSpawnTime = 1;
    public float maxSpawnTime = 3;
    private float asteroidSpawnTimer;

    [SerializeField]
    public float minForceMagnitudeTowardsCenter = 0.5f;

    [SerializeField]
    public float maxForceMagnitudeTowardsCenter = 1f;

    public List<GameObject> asteroidString = new List<GameObject>();
    public int stringCount = 1;

    [SerializeField]
    private int maxRotation = 10;
    [SerializeField]
    private int asteroidSpeed = 1;

    [SerializeField]
    private int spawnOffset = 1;
    private Vector3 addedVector;
    [SerializeField]
    private float smoothTime = 1.0f;
    [SerializeField]
    private Vector3 refVelocity = new Vector3(0, 0, 1);

    [SerializeField]
    private float waitingTime;
    private float waitingTimer;

    static public int asteroidScore;
    public int asteroidCount;
    private int asteroids;

    private float spawnTimer;
    [SerializeField]
    private float spawnTime = 1f;
    public Vector3 spawnPosition;

    public int asteroidsInScreen;
    
    private void Awake()
    {
        // setup singleton
        if (instance != null)
            Destroy(instance.gameObject);
        instance = this;

        addedVector = new Vector3(spawnOffset, 0, 0);

        asteroidCount = 0;
    }

    private void Start()
    {
        Vector3 spawnPosition = GetRandomPositionOffScreen();
        AsteroidsSpawnen(spawnPosition);

        waitingTimer = waitingTime;

        spawnTimer = 0f;
    }

    private void Update()
    {
        asteroidSpawnTimer -= Time.deltaTime;
        asteroidCount = asteroidString.Count;

        if (asteroidString.Count <= 0)
        {
            stringCount++;
            asteroids = stringCount;
            spawnPosition = GetRandomPositionOffScreen();
            for (int i = stringCount; i > 0; i--)
            {
                AsteroidsSpawnen(spawnPosition);
            }                      
        }
        else if (asteroidsInScreen < asteroidCount)
        {
            if(spawnPosition.x - 1 > -11)
                AsteroidsSpawnen(spawnPosition - Vector3.right);
            else
            {
                AsteroidsSpawnen(spawnPosition + Vector3.right * 9);
            }
        }
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

        switch (0)
        {
            case 0: // top
                screenPosition = new Vector3(Random.Range(-paddingWidth, Screen.width + paddingWidth), Screen.height + paddingHeight);
                break;

            //case 1: // right
            //    screenPosition = new Vector3(Screen.width + paddingWidth, Random.Range(-paddingHeight, Screen.height + paddingHeight));
            //    break;

            //case 2: // bottom
            //    screenPosition = new Vector3(Random.Range(-paddingWidth, Screen.width + paddingWidth), -paddingHeight);
            //    break;

            //case 3: // left
            //    screenPosition = new Vector3(-paddingWidth, Random.Range(-paddingHeight, Screen.height + paddingHeight));
            //    break;
        }

        // convert from view port space to world space
        Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        spawnPosition.y = 0;
        return spawnPosition;
    }

    public void NotifyAsteroidDestroyed(AsteroidData asteroid)
    {
        asteroidString.Remove(asteroid.gameObject);
        asteroidsInScreen--;
        asteroids--;
    }


    public void AsteroidsSpawnen(Vector3 spawnPosition)
    {
        asteroidsInScreen++;

        GameObject asteroid = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);
        asteroidString.Add(asteroid);


    }
}
