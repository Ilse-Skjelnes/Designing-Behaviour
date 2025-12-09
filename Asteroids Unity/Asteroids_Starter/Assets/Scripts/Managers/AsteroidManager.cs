using System.Collections;
using System.Collections.Generic;
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
    public int stringCount = 2;

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
        ResetTimer();
        SpawnAsteroidOffscreen();

        waitingTimer = waitingTime;
    }

    private void Update()
    {
        asteroidSpawnTimer -= Time.deltaTime;
        asteroidCount = asteroidString.Count;

        if (asteroidString.Count <= 0)
        {
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
        asteroidString.Add(asteroid);

        GameManager.Instance.score ++;
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

    public void NotifyAsteroidDestroyed(AsteroidData asteroid)
    {
        asteroidString.Remove(asteroid.gameObject);
    }

    private void SpawnAtFirstAsteroid(Vector3 firstAsteroidTransform)
    {   
        // instantiate new GO from prefab on position off screen
        GameObject asteroid = Instantiate(asteroidPrefab, firstAsteroidTransform, Quaternion.identity, transform);

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

    private void Grow(int asteroidAmount)
    {
        for (int i = 0; i < asteroidAmount; i++)
        {
            if (i == 0)
                continue;

            Vector3 spawnPosition = asteroidString[i - 1].transform.position;
            
            Vector3 addVector = Vector3.RotateTowards(addedVector, spawnPosition, 2 * Mathf.PI, 0.0f);
            
            Vector3 pos = spawnPosition + addVector;
            SpawnAtFirstAsteroid(pos);
        }
    }

    public void MakeAsteroidsMove(GameObject asteroid)
    {
        int listPlace = asteroidString.IndexOf(asteroid);

        if (listPlace > 0)
        {
            Vector3 currentPosition = asteroidString[listPlace].transform.position;
            Vector3 targetPosition = asteroidString[listPlace - 1].transform.position;
            Debug.Log(listPlace + "is following: " + (listPlace - 1));
            
            waitingTimer -= Time.deltaTime;
            if (waitingTimer <= 0)
            {
                asteroid.transform.position = Vector3.SmoothDamp(currentPosition, targetPosition, ref refVelocity, smoothTime);
                waitingTimer = waitingTime * listPlace;
                Debug.Log("WaitingTime is: " + waitingTimer);
            }

        }
    }
}
