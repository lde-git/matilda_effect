using UnityEngine;

public interface ITextDropArea
{
    void OnTextDrop(Texty text);
    void OnTextRemoved(Texty text);
    bool CanDropText(Texty text);

}
