using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour
{

    public SpriteRenderer ColorOn;
    public float fadeDuration = 1f; // Duration of the fade in and fade out
    public Sprite pressedSprite; 
    public Sprite normalSprite; 
    private SpriteRenderer buttonSpriteRenderer;
    internal object onClick;

    void Start()
    {
        if (ColorOn == null)
        {
            Debug.LogError("Sprite Renderer not assigned!");
        }
        else
        {
            ColorOn.color = new Color(ColorOn.color.r, ColorOn.color.g, ColorOn.color.b, 0f); // Start invisible
        }

        buttonSpriteRenderer = GetComponent<SpriteRenderer>();
        if (buttonSpriteRenderer == null)
        {
            Debug.LogError("Button Sprite Renderer not found!");
        }
        else if (normalSprite == null)
        {
            normalSprite = buttonSpriteRenderer.sprite; 
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("MouseDown");
        if (buttonSpriteRenderer != null && pressedSprite != null)
        {
            buttonSpriteRenderer.sprite = pressedSprite; 
        }
        StartCoroutine(Fader());
    }

    private IEnumerator Fader()
    {
        if (ColorOn == null) yield break; // Safety check

        // Fade In
        for (float t = 0; t <= 1; t += Time.deltaTime / fadeDuration)
        {
            ColorOn.color = new Color(ColorOn.color.r, ColorOn.color.g, ColorOn.color.b, t);
            yield return null;
        }

        if (buttonSpriteRenderer != null && normalSprite != null)
        {
            buttonSpriteRenderer.sprite = normalSprite;
        }

        // Fade Out
        for (float t = 1; t >= 0; t -= Time.deltaTime / fadeDuration)
        {
            ColorOn.color = new Color(ColorOn.color.r, ColorOn.color.g, ColorOn.color.b, t);
            yield return null;
        }
    }
}