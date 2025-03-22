using UnityEngine;

public class ColoritemArea : MonoBehaviour, IColoritemDropArea
{
    public ColoritemType coloritemType;
    public bool hasColoritem = false;
    public bool hasCorrectColoritem = false;
    public Coloritem currentColoritem;

    private void OnValidate()
    {
        this.name = $"ColoritemArea_{transform.GetSiblingIndex()}_{coloritemType}";
    }

    public void OnColoritemDrop(Coloritem coloritem) {
        coloritem.transform.SetParent(this.transform);
        coloritem.transform.position = transform.position + new Vector3(0, 0, -1);
        this.hasColoritem = true;
        this.currentColoritem = coloritem;

        if (this.coloritemType != coloritem.coloritemType) {
            Debug.Log($"Wrong color ({coloritem.name}) inserted.");
            this.hasCorrectColoritem = false; // Set to false if incorrect
            return;
        }

        // Set to true if correct color
        this.hasCorrectColoritem = true;
        Debug.Log($"Correct color ({coloritem.name}) inserted.");
        ColoritemManager.Instance.OnColoritemPlaced();
    }


    public bool CanDropColoritem(Coloritem coloritem) {
        if (hasColoritem) {
            Debug.Log($"Already has color {this.currentColoritem.name} inserted. Can't insert {coloritem.name}.");
            return false;
        }
        return true;
    }

    public void OnColoritemRemoved(Coloritem coloritem)
    {
        this.hasColoritem = false;
        this.currentColoritem = null;
        this.hasCorrectColoritem = false;

        Debug.Log($"{coloritem.name} removed from {this.name}. Returning to start position.");
        coloritem.ResetToStartPosition(); // Call the method to reset its position
    }
}