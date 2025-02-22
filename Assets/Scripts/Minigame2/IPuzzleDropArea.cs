using UnityEngine;

public interface IPuzzleDropArea
{
    void OnPuzzleDrop(PuzzlePiece piece);
    void OnPuzzleRemoved(PuzzlePiece piece);

    bool CanDropPuzzle(PuzzlePiece piece);

}
