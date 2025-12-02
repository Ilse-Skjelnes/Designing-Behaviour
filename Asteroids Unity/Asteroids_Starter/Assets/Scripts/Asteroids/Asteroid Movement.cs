using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    private float asteroidMovementTimer;
    [SerializeField]
    private float asteroidMovementTime = 5f;

    // Update is called once per frame
    void Update()
    {     
        //leadAsteroidTransform.Add(AsteroidManager.Instance.asteroidString[0].transform);
        asteroidMovementTimer -= Time.deltaTime;
        AddFollowingForce();
        if (asteroidMovementTimer <= 0)
        {
            
        }
        
    }

    private void AddFollowingForce()
    {
        if (asteroidMovementTimer <= 0)
        {
            for (int i = 0; i < AsteroidManager.Instance.asteroidString.Count; i++)
            {
                if (i == 0)
                    continue;
                Vector3 direction = AsteroidManager.Instance.asteroids[i].TargetTransform.position - AsteroidManager.Instance.asteroids[i].transform.position;
                AsteroidManager.Instance.asteroids[i].Rigidbody.AddForce(direction.normalized * AsteroidManager.Instance.AstreroidfollowingForce * Time.deltaTime);
            }
        }

        if (asteroidMovementTimer <= 0)
        {
            for (int i = AsteroidManager.Instance.asteroidString.Count - 1; i > 0; i--)
            {
                if (i <= 0)
                    continue;
                Vector3 realPos = AsteroidManager.Instance.asteroidString[i - 1].transform.position;
                AsteroidManager.Instance.asteroidString[i].transform.position = realPos;

                asteroidMovementTimer = asteroidMovementTime;
            }
        }
    }
}
