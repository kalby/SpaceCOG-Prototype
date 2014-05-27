using UnityEngine;
using System.Collections;

public class GameWorldControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        CheckKeys();
	}

    private void CheckKeys()
    {
        //if they hit escape load the previous scene
        if (Input.GetKeyDown("escape"))
        {
            //slideSound.Play();
            //while (slideSound.IsInvoking())
            //{
                //do nothing
            //}
            MainMenuSelect();
        }
    }

    public void MainMenuSelect()
    {
        Application.LoadLevel("UIMainMenu");
    }
}
