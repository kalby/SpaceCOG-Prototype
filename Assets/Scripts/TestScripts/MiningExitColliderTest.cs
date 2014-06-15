using UnityEngine;
using System.Collections;

public class MiningExitColliderTest : MonoBehaviour {

    //Ensures ship is inside the crystals collider
    void onTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            IntegrationTest.Pass(gameObject);
        }
        
    }

    //When the ship leave the collider the ship can no longer mine sol
    //Therefor we pass the test
    void onTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            IntegrationTest.Pass(gameObject);
        }
    }
}
