using UnityEngine;
using System.Collections;

//This script handles UI Sounds and actions based on keypresses and provides public functions for onClicks and onDoubleClicks handled by NGUI to control the UI.
//I would've liked to put all the UI sounds on one element and referenced them, however that doesn't take advantage of NGUIs built in UIPlaySound clicks for faster prototyping.
public class MainMenuControl : MonoBehaviour
{
    private UIPlaySound quitSlideSound;
    private UIPlaySound slideSound;
    private UIPlaySound selectSound;
    
    private UIScrollBar slider;


    // Use this for initialization
    void Start()
    {
        //Get components from the scene at init.
        //Get the audio from the quit button, the slide onto quit audio
        GameObject quitButton = GameObject.FindWithTag("QuitButton");
        quitSlideSound = quitButton.GetComponent<UIPlaySound>();
        
        //Get the audio from the start button, the default select audio
        GameObject startButton = GameObject.FindWithTag("StartButton");
        slideSound = startButton.GetComponent<UIPlaySound>();

        //Get the control slider scrollbar.
        slider = GetSlider();

        //Get the audio from the slider, which happens to be the select sound
        selectSound = slider.GetComponent<UIPlaySound>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckKeys();
    }

    private void CheckKeys()
    {
        
        //just quit when escape is hit.
        if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
        }

        //move the slider up one element
        if (Input.GetKeyDown("up") || Input.GetKeyDown("w"))
        {
            slideSound.Play();
            slider.value = slider.value - 0.5f;
        }

        //move the slider down one element
        if (Input.GetKeyDown("down") || Input.GetKeyDown("s"))
        {
            //If the next one down is quit play the slide onto quit sound
            if(slider.value == 0.5f)
            {
                quitSlideSound.Play();
            }
            else
            {
                slideSound.Play();
            }
            slider.value = slider.value + 0.5f;
        }

        //they hit enter while START was selected, begin mission select
        if (Input.GetKeyDown("return") && slider.value == 0.0f)
        {
            selectSound.Play();
            while (selectSound.IsInvoking())
            {
                //do nothing
            }
            //then load level
            MissionSelect();
        }
        
        //they  hit enter while OPTIONS was selected, begin race/faction select
        if (Input.GetKeyDown("return") && slider.value == 0.5f)
        {
            selectSound.Play();
            while (selectSound.IsInvoking())
            {
                //do nothing
            }
            //then load level
            RaceFactionSelect();
        }
        //they hit enter while QUIT was selected, quit the application
        if (Input.GetKeyDown("return") && slider.value == 1.0f)
        {
            selectSound.Play();
            while (selectSound.IsInvoking())
            {
                //do nothing
            }
            Application.Quit();
        }


    }

    private UIScrollBar GetSlider()
    {
        UISprite sprite = GetComponent<UISprite>();
        return sprite.gameObject.GetComponent<UIScrollBar>();
    }

    public void SetSliderToStart()
    {
        GetSlider().value = 0.0f;
    }

    public void SetSliderToOptions()
    {
        GetSlider().value = 0.50f;
    }

    public void SetSliderToQuit()
    {
        GetSlider().value = 1.0f;
    }

    public void MissionSelect()
    {
        Application.LoadLevel("UIMission");
    }

    public void RaceFactionSelect()
    {
        Application.LoadLevel("UIRaceFaction");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
