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
    public float thrusterPower; //probably unnecessary but I'll leave it in for now, the code using it is disabled -D
    public float shipAcceleration;
    public float shipMaxSpeed;

    private float yaw = 0.0f;


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
        yaw = yaw + (moveLeftRight * shipRotationSpeed);
        
        //make the yaw angle loop around so it's not counting rotations
        //preventing silly high numbers
        if(yaw > 360)
        {
            yaw = 0;
        }
        
        currentSpeed = currentSpeed + (moveForwardBack * shipAcceleration);
        if (currentSpeed < 0)
        {
            currentSpeed = 0;
        }
        else if (currentSpeed > shipMaxSpeed)
        {
            currentSpeed = shipMaxSpeed;
        }

        //facing and orthogonal axis
        //cannot use transform.forward or transform.right due to the tilting pushing creating non-zero y component.
        //calculate the x and z unit vector components
        float scalarX = Mathf.Sin((transform.eulerAngles.y * Mathf.PI / 180));
        float scalarZ = Mathf.Cos((transform.eulerAngles.y * Mathf.PI / 180));
        
        //create the ship facing unit vector
        Vector3 facingXZ = new Vector3(scalarX, 0.0f, scalarZ);
        Vector3 orthogonalXZ = Vector3.Cross(facingXZ, Vector3.up);

        //momentum
        //give it forward momentum
        transform.Translate(facingXZ * Time.deltaTime * currentSpeed, Space.World);

        //give it side-to-side momentum, emulates another engine firing to the side. Use to implement strafing later.
        //transform.Translate((moveLeftRight) * orthogonalXZ * Time.deltaTime * thrusterPower, Space.World);

        //rotation
        //Make the ship tilt with pitch and roll based on horizontal and vertical input
        Quaternion rollRotation = Quaternion.AngleAxis(moveLeftRight * (-roll), facingXZ);
        //Quaternion pitchRotation = Quaternion.AngleAxis(moveForwardBack * (-pitch), orthogonalXZ);

        //simple turn rotation about the y axis for steering
        Quaternion yawRotation = Quaternion.AngleAxis(yaw, Vector3.up);

        //set the rotation to be the combination of all rotations.
        transform.rotation = rollRotation * yawRotation;


    }


    //This function is called when a gameobject enters the spaceships box collider.
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Lazer")
        {
            return;
        }
    }
}
