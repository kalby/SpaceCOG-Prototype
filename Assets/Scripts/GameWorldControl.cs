using UnityEngine;
using System.Collections;

public class GameWorldControl : MonoBehaviour
{
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

    // Use this for initialization
    void Start()
    {
        //Set the frame rate
        Application.targetFrameRate = 60;

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

        //Set UI Elements
        //Set the initial Score
        scoreLabel.text = "Score: " + score;

        //Set the starting kd ratio
        kdRatioLabel.text = "K/D:" + kills + "/" + deaths;

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
    public void addKills(int number)
    {
        kills += number;
        kdRatioLabel.text = "K/D:" + kills + "/" + deaths;
    }

    public int getKills()
    {
        return kills;
    }

    public void addDeaths(int number)
    {
        deaths += number;
        kdRatioLabel.text = "K/D:" + kills + "/" + deaths;
    }

    public int getDeaths()
    {
        return deaths;
    }

    public void addScore(int points)
    {
        score += points;
        scoreLabel.text = "Score: " + score;
    }

    public int getScore()
    {
        return score;
    }

    public void addSol(int amount)
    {
        sol += amount;
        playerSolLabel.text = "Sol: " + sol;
    }

    public int getSol()
    {
        return sol;
    }
}
