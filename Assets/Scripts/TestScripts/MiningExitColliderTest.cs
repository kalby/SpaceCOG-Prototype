using UnityEngine;
using System.Collections;

/**
 * This script is responsible for testing when a player leave the crystals mining area
 */

public class MiningExitColliderTest : MonoBehaviour {

    // Ensures ship is inside the crystals collider
    void onTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            return;
        }
        else
        {
            // If the collider doesnt recognise the tag then fail the test
            IntegrationTest.Fail(gameObject);
        }
        
    }

    // When the ship leave the collider the ship can no longer mine sol
    // Therefor we pass the test
    void onTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            // Pass the test when the player exits the collider
            IntegrationTest.Pass(gameObject);
        }
    }
}
