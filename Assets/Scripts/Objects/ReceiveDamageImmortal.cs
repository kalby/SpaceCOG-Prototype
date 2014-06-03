using UnityEngine;
using System.Collections;

public class ReceiveDamageImmortal : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void Hit(int damage)
    {
        //Immortals take no damage
        //Do nothing
    }

    //Receive a check on hit that determines a killing blow
    void CheckForKill(GameObject killingPlayer)
    {
        //insert anything here that needs to be sent back to the player on hit
    }

    void Death()
    {
        //Immortals don't die
        //Do nothing
    }

}
