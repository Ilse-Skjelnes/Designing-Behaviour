using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
    private Vector3 direction = Vector3.forward;
    [SerializeField]
    private int movementSpeed = 1;

    private float timer = 1f;
    public float timerMax = 1f;

    private void Awake()
    {
        timer = timerMax;
    }

    private void Update()
    {
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

        timer += Time.deltaTime;
        if (timer >= timerMax)
        {
            for (int i = AsteroidManager.Instance.segments.Count - 1; i > 0; i--)
            {
                AsteroidManager.Instance.segments[i].position = AsteroidManager.Instance.segments[i - 1].position;
            }
            this.transform.position = new Vector3(
                Mathf.Round((this.transform.position.x) + direction.x),
                0.0f,
                Mathf.Round((this.transform.position.z) + direction.z)
                );

            timer -= timerMax;
            transform.eulerAngles = new Vector3(0, GetAngleFromVector(direction), 0);
        }

        
    }
    private float GetAngleFromVector(Vector3 dir)
    {
        float n = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }
}
