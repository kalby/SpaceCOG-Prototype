﻿using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public GameObject[] playerList;
    //public Transform[] 
    public Transform spawnPoint;
    public GameObject player;

	// Use this for initialization
	void Start () {
        Instantiate(player, spawnPoint.position, spawnPoint.rotation);
	    
	}

	// Update is called once per frame
	void Update () {
	
	}
}
