using UnityEngine;
using System.Collections;

public class MissionControl : MonoBehaviour
{
    private UIPlaySound slideSound;
    private GameObject aesopInfoBox;
    private GameObject fourcroyInfoBox;
    private GameObject herronsInfoBox;
    private GameObject santaroInfoBox;

    // Use this for initialization
    void Start()
    {
        //Get the audio from the background, which is the slide sound
        GameObject background = GameObject.FindWithTag("Background");
        slideSound = background.GetComponent<UIPlaySound>();

        //Get the Mission Info Boxes
        aesopInfoBox = GameObject.FindWithTag("AesopInfoBox");
        fourcroyInfoBox = GameObject.FindWithTag("FourcroyInfoBox");
        herronsInfoBox = GameObject.FindWithTag("HerronsInfoBox");
        santaroInfoBox = GameObject.FindWithTag("SantaroInfoBox");

        //Hide them
        HideAesopMissionText();
        HideFourcroyMissionText();
        HideHerronsMissionText();
        HideSantaroMissionText();

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
            slideSound.Play();
            while (slideSound.IsInvoking())
            {
                //do nothing
            }
            MainMenuSelect();
        }
        if (Input.GetKeyDown("a"))
        {
            ShowAesopMissionText();
            ShowFourcroyMissionText();
            ShowHerronsMissionText();
            ShowSantaroMissionText();
        }
        if (Input.GetKeyUp("a"))
        {
            HideAesopMissionText();
            HideFourcroyMissionText();
            HideHerronsMissionText();
            HideSantaroMissionText();
        }
    }

    public void MainMenuSelect()
    {
        Application.LoadLevel("UIMainMenu");
    }

    public void ShowAesopMissionText()
    {
        aesopInfoBox.SetActive(true);
    }

    public void HideAesopMissionText()
    {
        aesopInfoBox.SetActive(false);
    }

    public void ShowFourcroyMissionText()
    {
        fourcroyInfoBox.SetActive(true);
    }

    public void HideFourcroyMissionText()
    {
        fourcroyInfoBox.SetActive(false);
    }

    public void ShowHerronsMissionText()
    {
        herronsInfoBox.SetActive(true);
    }

    public void HideHerronsMissionText()
    {
        herronsInfoBox.SetActive(false);
    }

    public void ShowSantaroMissionText()
    {
        santaroInfoBox.SetActive(true);
    }

    public void HideSantaroMissionText()
    {
        santaroInfoBox.SetActive(false);
    }
}
