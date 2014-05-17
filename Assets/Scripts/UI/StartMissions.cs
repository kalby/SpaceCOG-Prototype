using UnityEngine;
using System.Collections;

public class StartMissions : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void StartMissionsLevel()
    {
        Application.LoadLevel("UIMission");
    }
}
