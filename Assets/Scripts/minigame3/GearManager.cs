using UnityEngine;
using Fungus;
using System.Collections;

public class GearManager : MonoBehaviour
{
    public static GearManager Instance;

    public GameObject[] staticGears;

    private int totalGears;
    [SerializeField] private Flowchart flowchartChap1; // Assign Flowchart in Inspector
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

        foreach (var area in areas)
        {
            if (!area.hasCorrectGear)
            {
                allCorrect = false;
                break;
            }
        }

        if (allCorrect)
        {
            Debug.Log("All correct gears placed! Executing 'Gear_solved' and starting rotation.");
            StartCoroutine(RotateGearsForSeconds(5f)); // 5 second animation
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

    private IEnumerator RotateGearsForSeconds(float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            foreach (var area in areas)
            {
                if (area.currentGear != null)
                {
                    area.currentGear.transform.Rotate(0, 0, 100 * Time.deltaTime);
                }
            }

            foreach (var gear in staticGears)
            {
                if (gear != null)
                {
                    gear.transform.Rotate(0, 0, 100 * Time.deltaTime);
                }
            }

            elapsedTime += Time.deltaTime;
            yield return null; 
        }

        Debug.Log("Gear rotation finished! Now executing Gear_solved.");
        CallGameSuccessBlock();
    }
}

