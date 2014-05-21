using UnityEngine;
using System.Collections;

public class LoadingControl : MonoBehaviour {

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
        if (Input.GetKeyDown("return"))
        {
            //slideSound.Play();
            //while (slideSound.IsInvoking())
            //{
                //do nothing
            //}
            GameWorldSelect();
        }
    }

    public void GameWorldSelect()
    {
        Application.LoadLevel("GameWorld");
    }

}
