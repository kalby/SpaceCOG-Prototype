using UnityEngine;
using System.Collections;

public class SetSliderToButton : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetSliderToStart()
    {
        UISprite sprite = GetComponent<UISprite>();
        UIScrollBar scroll = sprite.gameObject.GetComponent<UIScrollBar>();
        scroll.value = 0.0f;
    }

    public void SetSliderToOptions()
    {
        UISprite sprite = GetComponent<UISprite>();
        UIScrollBar scroll = sprite.gameObject.GetComponent<UIScrollBar>();
        scroll.value = 0.50f;
    }

    public void SetSliderToQuit()
    {
        UISprite sprite = GetComponent<UISprite>();
        UIScrollBar scroll = sprite.gameObject.GetComponent<UIScrollBar>();
        scroll.value = 1.0f;
    }
}
