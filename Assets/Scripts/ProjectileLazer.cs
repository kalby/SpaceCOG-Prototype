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
}
