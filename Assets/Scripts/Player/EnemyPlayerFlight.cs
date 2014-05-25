using UnityEngine;
using System.Collections;
public class EnemyPlayerFlight : MonoBehaviour
{

    //rotation
    public float maxRollAngle;
    public float rotationSpeed;

    public float range;

    //Particle System Engine
    public GameObject[] engine;

    //velocity variables
    public float acceleration;
    public float maxSpeed;
    public float currentSpeed;
    public float speedBoost;
    //To do for later, I'll leave it in for now, the code using it is disabled -D
    public float thrusterPower;

    //private Variables
    private float yaw = 0.0f;

    private GameObject target;

    private float delayTime;

    private float horizontal;

    void Start()
    {
        horizontal = 0f;
    }

    // Update is called once per frame
    void Update()
    {        
        //INPUTS
        //set the inputs for wasd movement.
        //should be in update not forcedUpdate() as "input flags aren't reset until update() is called."

        //side-to-side.
        
        if (Time.time > delayTime)
        {
            horizontal = Random.Range(-1, 2);
            delayTime = Time.time + 3f;
        }
        
        //forwards and backwards.
        float vertical = 1f;        
        
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
    }
}
