using UnityEngine;
using System.Collections;

public class Aeonur : MonoBehaviour
{
    public float speed;
    public float fireRange;
    public float fireRate;
    public GameObject eyePosition;
    public float maxDistance;
    public LineRenderer lineRender;
    public GameObject aeonurAnimation;

    //Get the allPlayers list from the gameController
    public ArrayList pl;

    private GameObject targetedPlayer;
    private float delayShot;
    private GameObject playerShipArray;


    // Use this for initialization
    void Start()
    {
        aeonurAnimation.animation.Play();
        FindTarget();
    }

    // Update is called once per frame
    void Update()
    {
        if (targetedPlayer != null)
        {
            transform.LookAt(targetedPlayer.transform.position);
            float targetDistance = Vector3.Distance(eyePosition.transform.position, targetedPlayer.transform.position);//Distance between target and Aeonur
            transform.position = Vector3.Lerp(transform.position, targetedPlayer.transform.position, Time.deltaTime * speed);//Moves Aeonur to targets position
            //Debug.Log("Distance between player and target: " + targetDistance);
            if (targetDistance <= fireRange)
            {
                //Start firing at the target
                if (Time.time > delayShot)
                {
                    Ray ray = new Ray(eyePosition.transform.position, eyePosition.transform.forward);
                    lineRender.SetPosition(0, ray.origin);//Set first position as the Aeonur's position
                    lineRender.SetPosition(1, targetedPlayer.transform.position);//Set second position as the target's position
                    targetedPlayer.SendMessage("Hit", CalculateDamageDealt());//Send the amount of damage dealt to the targeted player
                    delayShot = Time.time + fireRate;
                }
            }
            else
            {
                maxDistance = 1000;
                FindTarget();
            }
        }
        else
        {
            maxDistance = 1000;//Reset the maxDistance so that a new target can be found
            FindTarget();
        }
    }

    void FindTarget()
    {
        //Reference the Player Ship Array in the scene to access its public variables;
        PlayerShipArray playerShipArray = GameObject.FindObjectOfType<PlayerShipArray>();

        //Set the allPlayers list from the Player Ship Array
        pl = playerShipArray.allPlayers;

        foreach (GameObject p in pl)
        {
            if (p != null)
            {
                float distance = Vector3.Distance(transform.position, p.transform.position);

                if (distance <= maxDistance)
                {
                    maxDistance = distance;
                    targetedPlayer = p;
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {

    }

    int CalculateDamageDealt()
    {
        int number = Random.Range(1, 11);

        if (number <= 2)//1 or 2
        {
            return 0;
        }
        else if (number == 3 || number == 4)//3 or 4
        {
            return 5;
        }
        else if (number >= 5 && number <= 7)//5, 6 or 7
        {
            return 10;
        }
        else if (number == 8 || number == 9)//8 or 9
        {
            return 15;
        }
        else //10
        {
            return 20;
        }
    }
}
