using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour
{

    public Transform target;//The target the camera needs to lookat and follow

    public float distance;//Distance the camera is behind the target

    public float height;//The height the camera is above the target

    public float lookOffset; //how far forward the camera looks to bring the ship to the bottom of the screen.

    public float heightDamping;//How smooth we want the transition to be from the old height to the current height

    public float rotationDamping;//How smooth we want the rotation to be.

    void LateUpdate()
    {
        if (!target)
        {
            return;
        }

        //Required rotation angles
        float wantedRotationAngle = target.eulerAngles.y;
        float wantedHeight = target.position.y + height;

        //Current rotation angles
        float currentRotationAngle = transform.eulerAngles.y;
        float currentHeight = transform.position.y;

        //Damp the rotation around the y axis
        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

        //Damp height
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

        //Convert angle into rotation
        Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        //Set the position of the camera on the x,z plane
        transform.position = target.position;
        transform.position -= currentRotation * Vector3.forward * distance;

        //Set the height of the camera
        transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);

        Transform centreSpot = target.transform;
        centreSpot.position.Set(transform.position.x, transform.position.y + lookOffset, transform.position.z + lookOffset);

        //Force the camera to look at the target (player)
        transform.LookAt(centreSpot);
    }
}
