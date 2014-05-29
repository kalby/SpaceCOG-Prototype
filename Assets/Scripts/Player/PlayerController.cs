using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    //This class is for everthing to do with the player except for flight

    //Firing
    //The list of transform positions to fire lazer shots from
    public Transform[] turrets;
    //The lazer shot to instantiate
    public GameObject lazer;
    //The range until the lazer is destroyed
    public float lazerRange;
    //Fire rate to simulate
    public float fireRate;
    //Scalar multiplier on forward vector
    public float lazerSpeed;
    //Damage taken before explosion
    public float health;

    //Explosion
    public GameObject shipExplosion;

    //Private variables
    //Delay on shot
    private float delayShot;
    //Increases as game progresses used to acquire rank up. (future implementation, persistent reward)
    private int score;
    //For respawning ships and incrementing while mining sol
    private int sol;
    //For instantiating lazer shots
    private GameObject lazerShot;

    void Start()
    {

    }

    void Update()
    {
        //Check Inputs
        GetKeys();
    }

    //INPUT
    void GetKeys()
    {
        //Player shoots when space or left mouse button is pressed or held down
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0))
        {
            ShootLazer();
        }
    }

    //WEAPONS
    void ShootLazer()
    {
        //Shoot Lazers from all turrets
        if (Time.time > delayShot)
        {
            foreach (Transform turretPosition in turrets)
            {
                //Instantiate a self-propelled lazer shot at the turret's position
                lazerShot = Instantiate(lazer, turretPosition.position, turretPosition.rotation) as GameObject;
                //Set the range the lazer shot can travel before being destroyed
                lazerShot.SendMessage("SetRange", lazerRange);
                //Add the player object to the lazer for sending back successful hits to the player firing the shot
                lazerShot.GetComponent<ProjectileLazer>().player = gameObject;
                //Set the tag of the shot to be of the same type as the player who fired it
                lazerShot.tag = gameObject.tag;
            }
            delayShot = Time.time + fireRate;
        }
    }

    //TRIGGERS
    //This function is called when a gameobject enters the spaceships box collider.
    void OnTriggerEnter(Collider other)
    {
        //if (other.tag == "Lazer")
        //{
        //    return;
        //}
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Boundary")
        {
            Destroy(gameObject);
            Instantiate(shipExplosion, transform.position, transform.rotation);
        }
    }

    //MESSAGES
    //Add to the players score when receiving back an AddToScore message
    void AddToScore(int points)
    {
        score += points;
        Debug.Log("Player score: " + score);
    }

    void Hit(int damage)
    {
        Debug.Log("Player Health: " + health);
        if (health > 0)
        {
            health -= damage;
            if (health <= 0)
            {
                //Someone got a kill on you, update their kill count, update your death count
                //Start respawn method
                Destroy(gameObject);
                Instantiate(shipExplosion, transform.position, transform.rotation);
            }
        }
    }
}
