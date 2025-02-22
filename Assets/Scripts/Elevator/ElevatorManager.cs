using UnityEngine;
using Fungus;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

public class ElevatorManager : MonoBehaviour
{
    public static ElevatorManager Instance;
    public int[] correctCombination = { 1, 9, 5, 6 };
    private List<int> enteredNumbers = new List<int>();

    public SpriteRenderer[] starImages;

    private float animationLoop;

    public GameObject underscore;
    public Vector3 underscoreOffset;
    public float blinkIntervall = 1;

    public int currentFloor = 2;
    public Sprite[] indicatorSprites;
    public SpriteRenderer indicatorNumber;
    private int currentSpriteIndex = 0; // Keep track of the current sprite
    private bool ascending = true;
    private bool elevatorUnlocked = false;


    public float travelTime = 2f;
    public float shakeAmount = 0.1f;
    public float shakeDuration = 0.5f;
    public float directionalShakeAmount = 0.05f;
    public float horizontalShakeAmount = 0.02f;
    private Vector3 originalCameraPosition;
    private bool isMoving = false;


    public GameObject[] floorButtonPanels;
    public GameObject elevatorDoor;
    private PolygonCollider2D elevatorDoorCollider;


    private PolygonCollider2D button2Collider;
    private PolygonCollider2D button1Collider;
    

    public Flowchart flowchart;

    void Awake()
    {
        var panel = floorButtonPanels[0];
        var button2Transform = panel.transform.GetChild(1);
        var button1Transform = panel.transform.GetChild(3);

        button2Collider = button2Transform.GetComponent<PolygonCollider2D>(); 
        button1Collider = button1Transform.GetComponent<PolygonCollider2D>();
        elevatorDoorCollider = elevatorDoor.GetComponent<PolygonCollider2D>();


        StartCoroutine(AnimateIndicator());
        foreach (var star in starImages)
        {
            star.gameObject.SetActive(false);
        }
        Instance = this;

        originalCameraPosition = Camera.main.transform.position;
    }

    public void AddNumber(int number)
    {
        if (enteredNumbers.Count < 4)
        {
            enteredNumbers.Add(number);
            Debug.Log("Added number: " + number);

            if (enteredNumbers.Count <= starImages.Length)
            {
                starImages[enteredNumbers.Count - 1].gameObject.SetActive(true);
            }
            else
            {
                Debug.LogError("Not enough star images assigned!");
            }

            if (enteredNumbers.Count == 4)
            {
                CheckCombination();
            }
        }
    }

    private IEnumerator AnimateIndicator()
    {
        while (true)
        {
            int minIndex = (currentFloor == 2) ? 0 : 3; // Set minIndex based on currentFloor
            int maxIndex = (currentFloor == 2) ? 3 : 6; // Set maxIndex based on currentFloor 

            for (int i=0; i<floorButtonPanels.Length; i++) {

                var panel = floorButtonPanels[i];
                var button2Transform = panel.transform.GetChild(1);
                var button1Transform = panel.transform.GetChild(3);

                Transform bigFloorIndicatorTransform = null;
                if (i==0) bigFloorIndicatorTransform = panel.transform.GetChild(4);
                if (currentFloor == 2)
                {
                    if (button2Transform.TryGetComponent<SpriteRenderer>(out SpriteRenderer button1SpriteRenderer)) button1SpriteRenderer.sprite = indicatorSprites[6];
                    if (button1Transform.TryGetComponent<SpriteRenderer>(out SpriteRenderer button2SpriteRenderer)) button2SpriteRenderer.sprite = indicatorSprites[7];
                    if (bigFloorIndicatorTransform != null && bigFloorIndicatorTransform.TryGetComponent<SpriteRenderer>(out SpriteRenderer bigFloorIndicatorRenderer)) bigFloorIndicatorRenderer.sprite = indicatorSprites[0];
                }
                else {
                    if (button2Transform.TryGetComponent<SpriteRenderer>(out SpriteRenderer button1SpriteRenderer)) button1SpriteRenderer.sprite = indicatorSprites[7];
                    if (button1Transform.TryGetComponent<SpriteRenderer>(out SpriteRenderer button2SpriteRenderer)) button2SpriteRenderer.sprite = indicatorSprites[6];
                    if (bigFloorIndicatorTransform != null && bigFloorIndicatorTransform.TryGetComponent<SpriteRenderer>(out SpriteRenderer bigFloorIndicatorRenderer)) bigFloorIndicatorRenderer.sprite = indicatorSprites[3];
                }
            }
            

            // Determine the next sprite index within the allowed range
            if (ascending)
            {
                currentSpriteIndex++;
                if (currentSpriteIndex >= maxIndex)
                {
                    currentSpriteIndex = maxIndex - 2; // Go back one step before reversing
                    ascending = false;
                }
            }
            else
            {
                currentSpriteIndex--;
                if (currentSpriteIndex < minIndex)
                {
                    currentSpriteIndex = minIndex + 1; // Go one step forward before reversing
                    ascending = true;
                }
            }

            StartCoroutine(ChangeIndicatorSprite(indicatorSprites[currentSpriteIndex]));
            yield return new WaitForSeconds(Random.Range(0.1f, 0.3f)); // Vary the delay
        }
    }

    private IEnumerator ChangeIndicatorSprite(Sprite newSprite)
    {
        float duration = Random.Range(0.1f, 0.2f); // Vary the duration
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            t = EaseInOutCubic(t); // Apply easing function

            // Example: Fade the alpha
            Color color = indicatorNumber.color;
            color.a = Mathf.Lerp(0.5f, 1f, t); // Change 0f to 0.5f for less flickering
            indicatorNumber.color = color;

            yield return null;
        }

        indicatorNumber.sprite = newSprite;
    }

    // Example ease-in-out cubic function
    private float EaseInOutCubic(float t)
    {
        return t < 0.5f ? 4 * t * t * t : (t - 1) * (2 * t - 2) * (2 * t - 2) + 1;
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isMoving)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (button2Collider != null && button2Collider.OverlapPoint(mousePosition))
            {
                
                if (elevatorUnlocked) { 
                    if (currentFloor == 1) {
                        StartCoroutine(MoveElevator(2));
                        currentFloor = 2;
                    }
                    Debug.Log("Elevator already on Floor 2");
                } else {
                    flowchart.SendFungusMessage("ElevatorLockedMessage");
                    return;
                }
            }

            if (button1Collider != null && button1Collider.OverlapPoint(mousePosition))
            {
                if (elevatorUnlocked) { 
                    if (currentFloor == 2) {
                        StartCoroutine(MoveElevator(1));
                    }
                Debug.Log("Elevator already on Floor 1");
                } else {
                    flowchart.SendFungusMessage("ElevatorLockedMessage");
                    return;
                }
            }

            if (elevatorDoorCollider != null && elevatorDoorCollider.OverlapPoint(mousePosition))
            {
                if (currentFloor == 1)
                {
                    flowchart.ExecuteBlock("Flur Keller");
                }
                else flowchart.ExecuteBlock("Flur EG");

            }

        }



        int amount = enteredNumbers.Count;

        for (int i = 0; i < 4; i++)
        {
            if (amount <= i)
            {
                starImages[i].gameObject.SetActive(false);
            }
            else
            {
                starImages[i].gameObject.SetActive(true);
            }
        }

        if (amount == 4)
        {
            underscore.SetActive(false);
            return;
        }
        animationLoop += Time.deltaTime;
        underscore.transform.position = starImages[amount].transform.position + underscoreOffset;

        if (animationLoop > blinkIntervall)
        {
            animationLoop = 0;
            underscore.gameObject.SetActive(true);
        }
        else if (animationLoop > blinkIntervall / 2)
        {
            underscore.gameObject.SetActive(false);
        }
    }

    private IEnumerator MoveElevator(int targetFloor)
    {
        isMoving = true;
        //audioSource.Play();

        button1Collider.enabled = false;
        button2Collider.enabled = false;

        originalCameraPosition = Camera.main.transform.position;
        yield return StartCoroutine(ShakeCamera(targetFloor)); // Shake the camera

        currentFloor = targetFloor;

        yield return new WaitForSeconds(travelTime);

        button1Collider.enabled = true;
        button2Collider.enabled = true;

        isMoving = false;
    }

    private IEnumerator ShakeCamera(int targetFloor)
    {
        float elapsed = 0.0f;
        float direction = (targetFloor == 1) ? 1f : -1f;
        float horizontalDirection = Random.Range(-1f, 1f); // Random horizontal direction

        while (elapsed < shakeDuration)
        {
            elapsed += Time.deltaTime;

            float y = direction * directionalShakeAmount;
            float x = Mathf.Sin(elapsed * 20f) * horizontalShakeAmount; // Horizontal shake

            Camera.main.transform.position = originalCameraPosition + new Vector3(x, y, 0);

            yield return null;
        }

        Camera.main.transform.position = originalCameraPosition;
    }


    private void CheckCombination()
    {
        bool correct = enteredNumbers.SequenceEqual(correctCombination);

        if (correct)
        {
            Debug.Log("Correct combination!");
            elevatorUnlocked = true;
            if (flowchart != null)
            {
                flowchart.ExecuteBlock("Aufzug");
            }
            else
            {
                Debug.LogError("Flowchart not assigned in ElevatorManager!");
            }
        }
        else
        {
            Debug.Log("Incorrect combination!");
            enteredNumbers.Clear();

            // Hide all stars
            foreach (var star in starImages)
            {
                star.gameObject.SetActive(false);
            }

            if (flowchart != null)
            {
                //flowchart.ExecuteBlock("WrongCombination");
            }
            else
            {
                Debug.LogError("Flowchart not assigned in ElevatorManager!");
            }
        }
    }

    public void ClearNumbers()
    {
        enteredNumbers.Clear();
    }
}