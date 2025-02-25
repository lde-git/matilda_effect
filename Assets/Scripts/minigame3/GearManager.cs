using UnityEngine;
using Fungus;

public class GearManager : MonoBehaviour
{
    public static GearManager Instance;

    private int totalGears;

    [SerializeField] private Flowchart flowchartChap1; // Assign Flowchart_Chap_1 in Inspector
    public GearArea[] areas;

    void Awake()
    {
        Instance = this;
        totalGears = FindObjectsOfType<Gear>().Length; 
    }

    public void OnGearPlaced()
    {
        if (areas == null || areas.Length == 0) return;

        bool allCorrect = true;

        // Check every area to see if it has the correct gear
        foreach (var area in areas)
        {
            if (!area.hasCorrectGear) {
                allCorrect = false;
                break;
            }
        }

        // Only call the success block if all gears are correct
        if (allCorrect)
        {
            Debug.Log("All correct gears placed! Executing 'Gear_solved'.");
            CallGameSuccessBlock();
        }
        else
        {
            Debug.Log("Not all gears are placed correctly yet.");
        }
    }


    private void CallGameSuccessBlock()
    {
        if (flowchartChap1 != null)
        {
            flowchartChap1.ExecuteBlock("Gear_solved");
        }
        else
        {
            Debug.LogError("Gear_solved is not assigned in the Inspector!");
        }
    }
}
