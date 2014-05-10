using UnityEngine;
using System.Collections;
public class SpaceshipFlight : MonoBehaviour
{

    public float currentSpeed;
    public float shipRotationSpeed;
    public float speedBoost;

    public GameObject LeftTurret;
    public GameObject RightTurret;
    public GameObject lazer;

    public float fireRate;

    private Vector3 moveDirection;
    private float defaultSpeed;
    private float delayShot;

    public float pitch;
    public float roll;
    public float thrusterPower;
    public float shipAcceleration;
    public float shipMaxSpeed;

    private float turnAngle = 0.0f;


    // Use this for initialization
    void Start()
    {

        defaultSpeed = currentSpeed;

    }

    // Update is called once per frame
    void Update()
    {
        //Rotates the spaceship in the direction the players presses.
        //transform.Rotate(0, Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime, 0);

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
        //set the inputs for wasd movement.
        float moveLeftRight = Input.GetAxis("Horizontal");
        float moveForwardBack = Input.GetAxis("Vertical");

        //Adjust the facing as the player turns
        turnAngle = turnAngle + (moveLeftRight*shipRotationSpeed); //*Mathf.Abs(transform.localEulerAngles.z%90)
        currentSpeed = currentSpeed + (moveForwardBack*shipAcceleration);
        if (currentSpeed < 0)
        {
            currentSpeed = 0;
        }
        else if (currentSpeed > shipMaxSpeed)
        {
            currentSpeed = shipMaxSpeed;
        }

        //calculate facing vector
        float facingX = Mathf.Sin((transform.eulerAngles.y * Mathf.PI / 180));
        float facingZ = Mathf.Cos((transform.eulerAngles.y * Mathf.PI / 180));
        Vector3 facingXZ = new Vector3(facingX, 0.0f, facingZ);
        Vector3 horizontal = new Vector3(facingZ, 0.0f, facingX);

        //give it forward momentum
        transform.Translate(facingXZ * Time.deltaTime * currentSpeed, Space.World);

        //give it angular momentum
        //transform.Translate(horizontal * Time.deltaTime * currentSpeed, Space.World);

        //create the thruster vector
        //Vector3 thruster = new Vector3(moveLeftRight, 0.0f, moveForwardBack);

        //adjust velocity with thrusters
        //rigidbody.velocity = thruster * Time.deltaTime * thrusterPower;

        //create a tilting rotation
        Quaternion tiltRotation = Quaternion.Euler(moveForwardBack * (+pitch), 0.0f, moveLeftRight * (-roll));
        //create a turn rotation about the y axis.
        Quaternion turnRotation = Quaternion.Euler(0.0f, turnAngle, 0.0f);
        //set the rotation to be the combination of all rotations.
        rigidbody.rotation = tiltRotation * turnRotation;
        

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Lazer")
        {
            return;
        }
    }
}
