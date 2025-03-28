using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewMonoBehaviourScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public string scene;
    public TextMeshProUGUI text;

    async void Start()
    {
        try
        {
            await SceneManager.LoadSceneAsync(scene);

        }
        catch (Exception e) {
            text.text = e.ToString();
        }
    
    
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
