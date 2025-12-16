using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{

    private SphereCollider sphereCollider;

    private float wrappingMargin = 1f;

    [SerializeField] private float maxMoveX = 1f;
    [SerializeField] private float maxMoveZ = 1f;

    [SerializeField] private float addedMoveX = 1f;
    [SerializeField] private float addedMoveZ = 1f;

    private float addedMoverX = 0f;
    private float addedMoverZ = 0f;
    private Vector3 spawnPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnPosition = AsteroidManager.Instance.spawnPosition;

        sphereCollider = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        // get position of object on screen
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);

        // for each edge of the screen calculate screen position, taking into account the radius of the object
        Vector3 rightEdgeScreenPos = Camera.main.WorldToScreenPoint(transform.position + new Vector3(-sphereCollider.radius, 0, 0));
        Vector3 leftEdgeScreenPos = Camera.main.WorldToScreenPoint(transform.position + new Vector3(sphereCollider.radius, 0, 0));
        Vector3 topEdgeScreenPos = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 0, -sphereCollider.radius));
        Vector3 bottomEdgeScreenPos = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 0, sphereCollider.radius));

        // set up new position variables to use in wrapping logic
        Vector3 newScreenPos = screenPos;

        // for each off-screen position, place on other side of screen and add object's radius to spawn off-screen
        // checking with a wrappingMargin to prevent flickering between two sides
        //if (rightEdgeScreenPos.x > Screen.width)
        //{

        //    // wrap to left
        //    AsteroidsOutScreen();
        //}
        if (leftEdgeScreenPos.x < 0)
        {
            // wrap to right
            AsteroidsOutScreen();
        }

        //if (topEdgeScreenPos.y > Screen.height + wrappingMargin)
        //{
        //    // wrap to bottom
        //    AsteroidsOutScreen();
        //}
        if (bottomEdgeScreenPos.y < 0)
        {
            // wrap to top
            Debug.Log("Asteroid left screen though bottom");
            AsteroidsOutScreen();
        }

        RandomMovement(spawnPosition.x, spawnPosition.z);
    }

    private void OnTriggerExit(Collider other)
    {
        AsteroidsOutScreen();
    }

    private void OnTriggerEnter(Collider other)
    {
        AsteroidsOutScreen();
    }

    private void OnCollisionEnter(Collision collision)
    {
        AsteroidsOutScreen();
    }

    private void RandomMovement(float spawnPointX, float spawnPointZ)
    {
        addedMoverX = addedMoverX + addedMoveX * Time.deltaTime;
        addedMoverZ = addedMoverZ + addedMoveZ * Time.deltaTime;

        Vector3 sinMovement = new Vector3(maxMoveX * Mathf.Sin(Time.time) + spawnPointX , 0, maxMoveZ * Mathf.Sin(Time.time) + spawnPointZ);
        Vector3 addedMovement = new Vector3(addedMoverX * maxMoveZ, 0, -addedMoverZ * maxMoveX);

        transform.position = addedMovement + sinMovement;
    }

    private void AsteroidsOutScreen()
    {
        Debug.Log("Asteroids left the screen");

        AsteroidManager.Instance.asteroidsInScreen--;
        AsteroidManager.Instance.asteroids.Remove(gameObject);
        Destroy(gameObject);
    }
}
