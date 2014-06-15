using UnityEngine;
using System.Collections;

public class AeonurMovementTest : MonoBehaviour {

    Aeonur a;

    private Vector3 startPos;
    private Vector3 currentPos;
	// Use this for initialization
	void Start () {
        a = GameObject.FindObjectOfType<Aeonur>();
        startPos = a.transform.position;

	}
	
	// Gets the starting position and current position of aeonur.
    // Find the distance between the two vector3s and once the distance is greater than 10
    // then we know aeonur has moved. A lesser value can be used but this ensures movement
    // has occured
	void Update () {
        currentPos = a.transform.position;
        float distanceTravelled = Vector3.Distance(currentPos, startPos);

        if (distanceTravelled > 10)
        {
            IntegrationTest.Pass(gameObject);
        }
	}
}
