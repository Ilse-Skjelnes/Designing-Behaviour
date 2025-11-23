using UnityEngine;

public class PlayerDeathHandler : MonoBehaviour
{    
    private void HandleDeath()
    {
        GameManager.Instance.NotifyPlayerDeath();
    }
}
