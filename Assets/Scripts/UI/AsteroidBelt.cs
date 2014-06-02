using UnityEngine;
using System.Collections;

public class AsteroidBelt : MonoBehaviour {
    public GameObject asteroidBelt;
    public float speed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.up * Time.deltaTime * speed);
	}
}
