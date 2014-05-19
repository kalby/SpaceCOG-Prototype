using UnityEngine;
using System.Collections;

public class AeonurSpawn : MonoBehaviour {

    public GameObject[] spawnPoints;//Spawn points for aenur

    public GameObject aenur;//Aenur himself

    private int playerCount;//Number of players in the mining area

    private bool aenurSpawned;



	// Use this for initialization
	void Start () {
        playerCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerCount++;
            if(!aenurSpawned)
            {
                calculateSpawnChance(playerCount);
            }

            Debug.Log("Player Count: " + playerCount);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerCount--;
            Debug.Log("Player Count: " + playerCount);
        }
    }

    void calculateSpawnChance(int numPlayers)
    {
        int randnomNum = Random.Range(1, 101);//Min num is inclusive and max num is inclusive
        switch (numPlayers)
        {
            case 3:
                if (randnomNum <= 10)
                {
                    //Instantiate Aenur at a random spawn point. 
                    Instantiate(aenur, spawnPoints[Random.Range(0, 4)].transform.position, Quaternion.identity);
                    aenurSpawned = true;
                }
                break;
            case 4:
                if (randnomNum <= 20)
                {
                    Instantiate(aenur, spawnPoints[Random.Range(0, 4)].transform.position, Quaternion.identity);
                    aenurSpawned = true;
                }
                break;
            case 5:
                if (randnomNum <= 35)
                {
                    Instantiate(aenur, spawnPoints[Random.Range(0, 4)].transform.position, Quaternion.identity);
                    aenurSpawned = true;
                }
                break;
            case 6:
                if (randnomNum <= 80)
                {
                    Instantiate(aenur, spawnPoints[Random.Range(0, 4)].transform.position, Quaternion.identity);
                    aenurSpawned = true;
                }
                break;
            case 7:
                Instantiate(aenur, spawnPoints[Random.Range(0, 4)].transform.position, Quaternion.identity);
                aenurSpawned = true;
                break;
            default:
                break;
        }
    }
}
