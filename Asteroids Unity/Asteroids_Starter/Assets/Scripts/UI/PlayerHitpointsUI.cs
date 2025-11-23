using TMPro;
using UnityEngine;

[RequireComponent (typeof(TextMeshProUGUI))]
public class PlayerHitpointsUI : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    private HitPoints playerHitPoints;

    private void Start()
    {
        // cache components
        textMesh = GetComponent<TextMeshProUGUI>();
        playerHitPoints = GameManager.Instance.Player.GetComponent<HitPoints>();
    }
    private void Update()
    {
        // update hit points UI text
        if (playerHitPoints != null)
            textMesh.text = "HP: " + playerHitPoints.CurrentHitPoints.ToString();
    }
}
