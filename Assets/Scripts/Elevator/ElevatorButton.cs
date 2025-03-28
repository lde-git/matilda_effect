using UnityEngine;
using UnityEngine.EventSystems;

public class ElevatorButton : MonoBehaviour 
{
    public int number;
    public ElevatorManager elevatorManager;
    private PolygonCollider2D polygonCollider;
    



    void Start()
    {
        polygonCollider = GetComponent<PolygonCollider2D>();
        if (polygonCollider == null)
        {
            Debug.LogError("PolygonCollider2D not found on " + gameObject.name);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (polygonCollider != null && polygonCollider.OverlapPoint(mousePosition))
            {
                elevatorManager.AddNumber(number);  // Directly call AddNumber

            }
        }
    }
}