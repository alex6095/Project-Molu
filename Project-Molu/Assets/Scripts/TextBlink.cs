using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class TextBlink : MonoBehaviour, IPointerDownHandler
{
    public TMP_Text text;
    public GameObject loginCanvas;

    // Start is called before the first frame update
    void Start()
    {
        loginCanvas.SetActive(false);

        StartCoroutine(FadeTextToFullAlpha());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator FadeTextToFullAlpha() // 알파값 0에서 1로 전환
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        while (text.color.a < 1.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + (Time.deltaTime));
            yield return null;
        }
        StartCoroutine(FadeTextToZeroAlpha());
    }

    public IEnumerator FadeTextToZeroAlpha()  // 알파값 1에서 0으로 전환
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
        while (text.color.a > 0.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime));
            yield return null;
        }
        StartCoroutine(FadeTextToFullAlpha());
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        loginCanvas.SetActive(true);
        Destroy(text.gameObject);
    }
}
