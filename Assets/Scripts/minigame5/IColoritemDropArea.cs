using UnityEngine;

public interface IColoritemDropArea
{
    void OnColoritemDrop(Coloritem coloritem);
    void OnColoritemRemoved(Coloritem coloritem);
    bool CanDropColoritem(Coloritem coloritem);

}
