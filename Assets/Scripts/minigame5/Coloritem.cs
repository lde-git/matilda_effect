using UnityEngine;

public class Coloritem : MonoBehaviour
{
    private Collider2D col;
    private Vector3 startDragPosition;
    public ColoritemType coloritemType;
    public IColoritemDropArea currentArea;
    public ColoritemArea startingArea;

    void Start()
    {
        col = GetComponent<Collider2D>();
        
        // Store the initial position when the game starts and never change it
        startDragPosition = transform.position;
        
        if (startingArea != null)
        {
            this.SetCurrentArea(startingArea);
        }
    }

    private void SetCurrentArea(IColoritemDropArea coloritemDropArea) {
        this.currentArea = coloritemDropArea;
    }

    private void OnMouseDown() {
        Debug.Log("MouseDown");
    }

    private void OnMouseDrag() {
        transform.position = GetMousePositionInWorldSpace();

        if (this.currentArea != null) {
            currentArea.OnColoritemRemoved(this);
            this.SetCurrentArea(null);
        }
    }

    private void OnMouseUp() {
        col.enabled = false;
        Collider2D hitCollider = Physics2D.OverlapPoint(transform.position);
        col.enabled = true;

        if (hitCollider != null && hitCollider.TryGetComponent(out IColoritemDropArea coloritemDropArea)) {
            if (coloritemDropArea.CanDropColoritem(this)) { 
                coloritemDropArea.OnColoritemDrop(this);
                this.SetCurrentArea(coloritemDropArea);
                return; 
            }
        }

        Debug.Log($"{this.name} was not placed in a valid area. Returning to start position.");
        transform.position = startDragPosition;

        if (currentArea != null) {
            currentArea.OnColoritemRemoved(this);
            this.SetCurrentArea(null);
        }
    }

    public Vector3 GetMousePositionInWorldSpace() {
        Vector3 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        p.z = 0f;
        return p;
    }

    public void ResetToStartPosition()
    {
        Debug.Log($"{this.name} is resetting to its starting position.");
        transform.position = startDragPosition; 
    }
}
