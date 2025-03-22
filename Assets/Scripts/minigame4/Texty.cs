using UnityEngine;

public class Texty : MonoBehaviour
{
    private Collider2D col;
    private Vector3 startDragPosition;
    public TextType textType;
    public ITextDropArea currentArea;
    public TextArea startingArea;

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

    private void SetCurrentArea(ITextDropArea textDropArea) {
        this.currentArea = textDropArea;
    }

    private void OnMouseDown() {
        Debug.Log("MouseDown");
    }

    private void OnMouseDrag() {
        transform.position = GetMousePositionInWorldSpace();

        if (this.currentArea != null) {
            currentArea.OnTextRemoved(this);
            this.SetCurrentArea(null);
        }
    }

    private void OnMouseUp() {
        col.enabled = false;
        Collider2D hitCollider = Physics2D.OverlapPoint(transform.position);
        col.enabled = true;

        if (hitCollider != null && hitCollider.TryGetComponent(out ITextDropArea textDropArea)) {
            if (textDropArea.CanDropText(this)) { 
                textDropArea.OnTextDrop(this);
                this.SetCurrentArea(textDropArea);
                return; 
            }
        }

        Debug.Log($"{this.name} was not placed in a valid area. Returning to start position.");
        transform.position = startDragPosition;

        if (currentArea != null) {
            currentArea.OnTextRemoved(this);
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
