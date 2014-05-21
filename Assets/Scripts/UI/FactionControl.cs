using UnityEngine;
using System.Collections;

public class FactionControl : MonoBehaviour {

   
    private UIPlaySound slideSound;

    private UIScrollBar raceScrollBar;
    private UIScrollBar shipScrollBar;

	void Start () {
        //Get components from the scene at init.
        //Get the audio from the background, which is the slide sound.
        GameObject background = GameObject.FindWithTag("Background");
        slideSound = background.GetComponent<UIPlaySound>();
        //Get the scroll bar from the race scroll element.
        GameObject raceScrollBarObject = GameObject.FindWithTag("RaceScrollBar");
        raceScrollBar = raceScrollBarObject.GetComponent<UIScrollBar>();
        //Get the scroll bar from the ship scroll element.
        GameObject shipScrollBarObject = GameObject.FindWithTag("ShipScrollBar");
        shipScrollBar = shipScrollBarObject.GetComponent<UIScrollBar>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        CheckKeys();
	}

    private void CheckKeys()
    {
        //if they hit escape load the previous scene
        if (Input.GetKeyDown("escape"))
        {
            slideSound.Play();
            while (slideSound.IsInvoking())
            {
                //do nothing
            }
            MainMenuSelect();
        }
        //if they hit enter pretend to save changes and load previous scene
        if (Input.GetKeyDown("return"))
        {
            //This is where the settings would be saved.
            slideSound.Play();
            while (slideSound.IsInvoking())
            {
                //do nothing
            }
            MainMenuSelect();
        }
    }

    public void MainMenuSelect()
    {
        Application.LoadLevel("UIMainMenu");
    }

    public void MoveRaceScrollLeft()
    {
        raceScrollBar.value += 1f;
    }

    public void MoveRaceScrollRight()
    {
        raceScrollBar.value -= 1f;
    }

    public void MoveShipScrollLeft()
    {
        shipScrollBar.value += 1f;
    }

    public void MoveShipScrollRight()
    {
        shipScrollBar.value -= 1f;
    }
}
