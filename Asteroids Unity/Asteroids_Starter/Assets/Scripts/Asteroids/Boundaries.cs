using UnityEngine;

public class Boundaries : MonoBehaviour
{

    public float width = 8;
    public float height = 7;


    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < -width)
        {
            transform.position = new Vector3(width, transform.position.y, transform.position.z);
        }
        if (transform.position.x > width)
        {
            transform.position = new Vector3(-width, transform.position.y, transform.position.z);
        }
        if (transform.position.z < -height)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, height);
        }
        if (transform.position.z > height)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -height);
        }
    }
}
