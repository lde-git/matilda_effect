using UnityEngine;

public class Card : MonoBehaviour
{
    private Collider2D col;
    private Vector3 startDragPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        col = GetComponent<Collider2D>();
    }

    private void OnMouseDown() {
        startDragPosition = transform.position;
        transform.position = GetMousePositionInWorldSpace();
    }
    // Update is called once per frame

    private void OnMouseDrag() {
        transform.position = GetMousePositionInWorldSpace();
    }

    private void OnMouseUp() {
        col.enabled = false;
        Collider2D hitCollider = Physics2D.OverlapPoint(transform.position);
        col.enabled = true;
        if(hitCollider != null && hitCollider.TryGetComponent(out ICardDropArea cardDropArea)) {
            cardDropArea.OnCardDrop(this);
        } else {
            transform.position = startDragPosition;
        }
    }

    public Vector3 GetMousePositionInWorldSpace() {
        Vector3 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        p.z = 0f;
        return p;
    }

}
