using UnityEngine;
using System.Collections;

public class RaceShipControl : MonoBehaviour {

    private UIPlaySound slideSound;

	// Use this for initialization
	void Start () {
        //Get the audio from the background, which is the slide sound
        GameObject background = GameObject.FindWithTag("Background");
        slideSound = background.GetComponent<UIPlaySound>();
	
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
    }

    public void MainMenuSelect()
    {
        Application.LoadLevel("UIMainMenu");
    }
}
