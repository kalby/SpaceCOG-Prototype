using UnityEngine;
using System.Collections;

public class ReceiveDamage : MonoBehaviour
{
    //Maximum Health of the object
    public int maximumHealth;

    //Current Health of the object
    public int currentHealth;

    // Use this for initialization
    void Start()
    {
        currentHealth = maximumHealth;
    }

    // Update is called once per frame
    void Update()
    {

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
        //Explode animation
        //Blow up sound

        if (gameObject != null)
        {
            Destroy(gameObject);
        }
    }

}
