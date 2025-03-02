using UnityEngine;
using Fungus;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance;

    private int totalPuzzlePieces;

    [SerializeField] private Flowchart flowchartChap1; // Assign Flowchart_Chap_1 in Inspector
    public PuzzleArea[] areas;

    void Awake()
    {
        Instance = this;
        totalPuzzlePieces = FindObjectsOfType<PuzzlePiece>().Length; 
    }

    public void OnPuzzlePlaced()
    {
        if (areas == null || areas.Length == 0) return;

        bool allCorrect = true;
        
        foreach (var area in areas)
        {
            if (!area.hasCorrectPuzzle)
            {
                allCorrect = false;
                break;
            }
        }
        if (allCorrect) {
            Debug.Log("All correct puzzle pieces placed! Executing 'Puzzle_solved'.");
            // Call the "Game_success" block in the Flowchart_Start flowchart
            CallGameSuccessBlock();
        }
        else
        {
            Debug.Log("Not all pieces are placed correctly yet.");
        }
        
    }

    private void CallGameSuccessBlock()
    {
        if (flowchartChap1 != null)
        {
            flowchartChap1.ExecuteBlock("Puzzle_solved");
        }
        else
        {
            Debug.LogError("Puzzle_solved is not assigned in the Inspector!");
        }
    }
}
