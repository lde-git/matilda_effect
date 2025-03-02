using System;
using UnityEngine;

public class PuzzleArea : MonoBehaviour, IPuzzleDropArea
{
    public PuzzleType puzzleType;
    public bool hasPuzzle = false;
    public bool hasCorrectPuzzle = false;
    public PuzzlePiece currentPuzzle;

    private void OnValidate()
    {
        this.name = $"PuzzleArea_{transform.GetSiblingIndex()}_{puzzleType}";
    }

    public void OnPuzzleDrop(PuzzlePiece puzzle) 
    {
        puzzle.transform.SetParent(this.transform); // Ensure the puzzle is a child of this area
        
        puzzle.transform.position = transform.position + new Vector3(0, 0, -1);
        this.hasPuzzle = true;
        this.currentPuzzle = puzzle;

        if (this.puzzleType != puzzle.puzzleType) 
        { 
            Debug.Log($"Wrong {puzzle.name} inserted");
            this.hasCorrectPuzzle = false;
            return;
        }

        this.hasCorrectPuzzle = true;
        Debug.Log($"Correct {puzzle.name} inserted");
        PuzzleManager.Instance.OnPuzzlePlaced();
    }

    public bool CanDropPuzzle(PuzzlePiece puzzle) 
    {
        if (hasPuzzle)
        {
            Debug.Log($"Already has {this.currentPuzzle.name} inserted.\nCan't insert {puzzle.name}");
            return false;
        }
        return true;
    }

   public void OnPuzzleRemoved(PuzzlePiece puzzle)
    {
        this.hasPuzzle = false;
        this.currentPuzzle = null;
        this.hasCorrectPuzzle = false;

        Debug.Log($"{puzzle.name} removed from {this.name}. Returning to start position.");
        puzzle.ResetToStartPosition();
    }

}
