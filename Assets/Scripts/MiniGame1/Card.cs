using UnityEngine;

public class Card : MonoBehaviour
{
    private Collider2D col;
    private Vector3 startDragPosition;
    public CardType cardType;
    public ICardDropArea currentArea;
    public Area startingArea;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        col = GetComponent<Collider2D>();
        this.SetCurrentArea(startingArea);
    }

    private void SetCurrentArea(ICardDropArea cardDropArea) {
        this.currentArea = cardDropArea;
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
        this.name = $"Card_{transform.GetSiblingIndex()}_{cardType}";
    }


    private void OnMouseDrag() {
        transform.position = GetMousePositionInWorldSpace();
        if (this.currentArea != null) {
            currentArea.OnCardRemoved(this);
            this.SetCurrentArea(null);
        } 
    }

    private void OnMouseUp() {
        col.enabled = false;
        Collider2D hitCollider = Physics2D.OverlapPoint(transform.position);

        col.enabled = true;
        if(hitCollider != null && hitCollider.TryGetComponent(out ICardDropArea cardDropArea)) {
            if (cardDropArea.CanDropCard(this)) { 
                cardDropArea.OnCardDrop(this);
                this.SetCurrentArea(cardDropArea);
                return;
            };
        } 
        //Card cant be placed 
        transform.position = startDragPosition;
    }

    public Vector3 GetMousePositionInWorldSpace() {
        Vector3 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        p.z = 0f;
        return p;
    }

}
