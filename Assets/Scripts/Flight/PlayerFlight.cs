using UnityEngine;
using System.Collections;
public class PlayerFlight : MonoBehaviour
{
    //Firing
    public Transform[] turrets;
    public GameObject lazer;
    public float fireRate;
    public float lazerSpeed;

    //rotation
    public float maxRollAngle;
    public float rotationSpeed;

    //velocity variables
    public float acceleration;
    public float maxSpeed;
    public float currentSpeed;
    public float speedBoost;
    //To do for later, I'll leave it in for now, the code using it is disabled -D
    public float thrusterPower;

    //private Variables
    private float yaw = 0.0f;
    private float delayShot;
    private int score;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //INPUTS
        //set the inputs for wasd movement.
        //should be in update not forcedUpdate() as "input flags aren't reset until update() is called."

        //side-to-side.
        float horizontal = Input.GetAxis("Horizontal");
        //forwards and backwards.
        float vertical = Input.GetAxis("Vertical");

        //STEERING
        //Adjust the facing as the player turns.
        yaw = yaw + (horizontal * rotationSpeed);

        //make the yaw angle loop around so it's not counting rotations.
        //prevents possible overflow.
        if (yaw > 360 || yaw < -360)
        {
            yaw = 0;
        }

        //VELOCITY CONTROL
        currentSpeed = currentSpeed + (vertical * acceleration);
        if (currentSpeed < 0f)
        {
            currentSpeed = 0f;
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
        //Vector3 orthogonalXZ = Vector3.Cross(facingXZ, Vector3.up);

        //MOMENTUM
        //give it forward momentum in the XZ plane.
        transform.Translate(facingXZ * Time.deltaTime * currentSpeed, Space.World);

        //give it side-to-side momentum, emulates another engine firing to the side. Use to implement thrusters later.
        //transform.Translate((horizontal) * orthogonalXZ * Time.deltaTime * thrusterPower, Space.World);

        //ROTATION
        Quaternion rotation = Quaternion.Euler(0.0f, yaw, -maxRollAngle * horizontal * Mathf.Sign(currentSpeed));
        //Use slerp to sexy (spherical interpolate) up that rotation!
        Quaternion slerpedRotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10);
        transform.rotation = slerpedRotation;

        //Players shoots when spacebar is held down
        if (Input.GetKey(KeyCode.Space))
        {
            //Time delay to simulate a fire rate
            if (Time.time > delayShot)
            {                
                //Shoot Lazers from both turrets
                foreach (Transform t in turrets)
                {                    
                    GameObject lazerInstance;
                    lazerInstance = Instantiate(lazer, t.position, t.rotation) as GameObject;
                    lazerInstance.rigidbody.AddForce(t.forward * lazerSpeed);
                    lazerInstance.GetComponent<ProjectileLazer>().player = transform;
                }
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
        //if (other.tag == "Lazer")
        //{
        //    return;
        //}
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Boundary")
        {
            Destroy(gameObject);
        }
    }

    //This is mainly used to check that a message is being received once a lazer hits an enemy
    void AddToScore(int points)
    {
        score += points;
        Debug.Log("Player score: " + score);
    }
}
