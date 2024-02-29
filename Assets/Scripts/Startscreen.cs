using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Startscreen : MonoBehaviour
{
    // Start is called before the first frame update
    private Image image;
    [SerializeField] TextMeshProUGUI text;
    void Start()
    {
        image = GetComponent<Image>();
        StartCoroutine(StartScreen());
    }

    IEnumerator StartScreen()
    {
        Time.timeScale = 0;
        float alpha = 1;
        while (alpha > 0)
        {
            alpha -= 0.01f;
            image.color = new Color(0, 0, 0, alpha);
            text.color = new Color(1, 1, 1, alpha);
            yield return new WaitForSecondsRealtime(0.02f);
        }
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
}
