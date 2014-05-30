using UnityEngine;
using System.Collections;

public class ProjectileLazer : MonoBehaviour
{

    //Player who fired the shot
    public GameObject player;
    //Type of explosion on destruction
    public GameObject explosion;
    //Damage of lazer shot
    public int damage;
    //Score for hitting enemy players with lazer shot
    public int points;
    //Lazer speed modifier, wasp shouldn't outrun
    public float lazerSpeed;

    //Private variables
    //Distance from spawnpoint before being destroyed
    private float range;
    //Instantiation point
    private Vector3 spawnPoint;

    // Use this for initialization
    void Start()
    {
        //Set the spawn point on instantiation
        spawnPoint = transform.position;
        //Add a forward force to the lazer shot
        rigidbody.AddForce(transform.forward * lazerSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        //Difference between spawn and current position
        float distance = Vector3.Distance(spawnPoint, transform.position);
        //Enforce a range limit
        if (distance >= range)
        {
            Destroy(gameObject);
            Instantiate(explosion, transform.position, transform.rotation);
        }
    }

    void OnTriggerEnter(Collider collidedWith)
    {
        //First a null guard
        if (collidedWith == null || player == null)
        {
            return;
        }
        //Now check for things you're supposed to pass through, including friendly fire
        if (collidedWith.tag == "Boundary" || collidedWith.tag == "Crystals" || collidedWith.tag == player.tag || collidedWith.tag == "Lazer")
        {
            return;
        }
        //Debug.Log("Collided Tag: " + collidedWith.tag + " and Player Tag: " + player.tag);
        //This is to tell the player getting hit. Used for taking health from player getting hit
        collidedWith.SendMessage("Hit", damage);
        collidedWith.SendMessage("CheckForKill", player);
        //When the lazer was instantiated it was given a reference to whoever instantiated it, stored in player.
        //Sends a message back to the player to indicate whether or not the bullet hit the enemy
        //The "AddToScore" is a method in the PlayerController script and points is a parameter of that method
        player.SendMessage("AddToScore", points);
        //Destroys the lazer gameobject
        Destroy(gameObject);
        Instantiate(explosion, transform.position, transform.rotation);
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

    //For receiving new range via SendMessage from firing object
    void SetRange(float newRange)
    {
        range = newRange;
    }
}
