using UnityEngine;
using System.Collections;

/**
 * This script is responsible for testing when a player comes into contact with a crystals collider
 * that it is recognised
 */
public class MiningEnterColliderTest : MonoBehaviour {
    // When the player enters the collider this method is called
    void onTriggerEnter(Collider other)
    {
        // If the players tag is equal to "Player"
        if (other.tag == "Player")
        {
            // Pass the test
            IntegrationTest.Pass(gameObject);
        }
        else
        {
            // Fail the test if the "Player" tag isnt recognised
            IntegrationTest.Fail(gameObject, "GameObject not a mining object");
        }
    }
}
