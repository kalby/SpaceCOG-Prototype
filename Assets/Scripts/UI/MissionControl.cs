using UnityEngine;
using System.Collections;

public class MissionControl : MonoBehaviour
{
    private UIPlaySound selectSound;
    private GameObject missionInfoBox;

    // Use this for initialization
    void Start()
    {
        //Get the audio from the quit button, the slide onto quit audio
        GameObject backEscape = GameObject.FindWithTag("BackEscape");
        selectSound = backEscape.GetComponent<UIPlaySound>();
        missionInfoBox = GameObject.FindWithTag("MissionInfoBox");
        hideMissionText();

    }

    // Update is called once per frame
    void Update()
    {
        CheckKeys();
    }

    private void CheckKeys()
    {
        //if they hit escape load the previous scene
        if (Input.GetKeyDown("escape"))
        {
            selectSound.Play();
            while (selectSound.IsInvoking())
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

    public void ShowMissionText()
    {
        missionInfoBox.SetActive(true);
    }

    public void hideMissionText()
    {
        missionInfoBox.SetActive(false);
    }
}
