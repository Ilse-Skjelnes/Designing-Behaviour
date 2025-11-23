using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class HitPoints : MonoBehaviour
{
    [SerializeField]
    private int maxHitPoints = 2;

    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private GameObject objectToSpawnWhenDamaged = null;

    [SerializeField]
    private GameObject objectToSpawnOnDeath = null;

    [SerializeField]
    private float invulnerabilityTimeAfterDamage = 0.5f;
    private float invulnerabilityTimer = 0.0f;

    public UnityEvent onInvulnerabilityEnabled;
    public UnityEvent onInvulnerabilityDisabled;

    private int currentHitpoints = 0;
    public int CurrentHitPoints { get { return currentHitpoints; } }

    private Collider objectCollider;

    private void Start()
    {
        // initialize hitpoints
        currentHitpoints = maxHitPoints;

        // cache collider
        objectCollider = GetComponent<Collider>();
    }

    private void Update()
    {
        invulnerabilityTimer -= Time.deltaTime;

        if (invulnerabilityTimer < 0.0f)
            DisableInvulnerability();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // take damage when colliding with objects in layer mask
        if (LayerHelper.IsInLayerMask(collision.gameObject,layerMask))
            TakeDamage();
    }

    private void OnTriggerEnter(Collider other)
    {
        // take damage when triggering with objects in layer mask
        if (LayerHelper.IsInLayerMask(other.gameObject, layerMask))
            TakeDamage();
    }

    private void TakeDamage()
    {
        // don't take any damage if invulnerable
        if (invulnerabilityTimer > 0.0f)
            return;

        currentHitpoints--;

        // instantiate new object when hit (usually for VFX)
        if (objectToSpawnWhenDamaged != null)
            GameObject.Instantiate<GameObject>(objectToSpawnWhenDamaged, transform.position, transform.rotation);

        if (currentHitpoints <= 0)
        {
            Die();
            return;
        }

        // enable invulnerability after taking damage
        EnableInvulnerability();
    }

    private void EnableInvulnerability()
    {
        // don't do anything if invulnerability time is zero or lower
        if (invulnerabilityTimeAfterDamage <= 0.0f) 
            return;

        invulnerabilityTimer = invulnerabilityTimeAfterDamage;
        
        // disable collision to temporarily prevent additional collision
        objectCollider.enabled = false;

        if (onInvulnerabilityEnabled != null)
            onInvulnerabilityEnabled.Invoke();
    }
    private void DisableInvulnerability()
    {
        // re-enable collision
        objectCollider.enabled = true;

        if (onInvulnerabilityDisabled != null)
            onInvulnerabilityDisabled.Invoke();
    }

    private void Die()
    {
        // instantiate new object before dying (usually for VFX)
        if (objectToSpawnOnDeath != null)
            GameObject.Instantiate<GameObject>(objectToSpawnOnDeath, transform.position, transform.rotation);

        SendMessage("HandleDeath",SendMessageOptions.DontRequireReceiver);

        Destroy(gameObject);
    }
}
