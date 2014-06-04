using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerShipArray: MonoBehaviour {
    public GameObject[] enemyPlayers;
    public ArrayList allPlayers = new ArrayList();

    //Station spawn points
    //public GameObject[] stationOneSpawnPoints;
    public GameObject[] stationTwoSpawnPoints;
    public GameObject[] stationThreeSpawnPoints;

    //Teams
    //private ArrayList teamOne = new ArrayList();
    private ArrayList teamTwo = new ArrayList(); 
    private ArrayList teamThree = new ArrayList();

	// Use this for initialization
	void Start () {

        //foreach (GameObject spawn in stationOneSpawnPoints)
        //{
        //    int playerNum = Random.Range(0, 3);
        //    GameObject p = Instantiate(enemyPlayers[playerNum], spawn.transform.position, spawn.transform.rotation) as GameObject;
        //    p.tag = "TeamOne";
        //    teamOne.Add(p);
        //    allPlayers.Add(p);
        //}
        
        //Second Station
        GameObject p1 = Instantiate(enemyPlayers[0], stationTwoSpawnPoints[0].transform.position, stationTwoSpawnPoints[0].transform.rotation) as GameObject;
        p1.tag = "TeamTwo";
        teamTwo.Add(p1);
        allPlayers.Add(p1);

        GameObject p2 = Instantiate(enemyPlayers[1], stationTwoSpawnPoints[0].transform.position, stationTwoSpawnPoints[0].transform.rotation) as GameObject;
        p2.tag = "TeamTwo";
        teamTwo.Add(p2);
        allPlayers.Add(p2);

        GameObject p3 = Instantiate(enemyPlayers[2], stationTwoSpawnPoints[0].transform.position, stationTwoSpawnPoints[0].transform.rotation) as GameObject;
        p3.tag = "TeamTwo";
        teamTwo.Add(p3);
        allPlayers.Add(p3);

        //Third Station
        GameObject p4 = Instantiate(enemyPlayers[0], stationThreeSpawnPoints[0].transform.position, stationTwoSpawnPoints[0].transform.rotation) as GameObject;
        p4.tag = "TeamThree";
        teamThree.Add(p4);
        allPlayers.Add(p4);

        GameObject p5 = Instantiate(enemyPlayers[1], stationThreeSpawnPoints[0].transform.position, stationTwoSpawnPoints[0].transform.rotation) as GameObject;
        p5.tag = "TeamThree";
        teamThree.Add(p5);
        allPlayers.Add(p5);

        GameObject p6 = Instantiate(enemyPlayers[2], stationThreeSpawnPoints[0].transform.position, stationTwoSpawnPoints[0].transform.rotation) as GameObject;
        p6.tag = "TeamThree";
        teamThree.Add(p6);
        allPlayers.Add(p6);

        foreach (GameObject p in allPlayers)
        {
            //Debug.Log(p.tag);
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
