using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
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

    [SerializeField]
    public float minForceMagnitudeTowardsCenter = 0.5f;

    [SerializeField]
    public float maxForceMagnitudeTowardsCenter = 1f;

    public List<GameObject> asteroids = new List<GameObject>();
    public List<Transform> spawnPostions = new List<Transform>();
    public int snakeCount = 0;

    static public int asteroidScore;
    public int asteroidCount;
    private int asteroid;

    private float spawnTimer;
    [SerializeField]
    private float spawnTime = 3;
    public Vector3 spawnPosition;

    public int asteroidsInScreen;
    public bool startGame = false;
    
    private void Awake()
    {
        // setup singleton
        if (instance != null)
            Destroy(instance.gameObject);
        instance = this;

        asteroidCount = 0;

        spawnPosition = GetRandomPositionOffScreen();
    }

    private void Start()
    {
        spawnTimer = 0f;
    }

    private void Update()
    {
        spawnTimer -= Time.deltaTime;

        GameManager.Instance.PlayCutScene(snakeCount);

        //Spawn the head of the asteroid snake
        if (asteroidCount <= 0)
        {
            snakeCount++;
            GameManager.Instance.cutSceneTimer = GameManager.Instance.cutSceneTime;

            asteroidCount = snakeCount;
            asteroid = 1;

            spawnPosition = GetRandomPositionOffScreen();
            AsteroidsSpawnen(spawnPosition);
        }

        //Spawn the body of the Snake
        if (asteroid < snakeCount)
        {
            AsteroidsSpawnen(spawnPosition);
            asteroid++;
        }
        
        if (asteroidsInScreen < asteroidCount)
        {
            if (spawnPosition.x - 1 > -11)
                AsteroidsSpawnen(spawnPosition - Vector3.right);
            else
            {
                AsteroidsSpawnen(spawnPosition + Vector3.right);
            }
        }
    }

    private Vector3 GetRandomPositionOffScreen()
    {
        // randomly choose which side to spawn
        int side = Random.Range(0, 2);

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
        asteroids.Remove(asteroid.gameObject);
        asteroidsInScreen--;
    }

    public void AsteroidsSpawnen(Vector3 spawnPosition)
    {

        if (spawnTimer <= 0)
        {
            asteroidsInScreen++;

            GameObject asteroid = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);
            asteroids.Add(asteroid);

            spawnTimer = spawnTime;
        }
    }
}
