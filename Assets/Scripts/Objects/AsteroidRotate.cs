using UnityEngine;
using System.Collections;

public class AsteroidRotate : MonoBehaviour
{
    //Some random axis of rotation for the asteroid to spin on.
    private Vector3 rotationAxisVector;
    //The amount of torque applied to the asteroid.
    private float rotationForce = 0.01f;

    // Use this for initialization
    void Start()
    {
        rotationAxisVector = randomVector();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        //set the physics rigidbody to spin the asteroid.
        rigidbody.AddTorque(rotationAxisVector * rotationForce);
    }

    private Vector3 randomVector()
    {
        return rotationAxisVector = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
    }
}
