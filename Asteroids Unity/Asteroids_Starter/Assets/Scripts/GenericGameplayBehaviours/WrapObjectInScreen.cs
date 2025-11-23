using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class WrapObjectInScreen : MonoBehaviour
{
    private SphereCollider sphereCollider;

    private float wrappingMargin = 1f;

    private void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
    }

    private void Update()
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
        Vector3 newPositionOffset = new Vector3();


        // for each off-screen position, place on other side of screen and add object's radius to spawn off-screen
        // checking with a wrappingMargin to prevent flickering between two sides
        if (rightEdgeScreenPos.x > Screen.width + wrappingMargin)
        {
            // wrap to left
            newScreenPos.x = 0;
            newPositionOffset.x -= sphereCollider.radius;
        }
        else if (leftEdgeScreenPos.x < 0 - wrappingMargin)
        {
            // wrap to right
            newScreenPos.x = Screen.width;
            newPositionOffset.x += sphereCollider.radius;
        }

        if (topEdgeScreenPos.y > Screen.height + wrappingMargin)
        {
            // wrap to bottom
            newScreenPos.y = 0;
            newPositionOffset.z -= sphereCollider.radius;
        }
        else if (bottomEdgeScreenPos.y < 0 - wrappingMargin)
        {
            // wrap to top
            newScreenPos.y = Screen.height;
            newPositionOffset.z += sphereCollider.radius;
        }

        // calculate final position and set object to new position
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(newScreenPos) + newPositionOffset;
        transform.position = newPosition;

    }
}
