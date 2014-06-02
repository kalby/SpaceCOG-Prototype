using UnityEngine;
using System.Collections;

public class AeonurReceiveDamage : MonoBehaviour
{
    //Aeonur Animation
    public GameObject aeonurAnimation;
    //Aeonur Death Animation
    public GameObject aeonurDeathAnimation;
    //Aeonur Enter Sound
    public GameObject aeonurEnterSound;
    //Aeonur Death Sound
    public GameObject aeonurDeathSound;
    //Maximum Health of the object
    public int maximumHealth;
    
    //Private Variables
    //Trigger to initiate death sequence
    private bool dieing;
    //Color object to pass in alpha values
    private Color fadeAlpha;
    //Current Health of the object
    private int currentHealth;

    // Use this for initialization
    void Start()
    {
        //Play his entrance noise
        AudioSource.PlayClipAtPoint(aeonurEnterSound.audio.clip, transform.position);
        //Make a colour
        fadeAlpha = Color.white;
        currentHealth = maximumHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //If Death() is called
        if (dieing == true)
        {
            //slow his animation and fade him to destruction
            DeathSequence();
        }
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
            //uncomment this if destroying this gives a kill.
            //killingPlayer.SendMessage("GotKillingBlow");
            //insert anything here that needs to be sent back to the player on death
        }
    }

    //If it can die fill this in
    void Death()
    {
        //Trip the dieing boolean to enable death sequence from the update loop.
        dieing = true;
        //Death sound
        AudioSource.PlayClipAtPoint(aeonurDeathSound.audio.clip, transform.position);
    }

    void DeathSequence()
    {
        //Fade the alpha with time
        fadeAlpha.a -= 0.1f * Time.deltaTime;
        //Get any child objects with the mesh on them
        SkinnedMeshRenderer[] meshes = GetComponentsInChildren<SkinnedMeshRenderer>();
        //Set all the meshes and hence Aeonur to be the fading color
        foreach (SkinnedMeshRenderer mesh in meshes)
        {
            mesh.renderer.material.SetColor("_TintColor", fadeAlpha);
        }
        //Death animation, in this case slow his animation to 15% suddenly
        aeonurAnimation.animation["Take 001"].speed = 0.15f;

        //Destroy Aeonur
        if (fadeAlpha.a <= 0)
        {
            Instantiate(aeonurDeathAnimation, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

}
