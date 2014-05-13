using UnityEngine;
using System.Collections;
public class SpaceshipFlight : MonoBehaviour
{
    //Firing
    public GameObject LeftTurret;
    public GameObject RightTurret;
    public GameObject lazer;
    public float fireRate;

    //roll shouldn't exceed 60 degrees, since it causes gimbal lock related issues, excessive spinning and jerking.
    //the way to get around this is to apply a small rotation each frame and send the new rotation through again to be incremented in the next frame.
    //This avoids euler angle conversions but loses the simple input based control and requires more intensive angle checking and rotation interpolation.
    //see the code I gave up on at the end of the file. May return to it later.
    //rotation
    public float roll;
    public float rotationSpeed;

    //velocity variables
    public float acceleration;
    public float maxSpeed;
    public float currentSpeed;
    public float speedBoost;
    //To do for later, I'll leave it in for now, the code using it is disabled -D
    public float thrusterPower;

    //private Variables
    private float pitch = 0.0f;
    private float yaw = 0.0f;
    private float defaultSpeed = 0.0f;
    private float delayShot;


    // Use this for initialization
    void Start()
    {
        defaultSpeed = currentSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        //INPUTS
        //set the inputs for wasd movement.
        //should be in update not forcedUpdate() as "input flags aren't reset until update() is called."
        //It uses far less resources and is smoother, I tested it.

        //side-to-side.
        float horizontal = Input.GetAxis("Horizontal");
        //forwards and backwards.
        float vertical = Input.GetAxis("Vertical");

        //STEERING
        //Adjust the facing as the player turns.
        yaw = yaw + (horizontal * rotationSpeed);

        //make the yaw angle loop around so it's not counting rotations.
        //prevents possible overflow.
        if (yaw > 360)
        {
            yaw = 0;
        }

        //VELOCITY CONTROL
        currentSpeed = currentSpeed + (vertical * acceleration);
        if (currentSpeed < -1)
        {
            currentSpeed = -1;
        }
        else if (currentSpeed > maxSpeed)
        {
            currentSpeed = maxSpeed;
        }

        //AXES
        //cannot use raw transform.forward due to the rotation creating non-zero y component.
        float tx = transform.forward.x;
        float tz = transform.forward.z;

        //create the ship facing vector in the XZ plane.
        Vector3 facingXZ = new Vector3(tx, 0.0f, tz);
        //create the horizontal vector in the XZ plane.
        Vector3 orthogonalXZ = Vector3.Cross(facingXZ, Vector3.up);

        //MOMENTUM
        //give it forward momentum in the XZ plane.
        transform.Translate(facingXZ * Time.deltaTime * currentSpeed, Space.World);

        //give it side-to-side momentum, emulates another engine firing to the side. Use to implement thrusters later.
        //transform.Translate((horizontal) * orthogonalXZ * Time.deltaTime * thrusterPower, Space.World);

        //ROTATION
        //Make the ship tilt with pitch and roll based on horizontal and vertical input
        Quaternion rollRotation = Quaternion.AngleAxis(horizontal * (-roll) * Mathf.Sign(currentSpeed), transform.forward);
        //Zero'd out by pitch, fix for gimbal lock related issue. For now no forward or backward tilt.
        //Quaternion pitchRotation = Quaternion.AngleAxis(vertical * (-pitch) * Mathf.Sign(currentSpeed), transform.right);
        //yaw rotation about the global y axis for steering locked to the XZ plane.
        Quaternion yawRotation = Quaternion.AngleAxis(yaw, Vector3.up);
        //set the rotation to be the combination of all rotations.
        Quaternion targetRotation = rollRotation * yawRotation;
        //Use slerp to sexy (spherical interpolate) up that rotation!
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 1);

        /* SPECIAL NOTE: with this implementation itt doesn't cumulatively add small amounts to the transform and save each frame
         * there are bugs with combined rotations and high roll. A 60 degree or lower roll is safe.*/

        //Give the player a speed boost when LeftShift is held down
        //Returns to normal speed when LeftShift is released
        /*if (Input.GetKey(KeyCode.LeftShift))
        {
            //Screen.lockCursor = false;
            defaultSpeed = currentSpeed;
            currentSpeed = speedBoost;
        }
        else
        {
            currentSpeed = defaultSpeed;
            //Screen.lockCursor = true;
        }*/


        //Players shoots when spacebar is held down
        if (Input.GetKey(KeyCode.Space))
        {
            //Time delay to simulate a fire rate
            if (Time.time > delayShot)
            {
                //Shoot Lazers from both turrets
                Instantiate(lazer, LeftTurret.transform.position, LeftTurret.transform.rotation);
                Instantiate(lazer, RightTurret.transform.position, RightTurret.transform.rotation);
                delayShot = Time.time + fireRate;
            }
        }


    }

    void FixedUpdate()
    {
        //physics and collision detection go here. Not Input controlled movement though.
    }


    //This function is called when a gameobject enters the spaceships box collider.
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Lazer")
        {
            return;
        }
    }
    //    Ignore this code, here for possible bug fixing later.
    //    //rotation
    //    //make the ship rotate with pitch and roll based on horizontal and vertical input, for banking motion.
    //    Quaternion rollRotation = Quaternion.AngleAxis(-horizontal, Vector3.forward);
    //    Quaternion pitchRotation = Quaternion.AngleAxis(vertical, Vector3.right);
    //    //yaw rotation about the y axis for steering.
    //    Quaternion yawRotation = Quaternion.AngleAxis(horizontal, Vector3.up);

    //    Quaternion currentRotation = transform.localRotation;
    //    float rollAngle = currentRotation.eulerAngles.z;
    //    float pitchAngle = currentRotation.eulerAngles.x;
    //    float yawAngle = currentRotation.eulerAngles.y;

    //    float lockRollRight = 340;
    //    float lockRollLeft = 20;
    //    float lockPitchBack = 340;
    //    float lockPitchForward = 20;
    //    float referenceDown = 180;
    //    //Debug.Log(pitchAngle);

    //    Quaternion rollUp = Quaternion.FromToRotation(Vector3.up, transform.right);

    //    if (rollAngle < lockRollRight && rollAngle > referenceDown)
    //    {
    //        transform.rotation = Quaternion.Slerp(transform.rotation, rollUp, 0.001f);
    //    }
    //    else if (rollAngle > lockRollLeft && rollAngle < referenceDown)
    //    {
    //        transform.rotation = Quaternion.Slerp(transform.rotation, rollUp, 0.001f);
    //    }
    //    else
    //    {
    //        //set the rotation to be the combination of all rotations.
    //        transform.rotation = transform.rotation * rollRotation * pitchRotation * yawRotation;
    //    }
}
