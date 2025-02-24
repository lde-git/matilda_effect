using UnityEngine;

public class Gear : MonoBehaviour
{
    private Collider2D col;
    private Vector3 startDragPosition;
    public GearType gearType;
    public IGearDropArea currentArea;
    public GearArea startingArea;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        col = GetComponent<Collider2D>();
        this.SetCurrentArea(startingArea);
    }

    private void SetCurrentArea(IGearDropArea gearDropArea) {
        this.currentArea = gearDropArea;
        if (currentArea == null) {
            return;
        }
        return;
    }


    private void OnMouseDown() {
        Debug.Log("MouseDown");
        var mouseWorldPosition = GetMousePositionInWorldSpace();
        startDragPosition = transform.position;
        transform.position = mouseWorldPosition;
        Debug.Log(mouseWorldPosition);
    }
    // Update is called once per frame

    private void OnValidate()
    {
        this.name = $"Gear_{transform.GetSiblingIndex()}_{gearType}";
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
        if(hitCollider != null && hitCollider.TryGetComponent(out IGearDropArea gearDropArea)) {
            if (gearDropArea.CanDropGear(this)) { 
                gearDropArea.OnGearDrop(this);
                this.SetCurrentArea(gearDropArea);
                return;
            };
        } 

        transform.position = startDragPosition;
    }

    public Vector3 GetMousePositionInWorldSpace() {
        Vector3 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        p.z = 0f;
        return p;
    }

}
