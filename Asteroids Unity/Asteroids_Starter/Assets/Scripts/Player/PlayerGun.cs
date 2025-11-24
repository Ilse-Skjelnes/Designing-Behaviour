using System;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private float bulletSpeed = 1.0f;

    [SerializeField]
    private GameObject bulletOrigin;

    [SerializeField]
    private float shootingCooldown = 1.0f;

    private float shootingCooldownTimer = 0.0f;

    private GameObject bullet;

    private Vector3 direction = Vector3.forward;
    private Vector3 directionR;

    private void Update()
    {
        // track cooldown between shots
        shootingCooldownTimer -= Time.deltaTime;

        // shoot if pressing button and shooting not on cooldown
        if (Input.GetKey(KeyCode.Space) && shootingCooldownTimer <= 0)
        {
            Shoot();
        }

        bullet.transform.position = new Vector3(
            Mathf.Round(bullet.transform.position.x + directionR.x / bulletSpeed),
            0.0f,
            Mathf.Round(bullet.transform.position.z + directionR.z / bulletSpeed)
            );

        if (shootingCooldownTimer <= 0)
        {
            directionR = direction;
        }
        
        if (Input.GetKeyDown(KeyCode.W))
            {
                direction = Vector3.forward;
            }
        else if (Input.GetKeyDown(KeyCode.S))
            {
                direction = Vector3.back;
            }
        else if (Input.GetKeyDown(KeyCode.D))
            {
                direction = Vector3.right;
            }
        else if (Input.GetKeyDown(KeyCode.A))
            {
                direction = Vector3.left;
            }


    }
    private void Shoot()
    {
        // create bullet at bullet origin's location and rotation, and launch with speed
        bullet = GameObject.Instantiate(bulletPrefab, bulletOrigin.transform.position, bulletOrigin.transform.rotation);
        //bullet.GetComponent<Rigidbody>().AddRelativeForce(0,0,bulletSpeed);

        // reset shooting cooldown
        shootingCooldownTimer = shootingCooldown;
    }

    private void FixedUpdate()
    {
        
    }
}
