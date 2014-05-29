using UnityEngine;
using System.Collections;
public class PlayerFlight : MonoBehaviour
{
    //Rotation
    public float maxRollAngle;
    //Used for Yaw
    public float rotationSpeed;

    //Particle System Engines
    public GameObject[] engine;

    //Velocity variables
    public float acceleration;
    public float maxSpeed;
    public float currentSpeed;

    //Private Variables
    //The current heading
    private float yaw;

    //Slider from Thrust Progress Bar
    private UISlider thrustSlider;

    void Start()
    {
        //Ensure max speed is non-zero so that percentage calculations don't throw an exception
        if (maxSpeed == 0)
        {
            maxSpeed = 0.1f;
        }

        //Set the frame rate
        Application.targetFrameRate = 60;

        //Get UI Elements
        //Thrust Progress Bar
        GameObject thrustProgressBar = GameObject.FindWithTag("ThrustProgressBar");
        thrustSlider = thrustProgressBar.GetComponent<UISlider>();
    }

    //Update is called once per frame
    void Update()
    {
        //PARTICLE SYSTEM
        if (currentSpeed <= 0f)
        {
            foreach (GameObject engineStream in engine)
            {
                engineStream.particleSystem.enableEmission = false;
            }
        }
        else
        {
            foreach (GameObject engineStream in engine)
            {
                engineStream.particleSystem.enableEmission = true;
                engineStream.particleSystem.emissionRate = currentSpeed / maxSpeed * 100f;
            }
        }

        //INPUTS
        //Side-to-side.
        float horizontal = Input.GetAxis("Horizontal");
        //Forwards and backwards.
        float vertical = Input.GetAxis("Vertical");

        //AXES
        //Can't use raw transform.forward due to non-zero y component
        float tx = transform.forward.x;
        float tz = transform.forward.z;
        //create the ship facing vector in the XZ plane.
        Vector3 facingXZ = new Vector3(tx, 0.0f, tz);
        //create the facing orthogonal vector in the XZ plane.
        //Vector3 orthogonalXZ = Vector3.Cross(facingXZ, Vector3.up);

        //STEERING
        //Adjust the facing as the player turns.
        yaw = yaw + (horizontal * rotationSpeed) * Time.deltaTime * 10f;
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
        //Update the UI
        thrustSlider.value = currentSpeed / maxSpeed;

        //MOVEMENT
        //move it forward in the XZ plane, works with collidables
        rigidbody.MovePosition(rigidbody.position + facingXZ * currentSpeed);
    }
}
