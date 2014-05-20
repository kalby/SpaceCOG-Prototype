using UnityEngine;
using System.Collections;

public class MissionControl : MonoBehaviour
{
    private UIPlaySound slideSound;
    private GameObject aesopInfoBox;
    private GameObject fourcroyInfoBox;
    private GameObject herronsInfoBox;
    private GameObject santaroInfoBox;

    private GameObject aesop;
    private GameObject fourcroy;
    private GameObject herrons;
    private GameObject santaro;

    private Vector3 aesopScale;
    private Vector3 fourcroyScale;
    private Vector3 herronsScale;
    private Vector3 santaroScale;

    private GameObject highlightedMission;

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

        //Get the Mission Selection Spheres.
        aesop = GameObject.FindWithTag("Aesop");
        fourcroy = GameObject.FindWithTag("Fourcroy");
        herrons = GameObject.FindWithTag("Herrons");
        santaro = GameObject.FindWithTag("Santaro");

        //Get the starting transform scales of the Mission Spheres
        aesopScale = aesop.transform.localScale;
        fourcroyScale = fourcroy.transform.localScale;
        herronsScale = herrons.transform.localScale;
        santaroScale = santaro.transform.localScale;


        //Hide the mission text boxes
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
        if (Input.GetKeyDown("return") && highlightedMission != null)
        {
            slideSound.Play();
            while (slideSound.IsInvoking())
            {
                //do nothing
            }
            LobbySelect();
        }
    }

    public void MainMenuSelect()
    {
        Application.LoadLevel("UIMainMenu");
    }

    public void LobbySelect()
    {
        Application.LoadLevel("UILobby");
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

    public void HighlightAesop()
    {
        //Set the selected for use on keypress Enter.
        highlightedMission = aesop;

        //Make the highlighted stand out a bit.
        aesop.transform.localScale = aesopScale * 1.2f;
        fourcroy.transform.localScale = fourcroyScale * 1.0f;
        herrons.transform.localScale = herronsScale * 1.0f;
        santaro.transform.localScale = santaroScale * 1.0f;
    }

    public void HighlightFourcroy()
    {
        //Set the selected for use on keypress Enter.
        highlightedMission = fourcroy;

        //Make the highlighted stand out a bit.
        aesop.transform.localScale = aesopScale * 1.0f;
        fourcroy.transform.localScale = fourcroyScale * 1.2f;
        herrons.transform.localScale = herronsScale * 1.0f;
        santaro.transform.localScale = santaroScale * 1.0f;
    }

    public void HighlightHerrons()
    {
        //Set the selected for use on keypress Enter.
        highlightedMission = herrons;

        //Make the highlighted stand out a bit.
        aesop.transform.localScale = aesopScale * 1.0f;
        fourcroy.transform.localScale = fourcroyScale * 1.0f;
        herrons.transform.localScale = herronsScale * 1.2f;
        santaro.transform.localScale = santaroScale * 1.0f;
    }

    public void HighlightSantaro()
    {
        //Set the selected for use on keypress Enter.
        highlightedMission = santaro;

        //Make the highlighted stand out a bit.
        aesop.transform.localScale = aesopScale * 1.0f;
        fourcroy.transform.localScale = fourcroyScale * 1.0f;
        herrons.transform.localScale = herronsScale * 1.0f;
        santaro.transform.localScale = santaroScale * 1.2f;
    }

}
