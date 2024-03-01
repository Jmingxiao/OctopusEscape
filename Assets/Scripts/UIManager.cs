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
        GameManager.Instance.LoadScene("Tutorial");
    }
    public void level1()
    {
        GameManager.Instance.LoadScene("level1");
    }
    public void level2()
    {
        GameManager.Instance.LoadScene("level2");
    }
    public void Tutorial()
    {
        GameManager.Instance.LoadScene("level2");
    }
    public void LoadLevelMenu()
    {
        GameManager.Instance.LoadScene("ChoseLevel");
    }

    public void LoadMainMenu()
    {
        GameManager.Instance.LoadScene("MainMenu");
    }
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
    }
}
