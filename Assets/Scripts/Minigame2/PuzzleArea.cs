using System;
using UnityEngine;

public class PuzzleArea : MonoBehaviour, IPuzzleDropArea
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public PuzzleType puzzleType;
    public bool hasPuzzle = false;
    public bool hasCorrectPuzzle = false;
    public PuzzlePiece currentPuzzle;


    private void OnValidate()
    {
        this.name = $"PuzzleArea_{transform.GetSiblingIndex()}_{puzzleType}";
    }


    public void OnPuzzleDrop(PuzzlePiece puzzle) {

        puzzle.transform.SetParent(this.transform);
        //in layer set card above target for mouse collision
        puzzle.transform.position = transform.position + new Vector3(0,0,-1);
        this.hasPuzzle = true;
        this.currentPuzzle = puzzle;
        if (this.puzzleType != puzzle.puzzleType) { 
            Debug.Log($"wrong {puzzle.name} inserted");
            return;
        }

        this.hasCorrectPuzzle = true;
        Debug.Log($"correct {puzzle.name} inserted");
        PuzzleManager.Instance.OnPuzzlePlaced();
    }


    public bool CanDropPuzzle(PuzzlePiece puzzle) {
        if (hasPuzzle)
        {
            Debug.Log($"already has {this.currentPuzzle.name} inserted \ncant insert {puzzle.name}");
            return false;
        }
        return true;
    }

    public void OnPuzzleRemoved(PuzzlePiece puzzle)
    {
        this.hasPuzzle = false;
        this.currentPuzzle = null;
        this.hasCorrectPuzzle = false;
        Debug.Log($"{puzzle.name} removed");
    }
}
