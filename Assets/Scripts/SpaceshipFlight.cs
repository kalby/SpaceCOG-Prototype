using UnityEngine;
using System.Collections;
public class SpaceshipFlight : MonoBehaviour
{

    public float currentSpeed;
    public float rotationSpeed;
    public float speedBoost;

    public GameObject LeftTurret;
    public GameObject RightTurret;
    public GameObject lazer;

    public float fireRate;

    private Vector3 moveDirection;
    private float defaultSpeed;
    private float delayShot;

    public float tilt;
    //public float tiltSpeed;
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
        turnAngle = turnAngle + moveLeftRight;

        //give it forward momentum
        transform.Translate(Vector3.forward * Time.deltaTime * currentSpeed, Space.World);

        //create the movement vector
        Vector3 movement = new Vector3(moveLeftRight, 0.0f, moveForwardBack);
        //make the movement affect velocity, essentially thrusters
        rigidbody.velocity = movement;
        //create a tilting rotation
        Quaternion tiltRotation = Quaternion.Euler(rigidbody.velocity.z * +tilt, 0.0f, rigidbody.velocity.x * -tilt);
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
