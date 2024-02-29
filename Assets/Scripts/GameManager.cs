using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
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
    static bool restart =false;
    public GameObject menu;
    public GameObject Startscreen;
    // Start is called before the first frame update
    void Start()
    {
        
        if(!restart)
            StartCoroutine(showTips());
        else
            Startscreen.SetActive(false);

        if(menu!=null){
            restart = true;
            menu.SetActive(false); 
        } 
        
    }
    public IEnumerator showTips()
    {
        if(tips!=null){
        tips?.SetActive(true);
        yield return new WaitForSeconds(3);
        tips?.SetActive(false);
        }
    }
    
    [SerializeField]private GameObject tips;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) )
            menu.SetActive(!menu.activeSelf);
        if(Input.GetKeyDown(KeyCode.R))
           GameManager.Instance.ReloadScene();
    }
    public void ReloadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
    public void LoadScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
        restart = false;
    }
    public void LoadNextScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex+1);
        restart = false;
    }
    
}
