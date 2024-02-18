using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public static SceneManager Instance { get; private set; }
    private void Awake() 
    { 
    // If there is an instance, and it's not me, delete myself.
    
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    public void StartNewGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Tutorial");
    }

    public void Quit()
    {
         #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
         #else
        Application.Quit();
        #endif
    }
}
