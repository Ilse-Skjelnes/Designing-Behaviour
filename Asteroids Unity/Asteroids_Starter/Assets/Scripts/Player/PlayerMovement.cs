using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody body;

    [SerializeField]
    private Animator thrusterAnimator;

    [SerializeField]
    private float forwardThrustForce = 1.0f;

    [SerializeField]
    private float rotationSpeed = 1.0f;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //Forward thrusting logic
        bool isThrusting = Input.GetKey(KeyCode.W);

        if (isThrusting)
            body.AddRelativeForce(0, 0, forwardThrustForce * Time.deltaTime);

        if(thrusterAnimator != null)
            thrusterAnimator.SetBool("Thrusting", isThrusting);

        //Rotation logic
        if (Input.GetKey(KeyCode.A))
            transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0);
        else if (Input.GetKey(KeyCode.D))
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
