using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class LimitRigidbodyVelocity : MonoBehaviour
{
    public float maxSpeed;

    private Rigidbody body;

    private void Start()
    {
        // get a reference to attached body
        body = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // when velocity exceeds given max, clamp it to limit it
        if (body.linearVelocity.magnitude > maxSpeed)
            body.linearVelocity = Vector3.ClampMagnitude(body.linearVelocity, maxSpeed);
    }

}
