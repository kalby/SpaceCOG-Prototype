using UnityEngine;
using System.Collections;
public class PlayerFlight : MonoBehaviour
{
    //This class is to be attached to any ship a player may fly
    //Rotation
    public float maxRollAngle;
    //Used for Yaw
    public float rotationSpeed;

    //Particle System Engines
    public GameObject[] engine;

    //Velocity Variables
    public float acceleration;
    public float maxSpeed;
    public float currentSpeed;

    //Sound Variables
    public GameObject engineSound;

    //Private Variables
    //The current heading
    private float yaw;
    //Slider from Thrust Progress Bar
    private UISlider thrustSlider;

    void Start()
    {
        //Initialisiation values
        //Ensure max speed is non-zero so that calculations don't divide by zero
        if (maxSpeed == 0)
        {
            maxSpeed = 1;
        }
        //Max speed should be units per second not per frame
        maxSpeed /= 60;

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
        //float tx = transform.forward.x;
        //float tz = transform.forward.z;
        //create the ship facing vector in the XZ plane.
        //Vector3 facingXZ = new Vector3(tx, 0.0f, tz);
        //create the facing orthogonal vector in the XZ plane.
        //Vector3 orthogonalXZ = Vector3.Cross(facingXZ, Vector3.up);

        //STEERING
        //Adjust the facing as the player turns.
        yaw = yaw + (horizontal * rotationSpeed) * Time.deltaTime * 10f;
        //Make the yaw angle loop around preventing possible overflow
        if (yaw > 360 || yaw < -360)
        {
            yaw = 0;
        }

        //ROTATION
        //Add in "* Mathf.Sign(currentSpeed)" to the Z roll if correct rolling with reverse thrust is desired
        Quaternion shipRotation = Quaternion.Euler(0.0f, yaw, -maxRollAngle * horizontal * Mathf.Sign(currentSpeed));
        //Spherically Interpolate the rotation
        Quaternion interpolatedRotation = Quaternion.Slerp(transform.rotation, shipRotation, Time.deltaTime * 5f);
        transform.rotation = interpolatedRotation;

        //VELOCITY CONTROL
        //Increment velocity on vertical input by acceleration
        currentSpeed = Mathf.Lerp(currentSpeed, currentSpeed + (vertical * acceleration), Time.deltaTime * 40f);
        //enforce speed limit
        if (currentSpeed < 0)
        {
            currentSpeed = 0;

        }
        else if (currentSpeed > maxSpeed)
        {
            currentSpeed = maxSpeed;
        }
        //Update the UI
        thrustSlider.value = currentSpeed / maxSpeed;

        //MOVEMENT
        //move it forward in the XZ plane
        //rigidbody.MovePosition(rigidbody.position + facingXZ * currentSpeed);

        //THRUSTER AUDIO
        engineSound.audio.volume = 0.2f * (currentSpeed / maxSpeed);
    }

    void FixedUpdate()
    {
        rigidbody.AddRelativeForce(Vector3.forward * currentSpeed * 100f);
    }
}
