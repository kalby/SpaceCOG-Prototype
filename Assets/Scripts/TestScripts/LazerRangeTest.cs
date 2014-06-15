using UnityEngine;
using System.Collections;

public class LazerRangeTest : MonoBehaviour {

    public GameObject player;
    public GameObject lazerObject;
    public GameObject lazerTemp;
    public float range;


    private Vector3 currentPos;
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
        currentPos = lazerTemp.transform.position;
        float distanceTravelled = Vector3.Distance(currentPos, spawnPosition);
        if (distanceTravelled > range)
        {
            IntegrationTest.Fail(gameObject,"Lazer travel a distance greater than its designated range");
        }
    }

    void onDestroy()
    {
        IntegrationTest.Pass(gameObject);
    }
}
