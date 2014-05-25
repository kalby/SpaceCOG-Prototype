using UnityEngine;
using System.Collections;

public class ProjectileLazer : MonoBehaviour {

    public GameObject player;
    public GameObject explosion;
    

    public int points;

    //private int damage;
    private float range;
    private Vector3 spawnPoint;
    private Vector3 currentPos;


    private float delayShot;
	// Use this for initialization
	void Start () {
        spawnPoint = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        //currentPos = transform.position;

        float distance = Vector3.Distance(spawnPoint, transform.position);

        if (distance >= range)
        {
            Destroy(gameObject);
            Instantiate(explosion, transform.position, transform.rotation);
        }

	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary")
        {
            return;
        }
        if (other.tag == "Crystals")
        {
            return;
        }
        
        if (other.tag != player.tag)//If they player who fired the lazer's tag isnt the same as the gameobject's tag that the lazer hit, then do damage
        {
            Debug.Log("OtherTag: " + other.tag + " and Player Tag: " + player.tag);
            //This is to tell the player getting hit. Used for taking health from player getting hit
            other.gameObject.SendMessage("Hit", 5);

            //When the lazer was instantiated it was given a reference to who instantiated it.
            //This is stores in the variable player
            //players is then used to send a message back to the player to indicate whether or not the bullet hit the enemy
            //The "AddToScore" is a method in the PlayerController script and points is a paramter of that method
            //player.SendMessage("AddToScore", points);

            //Destroys the lazer gameobject
            Destroy(gameObject);
            Instantiate(explosion, transform.position, transform.rotation);
        }
        //else if (other.tag == "Aeonur")
        //{
        //    other.gameObject.SendMessage("Hit", 5);//SendMessage to Hit method in Aeonur script to take 5 health
        //}
        
    }


    void OnTriggerExit(Collider other)
    {
        //If the lazer leaves the collider "Boundary" then the gameobject is destroyed
        if (other.tag == "Boundary")
        {
            Destroy(gameObject);
            Instantiate(explosion, transform.position, transform.rotation);
        }
    }

    void SetRange(float newRange)
    {
        range = newRange;
    }
}
