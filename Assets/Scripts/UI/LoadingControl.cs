using UnityEngine;
using System.Collections;

public class LoadingControl : MonoBehaviour
{

    //Variables for fluff text for use in EmulateTerminal()
    private GameObject fluff1;
    private GameObject fluff2;
    private GameObject fluff3;
    private GameObject fluff4;

    //GameObject array for use in EmulateTerminal()
    private GameObject[] fluffText;

    // Use this for initialization
    void Start()
    {
        //Get the texts
        fluff1 = GameObject.FindWithTag("Fluff1");
        fluff2 = GameObject.FindWithTag("Fluff2");
        fluff3 = GameObject.FindWithTag("Fluff3");
        fluff4 = GameObject.FindWithTag("Fluff4");

        //Initialise the array with space for 4 GameObjects, put the fluff texts in.
        fluffText = new GameObject[5] { fluff1, fluff2, fluff3, fluff4, null };

        //Set them all disabled if not already
        fluff1.SetActive(false);
        fluff2.SetActive(false);
        fluff3.SetActive(false);
        fluff4.SetActive(false);

        //Run the basic terminal emulation. I did this to quickly practice a little bit of co-routines, not a waste of time, promise.
        //Also automatically loads the GameWorld without input which is more accurate and handy.
        StartCoroutine(EmulateTerminal());
    }

    // Update is called once per frame
    void Update()
    {
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

    //A co-routine to display text every 1 seconds
    private IEnumerator EmulateTerminal()
    {
        for (int i = 0; i < fluffText.Length; i++)
        {
            //check that the last text is true, then we must be on the null element, select GameWorld scene.
            if (fluff4.activeSelf == true)
            {
                GameWorldSelect();
            }
            //ensure the null element doesn't cause an error
            if (fluffText[i] == null)
            { 
                break;
            }
            fluffText[i].SetActive(true);
            yield return new WaitForSeconds(1);
        }
    }

}
