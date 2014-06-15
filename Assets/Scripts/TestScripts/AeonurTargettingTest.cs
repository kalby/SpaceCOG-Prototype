using UnityEngine;
using System.Collections;

/**
 * This script is responsible for ensuring that Aeonur can find a target 
 */
public class AeonurTargettingTest : MonoBehaviour {

    // Declare Aenour variable
    Aeonur a;
	
	void Start () {
        // Find the Aeonur object in the scene
        a = GameObject.FindObjectOfType<Aeonur>();	
	}
	
	// Update is called once per frame
	void Update () {
        //Check if it has a target
        CheckHasTarget();
	}

    //Check to see if Aeonur has a target
    void CheckHasTarget()
    {
        // Check Aeonur's target
        if (a.targetedPlayer != null)
        {
            // Pass test if there is a target
            IntegrationTest.Pass(gameObject);
        }
    }
}
