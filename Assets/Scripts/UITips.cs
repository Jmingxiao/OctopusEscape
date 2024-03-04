using UnityEngine;
using UnityEngine.Tilemaps;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections;
public class UITips : MonoBehaviour
{
    [SerializeField]private Tilemap grappingTilemap;
    [SerializeField]private SpriteRenderer pullableSprite;
    [SerializeField]private SpriteRenderer mouseui;
    [SerializeField]private Animator uianime;
    
     public static UITips Instance { get; private set; }

    // Start is called before the first frame update
    private void Awake() {
         if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    public IEnumerator ShowGrapable()
    {
        float timer = 1.5f;
        while (timer > 0)
        {
            grappingTilemap.color =  Color.Lerp(Color.white, Color.black, Mathf.PingPong(Time.time, 1));
            timer -= Time.deltaTime;
            yield return null;
        }
        grappingTilemap.color = Color.white;

    }

    public void ShowPullable()
    {
        mouseui.gameObject.SetActive(true);
        uianime.gameObject.SetActive(true);
        pullableSprite.DOColor(Color.red, 0.3f).SetLoops(6, LoopType.Yoyo);
        mouseui.DOColor(Color.red, 0.3f).SetLoops(6, LoopType.Yoyo).onComplete += OncompleteMouse;
        uianime.SetTrigger("Shift");
    }
    public void ShowMouseUI()
    {
        mouseui.gameObject.SetActive(true);
        mouseui.DOColor(Color.red, 0.25f).SetLoops(6, LoopType.Yoyo).onComplete += OncompleteMouse;
    }
    public void OncompleteMouse()
    {   
        mouseui.color = Color.white;
        mouseui.gameObject.SetActive(false);
    }
    public void OncompletePullable()
    {
        pullableSprite.color = Color.white;
    }
}
