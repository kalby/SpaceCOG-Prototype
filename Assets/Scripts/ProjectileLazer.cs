using UnityEngine;
using System.Collections;

public class ProjectileLazer : MonoBehaviour {
    public float speed;

	// Use this for initialization
	void Start () {
        rigidbody.velocity = transform.forward * speed;	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            //Destroys the lazer gameobject
            Destroy(gameObject);
        }
    }


    void OnTriggerExit(Collider other)
    {
        //If the lazer leaves the collider "Boundary" then the gameobject is destroyed
        if (other.tag == "Boundary")
        {
            Destroy(gameObject);
        }
    }
}
