using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    private Collider2D col;
    private Vector3 startDragPosition;
    public PuzzleType puzzleType;
    public IPuzzleDropArea currentArea;
    public PuzzleArea startingArea;

    void Start()
    {
        col = GetComponent<Collider2D>();
        
        startDragPosition = transform.position;

        if (startingArea != null)
        {
            this.SetCurrentArea(startingArea);
        }
    }

    private void SetCurrentArea(IPuzzleDropArea puzzleDropArea)
    {
        this.currentArea = puzzleDropArea;
    }

    private void OnMouseDown()
    {
        Debug.Log("MouseDown");
    }

    private void OnMouseDrag()
    {
        transform.position = GetMousePositionInWorldSpace();
        if (this.currentArea != null)
        {
            currentArea.OnPuzzleRemoved(this);
            this.SetCurrentArea(null);
        }
    }

    private void OnMouseUp()
    {
        col.enabled = false;
        Collider2D hitCollider = Physics2D.OverlapPoint(transform.position);
        col.enabled = true;

        if (hitCollider != null && hitCollider.TryGetComponent(out IPuzzleDropArea puzzleDropArea))
        {
            if (puzzleDropArea.CanDropPuzzle(this))
            {
                puzzleDropArea.OnPuzzleDrop(this);
                this.SetCurrentArea(puzzleDropArea);
                return;
            }
        }

        // If dropped outside any GearArea, reset to its initial position
        Debug.Log($"{this.name} was not placed in a valid area. Returning to start position.");
        transform.position = startDragPosition;

         // Ensure it is removed from any previous area
        if (currentArea != null) {
            currentArea.OnPuzzleRemoved(this);
            this.SetCurrentArea(null);
        }
    }

    public Vector3 GetMousePositionInWorldSpace()
    {
        Vector3 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        p.z = 0f;
        return p;
    }

    public void ResetToStartPosition()
    {
        Debug.Log($"{this.name} is resetting to its starting position.");
        transform.position = startDragPosition; // Move piece back to its original place
    }

}
