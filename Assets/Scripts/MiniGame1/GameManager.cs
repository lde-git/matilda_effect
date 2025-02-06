using UnityEngine;
using Fungus;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private int totalCards;
    private int placedCards = 0;

    [SerializeField] private View archiveView; // Assign this in the Inspector

    void Awake()
    {
        Instance = this;
        totalCards = FindObjectsOfType<Card>().Length; // Count all cards in the scene
    }

    public void CardPlaced()
    {
        placedCards++;
        if (placedCards >= totalCards)
        {
            Debug.Log("All cards placed! Switching to 'archive_view'.");
            FadeToArchiveView();
        }
    }

    private void FadeToArchiveView()
    {
        if (archiveView == null)
        {
            Debug.LogError("Archive View is not assigned in the Inspector!");
            return;
        }

        var cameraManager = FungusManager.Instance.CameraManager;
        if (cameraManager == null)
        {
            Debug.LogError("CameraManager not found!");
            return;
        }

        // Use Fungus CameraManager to fade to the archive view
        cameraManager.FadeToView(Camera.main, archiveView, 1.0f, true, null, 
            LeanTweenType.easeInOutQuad, LeanTweenType.easeInOutQuad, 
            LeanTweenType.easeInOutQuad, LeanTweenType.easeInOutQuad);
    }
}
