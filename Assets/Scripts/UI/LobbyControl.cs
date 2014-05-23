using UnityEngine;
using System.Collections;

public class LobbyControl : MonoBehaviour
{

    private UIPlaySound slideSound;

    private GameObject startMatch;
    private GameObject startMatchGreyed;

    private int numberOfPlayers;
    private int notReadyCount;

    void Start()
    {
        //Get the audio from the background, which is the slide sound
        GameObject background = GameObject.FindWithTag("Background");
        slideSound = background.GetComponent<UIPlaySound>();

        //Get the START MATCH label.
        startMatch = GameObject.FindWithTag("StartMatch");
        //Get the greyed out label
        startMatchGreyed = GameObject.FindWithTag("StartMatchGreyed");

        //Ensure the greyed out label is displayed at start
        startMatch.SetActive(false);
        startMatchGreyed.SetActive(true);

        //Set the number of players
        numberOfPlayers = 9;
        //Ready count should be the number of players
        notReadyCount = numberOfPlayers;
    }

    // Update is called once per frame
    void Update()
    {
        //Check for input.
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
            MissionSelect();
        }
    }

    public void MissionSelect()
    {
        Application.LoadLevel("UIMission");
    }

    public void LoadingSelect()
    {
        Application.LoadLevel("UILoading");
    }

    public void changeReadyCount()
    {
        if (startMatch == null)
        {
            return;
        }
        UIToggle toggle = UIToggle.current;
        if (toggle.value == true)
        {
            //decrement the ready count
            notReadyCount -= 1;
        }
        else
        {
            //increment the ready count
            notReadyCount += 1;
        }
        //Check that the ready boxes are all checked.
        if (notReadyCount <= 0)
        {
            //Everyone is ready, make start match clickable.
            startMatchGreyed.SetActive(false);
            startMatch.SetActive(true);
        }
        else
        {
            //Someone isn't ready, grey out start match.
            startMatch.SetActive(false);
            startMatchGreyed.SetActive(true);
        }
    }

    public void FlipAllReadyBoxes()
    {
        UIToggle[] toggles = GameObject.FindObjectsOfType<UIToggle>();
        for (int i = 0; i <toggles.Length;i++)
        {
            toggles[i].value = !toggles[i].value;
        }
    }
}
