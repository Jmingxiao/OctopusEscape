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
    [SerializeField]private GameObject tips;
    float tipstime = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        
        if(!restart){
            if(tips!=null)
                tips.SetActive(true);
        }
        else
            Startscreen.SetActive(false);

        if(menu!=null){
            restart = true;
            menu.SetActive(false); 
        } 
        
        
    }
    // Update is called once per frame
    void Update()
    {
        if(tips!=null&&tipstime<=0){
            tips.SetActive(false);
        }else{
            tipstime -= Time.deltaTime;
        }

        if(Input.GetKeyDown(KeyCode.Escape) )
        {
            menu.SetActive(!menu.activeSelf);
            if(tips!=null&&tipstime<=0){
                tips.SetActive(!tips.activeSelf);
            }
        }
        if(Input.GetKeyDown(KeyCode.R))
           ReloadScene();
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
