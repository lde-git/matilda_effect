using UnityEngine;

public class TextArea : MonoBehaviour, ITextDropArea
{
    public TextType textType;
    public bool hasText = false;
    public bool hasCorrectText = false;
    public Texty currentText;

    private void OnValidate()
    {
        this.name = $"TextArea_{transform.GetSiblingIndex()}_{textType}";
    }

    public void OnTextDrop(Texty text) {
        text.transform.SetParent(this.transform);
        text.transform.position = transform.position + new Vector3(0, 0, -1);
        this.hasText = true;
        this.currentText = text;

        if (this.textType != text.textType) {
            Debug.Log($"Wrong text ({text.name}) inserted.");
            this.hasCorrectText = false; // Set to false if incorrect
            return;
        }

        // Set to true if correct text
        this.hasCorrectText = true;
        Debug.Log($"Correct text ({text.name}) inserted.");
        TextManager.Instance.OnTextPlaced();
    }


    public bool CanDropText(Texty text) {
        if (hasText) {
            Debug.Log($"Already has text {this.currentText.name} inserted. Can't insert {text.name}.");
            return false;
        }
        return true;
    }

    public void OnTextRemoved(Texty text)
    {
        this.hasText = false;
        this.currentText = null;
        this.hasCorrectText = false;

        Debug.Log($"{text.name} removed from {this.name}. Returning to start position.");
        text.ResetToStartPosition(); // Call the method to reset its position
    }
}