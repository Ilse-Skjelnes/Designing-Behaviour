using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    private void Update()
    {
        AsteroidManager.Instance.MakeAsteroidsMove(gameObject);
    }
}

