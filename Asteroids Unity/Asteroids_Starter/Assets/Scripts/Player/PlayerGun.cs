using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Unity.VisualScripting;
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

    private float timer = 1f;
    public float timerMax = 1.0f;

    public AudioSource source;
    public List<AudioClip> clips = new List<AudioClip>();

    private void Awake()
    {
        timer = timerMax;
    }
    private void Update()
    {
        // track cooldown between shots
        shootingCooldownTimer -= Time.deltaTime;

        // shoot if pressing button and shooting not on cooldown
        if (Input.GetKeyDown(KeyCode.Space) && shootingCooldownTimer <= 0)
        {
            PlaySound();
            Shoot();
        }
    }

    private void Shoot()
    {
        // create bullet at bullet origin's location and rotation, and launch with speed
        bullet = GameObject.Instantiate(bulletPrefab, bulletOrigin.transform.position, bulletOrigin.transform.rotation);
        bullet.GetComponent<Rigidbody>().AddRelativeForce(0,0,bulletSpeed);

        // reset shooting cooldown
        shootingCooldownTimer = shootingCooldown;
    }

    private void PlaySound()
    {
        int i = Random.Range(0, clips.Count + 1);
        source.clip = clips[i];
        source.Play();
    }
}
