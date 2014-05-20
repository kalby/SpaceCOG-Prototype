using UnityEngine;
using System.Collections;

public class ProjectileLazer : MonoBehaviour {

    public Transform player;

    public int points;


    private float delayShot;
	// Use this for initialization
	void Start () {	
	}
	
	// Update is called once per frame
	void Update () {

	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            //This is to tell the player getting hit. Used for taking health from player getting hit
            //other.gameObject.SendMessage("Hit");

            //When the lazer was instantiated it was given a reference to who instantiated it.
            //This is stores in the variable player
            //players is then used to send a message back to the player to indicate whether or not the bullet hit the enemy
            //The "AddToScore" is a method in the PlayerFlight script and point is a paramter of that method
            player.SendMessage("AddToScore", points);

            //Destroys the lazer gameobject
            Destroy(gameObject);            
        }
    }


    void OnTriggerExit(Collider other)
    {
        //If the lazer leaves the collider "Boundary" then the gameobject is destroyed
        if (other.tag == "Boundary")
        {
            Destroy(gameObject);
        }
    }
}
