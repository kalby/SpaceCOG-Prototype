using UnityEngine;
using System.Collections;

public class EnemyPlayerController : MonoBehaviour {
    //This class is for everthing to do with the player except for flight

    //Firing
    public Transform[] turrets;
    public GameObject lazer;
    public float lazerRange;
    public float fireRate;
    public float lazerSpeed;
    public float health;

    //Explosion
    public GameObject shipExplosion;

    private float delayShot;
    private int score;

    private PlayerShipArray playerShipArray;

	// Use this for initialization
	void Start () {
        playerShipArray = GameObject.FindObjectOfType<PlayerShipArray>();
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        ////Players shoots when spacebar is held down
        //if (Input.GetKey(KeyCode.Space))
        //{
            //Time delay to simulate a fire rate
            //if (Time.time > delayShot)
            //{
            //    //Shoot Lazers from both turrets
            //    foreach (Transform t in turrets)
            //    {
            //        GameObject lazerInstance;
            //        lazerInstance = Instantiate(lazer, t.position, t.rotation) as GameObject;
            //        lazerInstance.SendMessage("SetRange", lazerRange);
            //        lazerInstance.rigidbody.AddForce(t.forward * lazerSpeed);
            //        lazerInstance.GetComponent<ProjectileLazer>().player = transform;
            //    }
            //    delayShot = Time.time + fireRate;
            //}
        //}	
	}

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
            //Remove this gameobject from the players list since it has been destroyed
            playerShipArray.allPlayers.Remove(gameObject);

            Instantiate(shipExplosion, transform.position, transform.rotation);
        }
    }

    //This is mainly used to check that a message is being received once a lazer hits an enemy
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
                Destroy(gameObject);
                Instantiate(shipExplosion, transform.position, transform.rotation);
            }
        }
    }
}
