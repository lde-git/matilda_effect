using UnityEngine;
using Fungus;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private int totalCards;
    //private int placedCards = 0;

    [SerializeField] private Flowchart flowchartStart; // Assign Flowchart_Start in Inspector
    public Area[] areas;

    void Awake()
{
    Instance = this;
    totalCards = FindObjectsOfType<Card>().Length; // Count all cards in the scene
}

    public void OnCardPlaced()
    {
        if (areas == null || areas.Length == 0) return;

        foreach (var area in areas)
        {
            if (!area.hasCard) return;
        }
        // Call the "Game_success" block in the Flowchart_Start flowchart
        CallGameSuccessBlock();
    }

    private void CallGameSuccessBlock()
    {
        if (flowchartStart != null)
        {
            flowchartStart.ExecuteBlock("Game_success");
        }
        else
        {
            Debug.LogError("Flowchart_Start is not assigned in the Inspector!");
        }
    }
}
