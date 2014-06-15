using UnityEngine;
using System.Collections;

public class AeonurTargettingTest : MonoBehaviour {

    Aeonur a;
	// Use this for initialization
	void Start () {
        a = GameObject.FindObjectOfType<Aeonur>();	
	}
	
	// Update is called once per frame
	void Update () {
        CheckHasTarget();
	}

    //Check to see if Aeonur has a target
    void CheckHasTarget()
    {
        if (a.targetedPlayer != null)
        {
            IntegrationTest.Pass(gameObject);
        }
    }
}
