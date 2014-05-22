using UnityEngine;
using System.Collections;

public class Aeonur : MonoBehaviour 
{

    public float health;
    public float speed;
    public float damageTaken;
    //public float damageDealt;
    public float fireRange;
    public float fireRate;
    public GameObject eyePosition;
    public float maxDistance;
    public GameObject explostion;


    private GameObject targetedPlayer;
    private float delayShot;

    private GameObject[] players;

	// Use this for initialization
	void Start () 
    {
        animation.Play();
        FindTarget();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (targetedPlayer != null)
        {
            transform.LookAt(targetedPlayer.transform.position);
            float targetDistance = Vector3.Distance(eyePosition.transform.position, targetedPlayer.transform.position);//Distance between target and Aeonur
            transform.position = Vector3.Lerp(transform.position, targetedPlayer.transform.position, speed);//Moves Aeonur to targets position
            if (targetDistance <= fireRange)
            {
                //Start firing at the target
                if (Time.time > delayShot)
                {
                    targetedPlayer.SendMessage("Hit", CalculateDamageDealt());//Send the amount of damage dealt to the targeted player
                    delayShot = Time.time + fireRate;    
                }
                            
            }
        }
        else
        {            
            FindTarget();
        }
	}

    void FindTarget()
    {
        players = GameObject.FindGameObjectsWithTag("Player");//Finds all gameobjects with a "Player" tag. This is a bit expensive but a quick solution
        foreach (GameObject p in players)
        {
            float distance = Vector3.Distance(eyePosition.transform.position, p.transform.position);

            if (distance <= maxDistance)
            {
                maxDistance = distance;
                targetedPlayer = p;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Lazer")
        {
            if (health >= 0)
            {
                health -= damageTaken;
            }
            else
            {
                Destroy(gameObject);
                //Instantiate(explostion, transform.position, transform.rotation);
            }            
        }
    }

    int CalculateDamageDealt()
    {
        int number = Random.Range(1, 11);

        if (number <= 2)
        {
            return 5;
        }
        else if (number > 2 && number <= 5)
        {
            return 10;
        }
        else if(number >= 6 && number <= 8)
        {
            return 15;
        }else
        {
            return 20;
        }
    }
}
