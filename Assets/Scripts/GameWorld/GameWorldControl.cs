using UnityEngine;
using System.Collections;

//This script is designed to be placed on a background object that'll exist so long as the game world does. Currently the best place for that is on the 'sun' which is a directional light bathing all objects in its glow.
//It keeps track of any of the players statistics and is separate from their controlled objects so that it persists through player death.
public class GameWorldControl : MonoBehaviour
{
    //Player team variables for spawning in the right spots
    public bool teamOne;
    public bool teamTwo;
    public bool teamThree;

    //Private Variables
    //Player Kills
    private int kills;
    //Player Deaths
    private int deaths;
    //Increases as game progresses used to acquire rank up. (rank is future implementation, persistent reward)
    private int score;
    //For respawning ships and incrementing while mining sol
    private int sol;

    //Score Label
    private UILabel scoreLabel;
    //KD Ratio Label
    private UILabel kdRatioLabel;
    //Sol Label
    private UILabel playerSolLabel;
    //Game Object with Ship Selection Panel on it
    private GameObject shipSelectionPanel;

    // Use this for initialization
    void Start()
    {
        //Set the frame rate
        Application.targetFrameRate = 60;

        //Set the correct team from network code
        //For now hardcoded to TeamOne
        //teamOne = true;
        //teamTwo = false;
        //teamThree = false;

        //Get UI Elements
        //Score Label
        GameObject scoreObject = GameObject.FindWithTag("ScoreLabel");
        scoreLabel = scoreObject.GetComponent<UILabel>();

        //KD Ratio Label
        GameObject kdRatioObject = GameObject.FindWithTag("KDRatioLabel");
        kdRatioLabel = kdRatioObject.GetComponent<UILabel>();

        //Player Sol Label
        GameObject playerSolObject = GameObject.FindWithTag("PlayerSolLabel");
        playerSolLabel = playerSolObject.GetComponent<UILabel>();

        //Ship Selection Panel
        shipSelectionPanel = GameObject.FindWithTag("ShipSelectionPanel");

        //Set UI Elements
        //Set the initial Score
        scoreLabel.text = "Score: " + score;

        //Set the starting kd ratio
        kdRatioLabel.text = "Kills/Deaths:" + kills + "/" + deaths;

        //Set the player's Sol
        playerSolLabel.text = "Sol: " + sol;
    }

    // Update is called once per frame
    void Update()
    {
        //Check Input
        CheckKeys();
    }

    private void CheckKeys()
    {
        //if they hit escape load the previous scene
        if (Input.GetKeyDown("escape"))
        {
            //Quit to main menu immediately
            MainMenuSelect();
        }
    }

    public void MainMenuSelect()
    {
        Application.LoadLevel("UIMainMenu");
    }

    //GETTERS & SETTERS
    public void AddKills(int number)
    {
        kills += number;
        kdRatioLabel.text = "Kills/Deaths:" + kills + "/" + deaths;
    }

    public int GetKills()
    {
        return kills;
    }

    public void AddDeaths(int number)
    {
        deaths += number;
        kdRatioLabel.text = "Kills/Deaths:" + kills + "/" + deaths;
        //Open the Ship Selection Panel again
        NGUITools.SetActive(shipSelectionPanel, true);
    }

    public int GetDeaths()
    {
        return deaths;
    }

    public void AddScore(int points)
    {
        score += points;
        scoreLabel.text = "Score: " + score;
    }

    public int GetScore()
    {
        return score;
    }

    public void AddSol(int amount)
    {
        sol += amount;
        playerSolLabel.text = "Sol: " + sol;
    }

    public void SubtractSol(int amount)
    {
        sol -= amount;
        playerSolLabel.text = "Sol: " + sol;
    }

    public int GetSol()
    {
        return sol;
    }
}
