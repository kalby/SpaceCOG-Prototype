using UnityEngine;
using System.Collections;

//This script handles UI Sounds and actions based on keypresses and provides public functions for onClicks and onDoubleClicks handled by NGUI to control the UI.
//I would've liked to put all the UI sounds on one element and referenced them, however that doesn't take advantage of NGUIs built in UIPlaySound clicks for faster prototyping.
public class MainMenuControl : MonoBehaviour
{
    private UIPlaySound quitSlideSound;
    private UIPlaySound slideSound;
    private UIPlaySound selectSound;
    
    private UIScrollBar verticalScrollBar;

    void Start()
    {
        //Get components from the scene at init.
        //Get the audio from the quit button, the slide onto quit audio
        GameObject quitButton = GameObject.FindWithTag("QuitButton");
        quitSlideSound = quitButton.GetComponent<UIPlaySound>();
        
        //Get the audio from the start button, the default select audio
        GameObject startButton = GameObject.FindWithTag("StartButton");
        slideSound = startButton.GetComponent<UIPlaySound>();

        //Get the vertical scroll bar.
        GameObject verticalScrollBarObject = GameObject.FindWithTag("MainMenuScrollBar");
        verticalScrollBar = verticalScrollBarObject.gameObject.GetComponent<UIScrollBar>();

        //Get the audio from the background, which happens to be the select sound
        GameObject background = GameObject.FindWithTag("Background");
        selectSound = background.GetComponent<UIPlaySound>();
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
            verticalScrollBar.value = verticalScrollBar.value - 0.5f;
        }

        //move the slider down one element
        if (Input.GetKeyDown("down") || Input.GetKeyDown("s"))
        {
            //If the next one down is quit play the slide onto quit sound
            if(verticalScrollBar.value == 0.5f)
            {
                quitSlideSound.Play();
            }
            else
            {
                slideSound.Play();
            }
            verticalScrollBar.value = verticalScrollBar.value + 0.5f;
        }

        //they hit enter while START was selected, begin mission select
        if (Input.GetKeyDown("return") && verticalScrollBar.value == 0.0f)
        {
            selectSound.Play();
            while (selectSound.IsInvoking())
            {
                //do nothing
            }
            //then load level
            MissionSelect();
        }
        
        //they  hit enter while FACTION was selected, begin race/faction select
        if (Input.GetKeyDown("return") && verticalScrollBar.value == 0.5f)
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
        if (Input.GetKeyDown("return") && verticalScrollBar.value == 1.0f)
        {
            selectSound.Play();
            while (selectSound.IsInvoking())
            {
                //do nothing
            }
            Application.Quit();
        }


    }

    public void SetSliderToStart()
    {
        verticalScrollBar.value = 0.0f;
    }

    public void SetSliderToFaction()
    {
        verticalScrollBar.value = 0.50f;
    }

    public void SetSliderToQuit()
    {
        verticalScrollBar.value = 1.0f;
    }

    public void MissionSelect()
    {
        Application.LoadLevel("UIMission");
    }

    public void RaceFactionSelect()
    {
        Application.LoadLevel("UIFaction");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
