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
    public int startingSol;

    //Private Variables
    //Player Kills
    private int kills;
    //Player Deaths
    private int deaths;
    //Increases as game progresses used to acquire rank up. (rank is future implementation, persistent reward)
    private int score;
    //For respawning ships and incrementing while mining sol
    private int sol;
    //The player ship
    private GameObject playerShip;

    //Score Label
    private UILabel scoreLabel;
    //KD Ratio Label
    private UILabel kdRatioLabel;
    //Sol Label
    private UILabel playerSolLabel;
    //Game Object with Ship Selection Panel on it
    private GameObject shipSelectionPanel;
    //GameObject with Victory Panel on it
    private GameObject victoryPanel;
    //GameObject with Defeat Panel on it
    private GameObject defeatPanel;

    // Use this for initialization
    void Start()
    {
        //Set the frame rate
        Application.targetFrameRate = 60;

        //Player Initialisation
        //Set the correct team from network code
        //For now hardcoded to TeamOne
        //teamOne = true;
        //teamTwo = false;
        //teamThree = false;

        //Set the starting amount of Sol
        sol = startingSol;
        

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

        //Victory Panel
        victoryPanel = GameObject.FindWithTag("VictoryPanel");

        //Defeat Panel
        defeatPanel = GameObject.FindWithTag("DefeatPanel");
        

        //Ship Selection Panel
        shipSelectionPanel = GameObject.FindWithTag("ShipSelectionPanel");

        //Set UI Elements
        //Hide defeat panel
        NGUITools.SetActive(defeatPanel, false);
        //Hide victory panel
        NGUITools.SetActive(victoryPanel, false);
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
        
        //check that they can afford the cheapest ship, developer has to maintain this if adding new ships
        if (sol < 80)
        {
            //Player Defeat
            NGUITools.SetActive(defeatPanel, true);
        }
        else
        {
            //Open the Ship Selection Panel again
            NGUITools.SetActive(shipSelectionPanel, true);
        }

        //Check for victory
        //This is odd in that it will be run by every player playing anytime they die to a player or otherwise
        //It needs to check that I'm alive and no one else is, which is netcode dependent
        if (playerShip != null && CheckEnemyStationsDead() && CheckOtherPlayersDead())
        {
            //Set Victory Panel active
            NGUITools.SetActive(victoryPanel, true);
        }
        
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
        //only positive amounts can be added
        if (amount > 0)
        {
            sol += amount;
            playerSolLabel.text = "Sol: " + sol;
        }
    }

    public void SubtractSol(int amount)
    {
        //only positive amounts can be subtracted and can't make sol go negative
        if (amount > 0 && sol - amount >= 0)
        {
            sol -= amount;
            playerSolLabel.text = "Sol: " + sol;
        }
    }

    public int GetSol()
    {
        return sol;
    }

    public void SetPlayerShip(GameObject ship)
    {
        playerShip = ship;
    }

    public bool CheckOtherPlayersDead()
    {
        //Use netcode to determine if other players remain TODO
        return false;
    }

    public bool CheckEnemyStationsDead()
    {
        //Use netcode to determine if other player's stations remain TODO
        return false;
    }
}
