using UnityEngine;

public class GearArea : MonoBehaviour, IGearDropArea
{
    public GearType gearType;
    public bool hasGear = false;
    public bool hasCorrectGear = false;
    public Gear currentGear;

    private void OnValidate()
    {
        this.name = $"GearArea_{transform.GetSiblingIndex()}_{gearType}";
    }

    public void OnGearDrop(Gear gear) {
        gear.transform.SetParent(this.transform);
        // Position the gear inside the area, slightly below to prevent overlap
        gear.transform.position = transform.position + new Vector3(0, 0, -1);
        this.hasGear = true;
        this.currentGear = gear;

        // Check if the gear is correct and update the flag
        if (this.gearType != gear.gearType) {
            Debug.Log($"Wrong gear ({gear.name}) inserted.");
            this.hasCorrectGear = false; // Set to false if incorrect
            return;
        }

        // Set to true if correct gear
        this.hasCorrectGear = true;
        Debug.Log($"Correct gear ({gear.name}) inserted.");
        GearManager.Instance.OnGearPlaced(); // Call GearManager to check if all gears are correctly placed
    }


    public bool CanDropGear(Gear gear) {
        if (hasGear) {
            Debug.Log($"Already has gear {this.currentGear.name} inserted. Can't insert {gear.name}.");
            return false;
        }
        return true;
    }

    public void OnGearRemoved(Gear gear)
    {
        this.hasGear = false;
        this.currentGear = null;
        this.hasCorrectGear = false;

        Debug.Log($"{gear.name} removed from {this.name}. Returning to start position.");
        gear.ResetToStartPosition(); // Call the method to reset its position
    }
}