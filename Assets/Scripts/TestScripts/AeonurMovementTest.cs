using UnityEngine;
using System.Collections;
/**
 * The follow script is responsible for testing the movement of aeonur to ensure that once
 * it finds a target it starts to move towards that target.
 */

public class AeonurMovementTest : MonoBehaviour {

    // Variable of type Aeonur declared
    Aeonur a;

    // Starting position of Aeonur
    private Vector3 startPos;
    // Current position of Aeonur
    private Vector3 currentPos;

	void Start () {
        // Find Aeonur within the scene to get its position
        a = GameObject.FindObjectOfType<Aeonur>();
        // Get the starting point of Aeonur
        startPos = a.transform.position;
	}
	
	// Gets the starting position and current position of aeonur.
    // Find the distance between the two vector3s and once the distance is greater than 10
    // then we know aeonur has moved. A lesser value can be used but this ensures movement
    // has occured
	void Update () {
        // Get the current position of Aeonur
        currentPos = a.transform.position;
        // Calculate the distance Aeonur has travelled
        float distanceTravelled = Vector3.Distance(currentPos, startPos);

        //Check if the distance travelled has been met
        if (distanceTravelled > 10)
        {
            // Pass the test when the above becomes true
            IntegrationTest.Pass(gameObject);
        }
	}
}
