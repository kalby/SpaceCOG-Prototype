using UnityEngine;
using System.Collections;

/**
 * This script is responsible for testing the range of the laser. Lasers are to be destroyed once they have travelled
 * a certain distance. Each ship has a different range
 */

public class LazerRangeTest : MonoBehaviour {
    // Player ship prefab
    public GameObject player;
    // Laser prefab
    public GameObject lazerObject;
    // Instantiated laser is stored here
    // Made public for testing purposes
    public GameObject lazerTemp;
    // Range of the laser
    public float range;
    // Current position of the laser
    private Vector3 currentPos;
    // Spawn position of the laser
    private Vector3 spawnPosition;

	// Use this for initialization
	void Start () {
        //Instantiate a self-propelled lazer shot at the turret's position
        lazerTemp = Instantiate(lazerObject, Vector3.zero, Quaternion.identity) as GameObject;
        //Set the range the missile can travel before being destroyed
        lazerTemp.SendMessage("SetRange", range);
        //Send this gameObject to the missile for sending back successful hits to the player firing the shot
        lazerTemp.SendMessage("SetPlayer", player);
        spawnPosition = Vector3.zero;
	}

    void Update()
    {
        // Gets the current position of the laser
        currentPos = lazerTemp.transform.position;
        // Calculates the distance the laser has travelled
        float distanceTravelled = Vector3.Distance(currentPos, spawnPosition);
        // Checks distance travelled to range
        if (distanceTravelled > range)
        {
            // Fail the test if the Laser has travlled fruther that the range
            IntegrationTest.Fail(gameObject,"Lazer travel a distance greater than its designated range");
        }
    }

    // The laser gets destroyed when the distanceTravelled is greater that the range
    // Therefore we check for an onDestroy method
    // If this method is called then we know the laser has been destroyed
    void onDestroy()
    {
        // Pass the test of the laser gets destroyed
        IntegrationTest.Pass(gameObject);
    }
}
