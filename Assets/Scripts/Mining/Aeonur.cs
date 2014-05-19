using UnityEngine;
using System.Collections;

public class Aeonur : MonoBehaviour 
{

    public float health;
    public float speed;
    public float damageTaken;
    public float damageDealt;
    public float fireRange;
    public float fireRate;
    public GameObject eyePosition;
    public float maxDistance;

    private GameObject targetedPlayer;


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
            float range = Vector3.Distance(eyePosition.transform.position, targetedPlayer.transform.position);
            transform.position = Vector3.Lerp(transform.position, targetedPlayer.transform.position, speed);
            if (range <= fireRange)
            {
                //Start firing at the target
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
            health -= damageTaken;
        }
    }
}
