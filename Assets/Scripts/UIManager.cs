using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
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
    private void OnEnable() {
        Time.timeScale = 0;
    }

    private void OnDisable() {
        Time.timeScale = 1;
    }
    public void ResumeGame()
    {
       gameObject.SetActive(false);
    }

    public void StartNewGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Tutorial");
    }

    public void LoadMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("MainMenu");
    }
    public void Quit()
    {
         #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
         #else
        Application.Quit();
        #endif
    }
    public void ReloadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
