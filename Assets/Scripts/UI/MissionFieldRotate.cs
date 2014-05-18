using UnityEngine;
using System.Collections;

public class MissionFieldRotate : MonoBehaviour
{
    public float spinForce;

	// Use this for initialization
	void Start () {
        rigidbody.AddTorque(Vector3.up * spinForce);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
