using System.Runtime.CompilerServices;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
    private Vector3 direction = Vector3.forward;
    [SerializeField]
    private int movementSpeed = 1;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            direction = Vector3.forward;
            transform.Rotate(0, 0, 0, Space.World);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            direction = Vector3.back;
            transform.Rotate(0, 180, 0, Space.World);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            direction = Vector3.right;
            transform.Rotate(0, 90, 0, Space.World);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            direction = Vector3.left;
            transform.Rotate(0, -90, 0, Space.World);
        }

        
    }

    private void FixedUpdate()
    {
        this.transform.position = new Vector3(
            Mathf.Round((this.transform.position.x) + direction.x),
            0.0f,
            Mathf.Round((this.transform.position.z) + direction.z)
            );
    }
}
