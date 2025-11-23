using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RandomStartForceTowardsCenter : MonoBehaviour
{
    public float minForceMagnitude = 0.5f;
    public float maxForceMagnitude = 1f;

    private void Start()
    {
        // determine direction vector towards center
        Vector3 direction = -transform.position.normalized;

        // pick random magnitude
        float forceMagnitude = Random.Range(minForceMagnitude, maxForceMagnitude);

        // apply force in given direction with given magnitude 
        GetComponent<Rigidbody>().AddForce(direction * forceMagnitude, ForceMode.VelocityChange);
    }
}
