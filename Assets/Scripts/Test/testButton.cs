using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class testButton : MonoBehaviour
{
    // Start is called before the first frame update
    Image iiimahge;
    void Start()
    {
        iiimahge = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyDown(KeyCode.Space))
       {
           iiimahge.DOFade(0, 1.5f);
       }
    }
    Color RandomColor()
	{
		return new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1);
	}
}
