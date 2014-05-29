using UnityEngine;
using System.Collections;
public class PlayerFlight : MonoBehaviour
{

    //Rotation
    public float maxRollAngle;
    public float rotationSpeed;

    //Particle System Engine
    public GameObject[] engine;

    //Velocity variables
    public float acceleration;
    public float maxSpeed;
    public float currentSpeed;

    //Private Variables
    private float yaw;

    void Start()
    {
        Application.targetFrameRate = 60;
    }

    //Update is called once per frame
    void Update()
    {
        //Particle System control
        if (currentSpeed <= 0f)
        {
            foreach (GameObject p in engine)
            {
                p.particleSystem.enableEmission = false;
            }
        }
        else
        {
            foreach (GameObject p in engine)
            {
                p.particleSystem.enableEmission = true;
            }
        }

        //INPUTS
        //Side-to-side.
        float horizontal = Input.GetAxis("Horizontal");
        //Forwards and backwards.
        float vertical = Input.GetAxis("Vertical");

        //AXES
        //Cannot use raw transform.forward due to the rotation creating non-zero y component
        float tx = transform.forward.x;
        float tz = transform.forward.z;
        //create the ship facing vector in the XZ plane.
        Vector3 facingXZ = new Vector3(tx, 0.0f, tz);
        //create the sideways vector in the XZ plane.
        //Vector3 orthogonalXZ = Vector3.Cross(facingXZ, Vector3.up);

        //STEERING
        //Adjust the facing as the player turns.
        yaw = yaw + (horizontal * rotationSpeed * 1000) * Time.deltaTime * 0.01f;

        //Make the yaw angle loop around preventing possible overflow
        //Causes a small stutter on loopback but is high enough... now... it will practically never occur
        if (yaw > 36000 || yaw < -36000)
        {
            yaw = 0;
        }

        //ROTATION
        //Add in "* Mathf.Sign(currentSpeed)" to the Z roll if correct rolling with reverse thrust is desired
        Quaternion shipRotation = Quaternion.Euler(0.0f, yaw, -maxRollAngle * horizontal);
        //Spherically Interpolate the rotation
        Quaternion interpolatedRotation = Quaternion.Slerp(transform.rotation, shipRotation, Time.deltaTime * 5f);
        transform.rotation = interpolatedRotation;

        //VELOCITY CONTROL
        //Increment velocity on vertical input by acceleration
        currentSpeed = Mathf.Lerp(currentSpeed, currentSpeed + (vertical * acceleration), Time.deltaTime * 5f);
        //Prevent reversing and enforce speed limit
        if (currentSpeed < 0f)
        {
            currentSpeed = 0f;

        }
        else if (currentSpeed > maxSpeed)
        {
            currentSpeed = maxSpeed;
        }

        //MOVEMENT
        //move it forward in the XZ plane with collision.
        rigidbody.MovePosition(rigidbody.position + facingXZ * currentSpeed);
        
    }

    void FixedUpdate()
    {

    }
}
