using System;
using UnityEngine;

public class GearArea : MonoBehaviour, IGearDropArea
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
        //in layer set card above target for mouse collision
        gear.transform.position = transform.position + new Vector3(0,0,-1);
        this.hasGear = true;
        this.currentGear = gear;
        if (this.gearType != gear.gearType) { 
            Debug.Log($"wrong {gear.name} inserted");
            return;
        }

        this.hasCorrectGear = true;
        Debug.Log($"correct {gear.name} inserted");
        GearManager.Instance.OnGearPlaced();
    }


    public bool CanDropGear(Gear gear) {
        if (hasGear)
        {
            Debug.Log($"already has {this.currentGear.name} inserted \ncant insert {gear.name}");
            return false;
        }
        return true;
    }

    public void OnGearRemoved(Gear gear)
    {
        this.hasGear = false;
        this.currentGear = null;
        this.hasCorrectGear = false;
        Debug.Log($"{gear.name} removed");
    }
}
