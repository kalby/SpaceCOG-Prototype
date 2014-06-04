using UnityEngine;
using System.Collections;

public class ProjectileLazer : MonoBehaviour
{
    //Explode animation on destruction
    public GameObject explodeAnimation;
    //Damage of lazer shot
    public int damage;
    //Score for hitting enemy players with lazer shot
    public int points;
    //Lazer speed modifier, wasp shouldn't outrun
    public float lazerSpeed;

    //Private variables
    //Player who fired the shot
    private GameObject firingPlayer;
    //Distance from spawnpoint before being destroyed
    private float range;
    //Instantiation point
    private Vector3 spawnPoint;
    //Lazer surface hit location
    private Vector3 surfaceHitPosition;

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
            Instantiate(explodeAnimation, transform.position, explodeAnimation.transform.rotation);
        }
    }

    //Trigger
    //Using no physics on lazers due to performance load and really don't want it to be too realistic. Lazers should always fire straight.
    void OnTriggerEnter(Collider collidedWith)
    {
        //First a null guard
        if (collidedWith == null || firingPlayer == null)
        {
            return;
        }
        //Now check for things you're supposed to pass through, including friendly fire
        if (collidedWith.tag == "Boundary" || collidedWith.tag == "Crystals" || collidedWith.tag == firingPlayer.tag || collidedWith.tag == "Lazer")
        {
            return;
        }
        //Debug.Log("Collided Tag: " + collidedWith.tag + " and Player Tag: " + firingPlayer.tag);
        //This is to tell the player getting hit. Used for taking health from player getting hit
        collidedWith.SendMessage("Hit", damage);
        collidedWith.SendMessage("CheckForKill", firingPlayer);
        //When the lazer was instantiated it was given a reference to whoever instantiated it, stored in player.
        //Sends a message back to the player to indicate whether or not the bullet hit the enemy
        //The "AddToScore" is a method in the PlayerController script and points is a parameter of that method
        firingPlayer.SendMessage("AddToScore", points);
        //Play the explode animation
        Instantiate(explodeAnimation, surfaceHitPosition, explodeAnimation.transform.rotation);
        //Play the explode sound
        PlayClipAt(audio.clip, surfaceHitPosition);
        //Destroys the lazer gameobject
        Destroy(gameObject);
    }

    void OnTriggerExit(Collider other)
    {
        //If the lazer leaves the collider "Boundary" then the gameobject is destroyed
        if (other.tag == "Boundary")
        {
            Destroy(gameObject);
            Instantiate(explodeAnimation, transform.position, transform.rotation);
        }
    }

    //For receiving new range via SendMessage from firing object
    void SetRange(float newRange)
    {
        range = newRange;
    }

    //For receiving the gameObject via SendMessage of the firing object, which is the player firing the lazer
    void SetPlayer(GameObject player)
    {
        firingPlayer = player;
    }

    //credit for this method to link below, using this since the Unity built-in PlayClipAtPoint doesn't allow sound roll-off to be set on the clip
    //http://answers.unity3d.com/questions/316575/adjust-properties-of-audiosource-created-with-play.html
    AudioSource PlayClipAt(AudioClip clip, Vector3 pos)
    {
        GameObject tempGO = new GameObject("TempAudio"); // create the temp object
        tempGO.transform.position = pos; // set its position
        AudioSource aSource = tempGO.AddComponent<AudioSource>(); // add an audio source
        aSource.clip = clip; // define the clip
        aSource.minDistance = 2;
        aSource.maxDistance = 200;
        aSource.rolloffMode = AudioRolloffMode.Logarithmic;
        aSource.Play(); // start the sound
        Destroy(tempGO, clip.length); // destroy object after clip duration
        return aSource; // return the AudioSource reference
    }

    void FixedUpdate()
    {
        //Use raycasting to find the point where the lazer hits, so the animation can be spawned there
        //This prevents having to detect the position of the lazer when it enters the collider
        //When doing this the position is polled too late and the animation and sound play in the object instead of on the surface
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            surfaceHitPosition = hit.point;
        }
    }
}
