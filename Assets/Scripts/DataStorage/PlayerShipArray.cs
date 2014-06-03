using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerShipArray: MonoBehaviour {
    public GameObject[] enemyPlayers;
    public ArrayList allPlayers = new ArrayList();

    //Station spawn points
    public GameObject[] stationOneSpawnPoints;
    public GameObject[] stationTwoSpawnPoints;
    public GameObject[] stationThreeSpawnPoints;

    //Teams
    private ArrayList teamOne = new ArrayList();
    private ArrayList teamTwo = new ArrayList(); 
    private ArrayList teamThree = new ArrayList();

	// Use this for initialization
	void Start () {

        foreach (GameObject spawn in stationOneSpawnPoints)
        {
            int playerNum = Random.Range(0, 1);
            GameObject p = Instantiate(enemyPlayers[playerNum], spawn.transform.position, spawn.transform.rotation) as GameObject;
            p.tag = "TeamOne";
            teamOne.Add(p);
            allPlayers.Add(p);
        }
        
        foreach (GameObject spawn in stationTwoSpawnPoints)
        {
            int playerNum = Random.Range(0, 1);
            GameObject p = Instantiate(enemyPlayers[playerNum], spawn.transform.position, spawn.transform.rotation) as GameObject;
            p.tag = "TeamTwo";
            teamTwo.Add(p);
            allPlayers.Add(p);
        }

        foreach (GameObject spawn in stationThreeSpawnPoints)
        {
            int playerNum = Random.Range(0, 1);
            GameObject p = Instantiate(enemyPlayers[playerNum], spawn.transform.position, spawn.transform.rotation) as GameObject;
            p.tag = "TeamThree";
            teamThree.Add(p);
            allPlayers.Add(p);
        }

        foreach (GameObject p in allPlayers)
        {
            Debug.Log(p.tag);
        }	    
	}

	// Update is called once per frame
	void Update () {
	
	}

    void AddPlayer(GameObject player)
    {
        allPlayers.Add(player);
    }
}
