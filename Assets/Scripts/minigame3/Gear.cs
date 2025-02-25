using UnityEngine;

public class Gear : MonoBehaviour
{
    private Collider2D col;
    private Vector3 startDragPosition;
    public GearType gearType;
    public IGearDropArea currentArea;
    public GearArea startingArea;

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

    private void SetCurrentArea(IGearDropArea gearDropArea) {
        this.currentArea = gearDropArea;
    }

    private void OnMouseDown() {
        Debug.Log("MouseDown");
    }

    private void OnMouseDrag() {
        transform.position = GetMousePositionInWorldSpace();

        if (this.currentArea != null) {
            currentArea.OnGearRemoved(this);
            this.SetCurrentArea(null);
        }
    }

    private void OnMouseUp() {
        col.enabled = false;
        Collider2D hitCollider = Physics2D.OverlapPoint(transform.position);
        col.enabled = true;

        if (hitCollider != null && hitCollider.TryGetComponent(out IGearDropArea gearDropArea)) {
            if (gearDropArea.CanDropGear(this)) { 
                gearDropArea.OnGearDrop(this);
                this.SetCurrentArea(gearDropArea);
                return; // Gear placed correctly, so exit
            }
        }

        // If dropped outside any GearArea, reset to its initial position
        Debug.Log($"{this.name} was not placed in a valid area. Returning to start position.");
        transform.position = startDragPosition;

        // Ensure it is removed from any previous area
        if (currentArea != null) {
            currentArea.OnGearRemoved(this);
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
        transform.position = startDragPosition; // Move gear back to its original place
    }
}
