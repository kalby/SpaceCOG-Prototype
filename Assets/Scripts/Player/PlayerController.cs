using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    //This class is for everthing to do with the player except for flight

    //Firing
    //The number of missiles carried
    public int missileAmmo;
    //The list of transform positions to fire lazer shots from
    public Transform[] turrets;
    //The lazer shot to instantiate
    public GameObject lazer;
    //Lazer firing sound
    public GameObject lazerSound;
    //The range until the lazer is destroyed
    public float lazerRange;
    //Coefficient of the lazer heat (how badly does the heat slow the lazers)
    public float lazerHeatDeficiency;
    //Amount to increase lazer heat by each fire of the set of lazers
    public float lazerHeatClimb;
    //Amount of lazer heat dissipation occuring every frame
    public float lazerHeatDissipate;
    //Amount of time it takes to cool off from overheat/replace capacitor
    public float lazerOverheatPenalty;
    //LazerOverheatSound
    public GameObject lazerOverheatSound;
    //Base fire rate for lower bound, starting fire rate to simulate
    public float lazerFireRate;

    //The transform position to fire missiles from
    public Transform[] missileLaunchPoints;
    //The missile to instantiate
    public GameObject missile;
    //Missile firing sound
    public GameObject missileSound;
    //The range until the lazer is destroyed
    public float missileRange;
    //The missile fire rate
    public float missileFireRate;

    
    //Starting health
    public float maxHealth;
    //Explosion animation
    public GameObject shipExplosionAnimation;
    //Explosion Sound
    public GameObject shipExplosionSound;
    //Ship Collide Sound
    public GameObject shipCollideSound;
    

    //Private variables
    //Persistent gameObject for holding the GameWorldControlScript
    public GameObject background;
    //Script for storing persistent variables
    private GameWorldControl gameWorldControlScript;
    //Health remaining at any time
    public float currentHealth;
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

    //For instantiating missiles
    private GameObject missileFired;
    //Delay missile fire using
    private float missileDelayFire;

    //Slider from Health Progress Bar
    private UISlider healthSlider;
    //Slider from Lazer Heat Progress Bar
    private UISlider lazerHeatSlider;
    //Slider from Missile Slot 1
    private UISlider missileSlot1Slider;
    //Slider from Missile Slot 2
    private UISlider missileSlot2Slider;
    //Slider from Missile Slot 3
    private UISlider missileSlot3Slider;
    //Slider from Missile Slot 4
    private UISlider missileSlot4Slider;
    //Slider from Missile Slot 5
    private UISlider missileSlot5Slider;

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

        //Missile Slot 1 Progress Bar
        GameObject MissileSlot1ProgressBar = GameObject.FindWithTag("MissileSlot1ProgressBar");
        missileSlot1Slider = MissileSlot1ProgressBar.GetComponent<UISlider>();

        //Missile Slot 2 Progress Bar
        GameObject MissileSlot2ProgressBar = GameObject.FindWithTag("MissileSlot2ProgressBar");
        missileSlot2Slider = MissileSlot2ProgressBar.GetComponent<UISlider>();

        //Missile Slot 3 Progress Bar
        GameObject MissileSlot3ProgressBar = GameObject.FindWithTag("MissileSlot3ProgressBar");
        missileSlot3Slider = MissileSlot3ProgressBar.GetComponent<UISlider>();

        //Missile Slot 4 Progress Bar
        GameObject MissileSlot4ProgressBar = GameObject.FindWithTag("MissileSlot4ProgressBar");
        missileSlot4Slider = MissileSlot4ProgressBar.GetComponent<UISlider>();

        //Missile Slot 5 Progress Bar
        GameObject MissileSlot5ProgressBar = GameObject.FindWithTag("MissileSlot5ProgressBar");
        missileSlot5Slider = MissileSlot5ProgressBar.GetComponent<UISlider>();

        //Fill the missile Slots
        SetMissileSliders();
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

        //Update the UI
        lazerHeatSlider.value = lazerHeat;
        healthSlider.value = healthPercentage;

        //Check for overheat
        if (lazerHeat >= 1)
        {
            //Upper bound of 1 for max heat
            lazerHeat = 1;
            //fired too much, overheated
            blownLazerCapacitor = true;
            lazerOverheatSound.audio.Play();
            //imaginary capacitor takes 0.5 seconds to repair/replace/cool-off
            StartCoroutine(ReplaceCapacitor(lazerOverheatPenalty));
        }

        //Heat dissipation while not firing
        if (lazerHeat > 0)
        {
            lazerHeat -= lazerHeatDissipate;
            if (lazerHeat <= 0)
            {
                //Ensure it can't go negative
                lazerHeat = 0;
            }
        }
        //Check Inputs
        GetKeys();
    }

    //INPUT
    void GetKeys()
    {
        //Player shoots when space or left mouse button is pressed or held down
        if (Input.GetKey(KeyCode.Mouse0))
        {
            ShootLazer();
        }
        if (Input.GetKey(KeyCode.Space))
        {
            FireMissile();
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
                //play the shoot lazer sound
                lazerSound.audio.Play();
            }
            //Firing the lazer makes the turret hotter
            //If the heat isn't already maxed out
            if (lazerHeat < 1f)
            {
                //increase the laser heat value
                lazerHeat += lazerHeatClimb;
            }
            //Set the delay until can fire again, modified by heat
            lazerDelayShot = Time.time + lazerFireRate + (lazerHeatDeficiency * lazerHeat);
        }
            
    }

    void FireMissile()
    {
        //Shoot a missile from the launcher
        if (Time.time > missileDelayFire && missileAmmo > 0)
        {
            foreach (Transform missileLauncher in missileLaunchPoints)
            {
                //Instantiate a self-propelled lazer shot at the turret's position
                missileFired = Instantiate(missile, missileLauncher.position, missileLauncher.rotation) as GameObject;
                //Set the range the missile can travel before being destroyed
                missileFired.SendMessage("SetRange", missileRange);
                //Send this gameObject to the missile for sending back successful hits to the player firing the shot
                missileFired.SendMessage("SetPlayer", gameObject);
                //Add the current ship velocity to the missile fired.
                missileFired.rigidbody.velocity += rigidbody.velocity;
                //play the shoot lazer sound
                missileSound.audio.Play();
                //reduce the missile carried by the ship by 1
                missileAmmo -= 1;
                //Update the UI
                SetMissileSliders();
            }
            //Set the delay until can launch again
            missileDelayFire = Time.time + missileFireRate;
        }
    }

    //TRIGGERS
    //This function is called when a gameobject enters the spaceships box collider.
    void OnTriggerEnter(Collider collidedWith)
    {
        //Check for things you're supposed to pass through without a hit sound or already has its own
        if (collidedWith.tag == "Boundary" || collidedWith.tag == "Crystals" || collidedWith.tag == "Lazer")
        {
            return;
        }
        //Debug.Log("Collided Tag: " + collidedWith.tag + " and Player Tag: " + tag);
        //Debug.Log(collidedWith);
    }

    //COLLISION
    //This function is called when the ship begins touching another collider.
    //One must be non-kinematic, always true if ship is non-kinematic, as I've set them.
    void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > 2)
        {
            AudioSource.PlayClipAtPoint(shipCollideSound.audio.clip, transform.position);
            //Take some damage relative to how hard you got hit
            Hit((int) Mathf.Floor(collision.relativeVelocity.magnitude));
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Boundary")
        {
            Death();
        }
    }

    //MESSAGES
    //Add to the players score when receiving back an AddToScore message
    void AddToScore(int points)
    {
        gameWorldControlScript.AddScore(points);
    }

    public void Hit(int damage)
    {
        if (currentHealth > 0)
        {
            //apply the damage
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                //Bound the health if it went negative
                currentHealth = 0;
                //Die
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
        //Explode animation
        Instantiate(shipExplosionAnimation, transform.position, transform.rotation);
        //Blow up sound
        AudioSource.PlayClipAtPoint(shipExplosionSound.audio.clip, transform.position);
        //Update your death count
        //You died
        if (gameObject != null)
        {
            Destroy(gameObject);
        }
        gameWorldControlScript.AddDeaths(1);
    }

    void Kill()
    {
        //You got a kill, update your kill count
        gameWorldControlScript.AddKills(1);
    }

    //UI Setters
    //Set the sliders in the UI depending on the ammo count.
    //Crap code but had to be done quick
    void SetMissileSliders()
    {
        if (missileAmmo == 5)
        {
            missileSlot1Slider.value = 1;
            missileSlot2Slider.value = 1;
            missileSlot3Slider.value = 1;
            missileSlot4Slider.value = 1;
            missileSlot5Slider.value = 1;
        }
        if (missileAmmo == 4)
        {
            missileSlot1Slider.value = 1;
            missileSlot2Slider.value = 1;
            missileSlot3Slider.value = 1;
            missileSlot4Slider.value = 1;
            missileSlot5Slider.value = 0;
        }
        if (missileAmmo == 3)
        {
            missileSlot1Slider.value = 1;
            missileSlot2Slider.value = 1;
            missileSlot3Slider.value = 1;
            missileSlot4Slider.value = 0;
            missileSlot5Slider.value = 0;
        }
        if (missileAmmo == 2)
        {
            missileSlot1Slider.value = 1;
            missileSlot2Slider.value = 1;
            missileSlot3Slider.value = 0;
            missileSlot4Slider.value = 0;
            missileSlot5Slider.value = 0;
        }
        if (missileAmmo == 1)
        {
            missileSlot1Slider.value = 1;
            missileSlot2Slider.value = 0;
            missileSlot3Slider.value = 0;
            missileSlot4Slider.value = 0;
            missileSlot5Slider.value = 0;
        }
        if (missileAmmo == 0)
        {
            missileSlot1Slider.value = 0;
            missileSlot2Slider.value = 0;
            missileSlot3Slider.value = 0;
            missileSlot4Slider.value = 0;
            missileSlot5Slider.value = 0;
        }

    }
}
