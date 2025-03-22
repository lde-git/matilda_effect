using UnityEngine;
using Fungus;
using System.Collections;

public class ColoritemManager : MonoBehaviour
{
    public static ColoritemManager Instance;

    private int totalColoritems;
    [SerializeField] private Flowchart flowchartEpilog; // Assign Flowchart in Inspector
    public ColoritemArea[] coloritemAreas;

    void Awake()
    {
        Instance = this;
        totalColoritems = FindObjectsOfType<Coloritem>().Length;
    }

    public void OnColoritemPlaced()
    {
        if (coloritemAreas == null || coloritemAreas.Length == 0) return;

        bool allCorrect = true;

        foreach (var area in coloritemAreas)
        {
            if (!area.hasCorrectColoritem)
            {
                allCorrect = false;
                break;
            }
        }

        if (allCorrect)
        {
            Debug.Log("All correct color-items placed! Executing 'Final_solved'.");
            CallGameSuccessBlock();
        }
        else
        {
            Debug.Log("Not all color-items are placed correctly yet.");
        }
    }

    private void CallGameSuccessBlock()
    {
        if (flowchartEpilog != null)
        {
            flowchartEpilog.ExecuteBlock("Final_solved");
        }
        else
        {
            Debug.LogError("Final_solved is not assigned in the Inspector!");
        }
    }

}

