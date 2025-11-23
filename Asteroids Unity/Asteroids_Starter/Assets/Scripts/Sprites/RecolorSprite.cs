using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class RecolorSprite : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Color newColor;

    private Color originalColor;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        originalColor = spriteRenderer.color;
    }

    public void Recolor()
    {
        spriteRenderer.color = newColor;
    }

    public void ResetColor()
    {
        spriteRenderer.color = originalColor;
    }
}
