using UnityEngine;

public interface IGearDropArea
{
    void OnGearDrop(Gear gear);
    void OnGearRemoved(Gear gear);

    bool CanDropGear(Gear gear);

}
