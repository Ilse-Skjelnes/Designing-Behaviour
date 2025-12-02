using UnityEngine;
public class AsteroidData : MonoBehaviour
{
    [field: SerializeField]
    public Rigidbody Rigidbody {  get; private set; }
    public Transform TargetTransform {  get; private set; }
    public void SetFollowingTarget(Transform target)
    {
        TargetTransform = target;
    }
}
