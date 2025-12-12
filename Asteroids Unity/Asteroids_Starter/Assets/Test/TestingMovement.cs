using UnityEngine;

public class TestingMovement : MonoBehaviour
{

    public float maxMoveX = 5f;
    public float maxMoveZ = 5f;

    public float addedMoveX = 1f;
    public float addedMoveZ = 1f;

    float addedMoverX = 0f;
    float addedMoverZ = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        addedMoverX = addedMoverX + addedMoveX * Time.deltaTime;
        addedMoverZ = addedMoverZ + addedMoveZ * Time.deltaTime;
        Vector3 sinMovement = new Vector3(maxMoveX * Mathf.Sin(Time.time), 1, maxMoveZ * Mathf.Sin(Time.time));
        Vector3 addedMovement = new Vector3(addedMoverX * maxMoveZ, 0, -addedMoverZ * maxMoveX);
        transform.position = addedMovement + sinMovement;
    }
}
