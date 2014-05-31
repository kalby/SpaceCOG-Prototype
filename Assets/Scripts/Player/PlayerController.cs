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
    //Coefficient of the lazer heat (how badly does the heat slow the lazers, lower means a better lazer)
    public float lazerHeatEfficiency;
    //Amount to increase lazer heat by each fire of the set of lazers
    public float lazerHeatClimb;
    //Amount of lazer heat dissipation occuring every frame
    public float lazerHeatDissipate;
    //Base fire rate for lower bound, starting fire rate to simulate
    public float startFireRate;
    //Starting health
    public float maxHealth;
    //Explosion
    public GameObject shipExplosion;

    //Private variables
    //Persistent gameObject for holding the GameWorldControlScript
    public GameObject background;
    //Script for storing persistent variables
    private GameWorldControl gameWorldControlScript;
    //Health remaining at any time
    private float currentHealth;
    //Health remaining as a percentage
    private float healthPercentage;
    //For instantiating lazer shots
    private GameObject lazerShot;
    //Delay shot using fire rate
    private float lazerDelayShot;
    //Lazer heat value
    private float lazerHeat;
    //Penalty on lazer use for hitting heat 100%
    private bool blownLazerCapacitor;

    //Slider from Health Progress Bar
    private UISlider healthSlider;
    //Slider from Lazer Heat Progress Bar
    private UISlider lazerHeatSlider;

    void Start()
    {
        //catch divide-by-zero
        if (maxHealth == 0)
        {
            maxHealth = 1;
        }
        //Initialise values
        currentHealth = maxHealth;

        //Get background GameObject
        background = GameObject.FindWithTag("Background");
        //Get GameWorldControl script from background object
        gameWorldControlScript = background.GetComponent<GameWorldControl>();

        //Get UI Elements
        //Health Progress Bar
        GameObject healthProgressBar = GameObject.FindWithTag("HealthProgressBar");
        healthSlider = healthProgressBar.GetComponent<UISlider>();

        //Laser Heat Progress Bar
        GameObject lazerHeatProgressBar = GameObject.FindWithTag("LazerHeatProgressBar");
        lazerHeatSlider = lazerHeatProgressBar.GetComponent<UISlider>();
    }

    void Update()
    {
        //Calculate values
        if (currentHealth < 0)
        {
            //Ensure current health non-negative at any frame
            currentHealth = 0;
        }
        healthPercentage = currentHealth / maxHealth;
        //Check Inputs
        GetKeys();
        //Update the UI
        lazerHeatSlider.value = lazerHeat;
        healthSlider.value = healthPercentage;

        //Heat dissipation
        if (lazerHeat > 0)
        {
            lazerHeat -= lazerHeatDissipate;
            if (lazerHeat <= 0)
            {
                //Ensure it can't go negative
                lazerHeat = 0;
            }
        }
        //Check for overheat
        if (lazerHeat >= 1)
        {
            //Upper bound of 1 for max heat
            lazerHeat = 1;
            //fired too much, overheated
            blownLazerCapacitor = true;
            //imaginary capacitor takes 2 seconds to repair/replace
            StartCoroutine(ReplaceCapacitor(2.0f));
        }
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
    //CO-ROUTINES
    //For enabling lazer fire again after overheat
    private IEnumerator ReplaceCapacitor(float seconds)
    {
        //Bit of a hack, loop twice, first loop takes 'seconds' to complete
        //On second loop replace capacitor
        for (int i = 0; i < 2; i++)
        {
            //Second loop, capacitor replaced
            if (i == 1)
            {
                blownLazerCapacitor = false;
            }
            yield return new WaitForSeconds(seconds);
        }
    }

    //WEAPONS
    void ShootLazer()
    {
        //Shoot Lazers from all turrets
        if (Time.time > lazerDelayShot && blownLazerCapacitor != true)
        {
            foreach (Transform turretPosition in turrets)
            {
                //Instantiate a self-propelled lazer shot at the turret's position
                lazerShot = Instantiate(lazer, turretPosition.position, turretPosition.rotation) as GameObject;
                //Set the range the lazer shot can travel before being destroyed
                lazerShot.SendMessage("SetRange", lazerRange);
                //Send this gameObject to the lazer for sending back successful hits to the player firing the shot
                lazerShot.SendMessage("SetPlayer", gameObject);
                //Add the current ship velocity to the lazer shot, these are bolts of hot plasma or whatever, they act like bullets.
                lazerShot.rigidbody.velocity += rigidbody.velocity;
            }
            //Firing the lazer makes the turret hotter
            //If the heat isn't already maxed out
            if (lazerHeat < 1f)
            {
                //increase the laser heat value
                lazerHeat += lazerHeatClimb;
            }
            //Set the delay until can fire again, modified by heat
            lazerDelayShot = Time.time + startFireRate + (lazerHeatEfficiency * lazerHeat);
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
            Death();
        }
    }

    //MESSAGES
    //Add to the players score when receiving back an AddToScore message
    void AddToScore(int points)
    {
        gameWorldControlScript.AddScore(points);
    }

    void Hit(int damage)
    {
        if (currentHealth > 0)
        {
            //apply the damage
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                //Bound the health if it went negative
                currentHealth = 0;
                //Start respawn method
                if (gameObject != null)
                {
                    Destroy(gameObject);
                }
                Instantiate(shipExplosion, transform.position, transform.rotation);
                Death();
            }
        }
    }

    //Receive a check on hit that determines a killing blow
    void CheckForKill(GameObject killingPlayer)
    {
        if (currentHealth == 0)
        {
            //ugh, don't know how to avoid this ping pong
            killingPlayer.SendMessage("GotKillingBlow");
        }
    }

    //Call this when the player reduces an enemy players current health to 0
    void GotKillingBlow()
    {
        Kill();
    }

    void Death()
    {
        //You died, update your death count
        gameWorldControlScript.AddDeaths(1);
    }

    void Kill()
    {
        //You got a kill, update your kill count
        gameWorldControlScript.AddKills(1);
    }
}
