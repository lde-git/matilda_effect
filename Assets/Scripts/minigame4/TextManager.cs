using UnityEngine;
using Fungus;
using System.Collections;

public class TextManager : MonoBehaviour
{
    public static TextManager Instance;

    private int totalTexts;
    [SerializeField] private Flowchart flowchartChap3; // Assign Flowchart in Inspector
    public TextArea[] textAreas;

    void Awake()
    {
        Instance = this;
        totalTexts = FindObjectsOfType<Texty>().Length;
    }

    public void OnTextPlaced()
    {
        if (textAreas == null || textAreas.Length == 0) return;

        bool allCorrect = true;

        foreach (var area in textAreas)
        {
            if (!area.hasCorrectText)
            {
                allCorrect = false;
                break;
            }
        }

        if (allCorrect)
        {
            Debug.Log("All correct texts placed! Executing 'Zeitstrahl_solved'.");
            CallGameSuccessBlock();
        }
        else
        {
            Debug.Log("Not all texts are placed correctly yet.");
        }
    }

    private void CallGameSuccessBlock()
    {
        if (flowchartChap3 != null)
        {
            flowchartChap3.ExecuteBlock("Zeitstrahl_solved");
        }
        else
        {
            Debug.LogError("Zeitstrahl_solved is not assigned in the Inspector!");
        }
    }

}

