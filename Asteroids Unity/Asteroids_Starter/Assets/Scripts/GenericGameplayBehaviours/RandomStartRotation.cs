using UnityEngine;

public class RandomStartRotation : MonoBehaviour
{
    private void Start()
    {
        //Give object random rotation
        float randomRotation = Random.value * 360f;
        transform.Rotate(0, randomRotation, 0);
    }
}
