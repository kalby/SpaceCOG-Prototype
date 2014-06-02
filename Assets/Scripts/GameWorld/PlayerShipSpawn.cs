using UnityEngine;
using System.Collections;

//This script is used by the buttons in the ship selection panel
public class PlayerShipSpawn : MonoBehaviour
{

    //Holds the prefab ships the player may choose to spawn, one at a time
    public GameObject wasp;
    public GameObject mustang;
    public GameObject hermit;
    //The Game World Control, contains team values that are set by the network
    public GameWorldControl gameWorldControl;
    //The Player Ship Array, contains an array of all ships and allows some injection of fake players
    public PlayerShipArray playerShipArray;

    //Private Variables
    //Main Camera
    private GameObject mainCamera;
    //PlayerCamera script from Main Camera
    private PlayerCamera playerCameraScript;
    //Holds the current GameObject ship that the player can control, can only ever be one of these... At least not without fundamental game mechanic re-design
    private GameObject playerShip;
    //The point in space the ship spawns at
    private Transform spawnPoint;
    //The tag to assign to the instantiated player ship
    private string playerTag;
    //The Sol cost to spawn this ship
    private int shipCost;
    //Not enough Sol Object with label on it
    private GameObject notEnoughSolObject;

    // Use this for initialization
    void Start()
    {
        //Check what team the player is on
        if (gameWorldControl.teamOne)
        {
            //Get the first team spawn point
            GameObject spawnPointTeamOne = GameObject.FindWithTag("SpawnPointTeamOne");
            //Pull out the transform
            Transform spawnPointTeamOneTransform = spawnPointTeamOne.transform;
            //Set the spawn point
            spawnPoint = spawnPointTeamOneTransform;
            //Set the tag to assign on instantiate
            playerTag = "TeamOne";
        }
        else if (gameWorldControl.teamTwo)
        {
            //Get the second team spawn point
            GameObject spawnPointTeamTwo = GameObject.FindWithTag("SpawnPointTeamTwo");
            //Pull out the transform
            Transform spawnPointTeamTwoTransform = spawnPointTeamTwo.transform;
            //Set the spawn point
            spawnPoint = spawnPointTeamTwoTransform;
            //Set the tag to assign on instantiate
            playerTag = "TeamTwo";
        }
        else if (gameWorldControl.teamThree)
        {
            //Get the third team spawn point
            GameObject spawnPointTeamThree = GameObject.FindWithTag("SpawnPointTeamThree");
            //Pull out the transform
            Transform spawnPointTeamThreeTransform = spawnPointTeamThree.transform;
            //Set the spawn point
            spawnPoint = spawnPointTeamThreeTransform;
            //Set the tag to assign on instantiate
            playerTag = "TeamThree";
        }

        //Get the Main Camera
        mainCamera = GameObject.FindWithTag("MainCamera");
        playerCameraScript = mainCamera.GetComponent<PlayerCamera>();

        //Get the UI Elements
        //Not enough Sol object
        notEnoughSolObject = GameObject.FindWithTag("NotEnoughSolLabel");
        //Hide the not enough Sol warning
        NGUITools.SetActive(notEnoughSolObject, false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SelectWasp()
    {
        //Hide the not enough Sol warning
        NGUITools.SetActive(notEnoughSolObject, false);
        //Set the cost of the ship
        shipCost = 200;
        //Remove any old one, calling this on null seems fine
        Destroy(playerShip);
        //Instantiate the Wasp at the spawn point position with its rotation
        playerShip = Instantiate(wasp, spawnPoint.position, spawnPoint.rotation) as GameObject;
        //Give it the right tag, have the camera follow its lookPoint and possibly later, other options
        ConfigureShipSettings();
    }

    public void SelectMustang()
    {
        //Hide the not enough Sol warning
        NGUITools.SetActive(notEnoughSolObject, false);
        //Set the cost of the ship
        shipCost = 300;
        //Remove any old one, calling this on null seems fine
        Destroy(playerShip);
        //Replace with new one
        playerShip = Instantiate(mustang, spawnPoint.position, spawnPoint.rotation) as GameObject;
        //Give it the right tag, have the camera follow its lookPoint and possibly later, other options
        ConfigureShipSettings();
    }

    public void SelectHermit()
    {
        //Hide the not enough Sol warning
        NGUITools.SetActive(notEnoughSolObject, false);
        //Set the cost of the ship
        shipCost = 80;
        //Remove any old one, calling this on null seems fine
        Destroy(playerShip);
        //Replace with new one
        playerShip = Instantiate(hermit, spawnPoint.position, spawnPoint.rotation) as GameObject;
        //Give it the right tag, have the camera follow its lookPoint and possibly later, other options
        ConfigureShipSettings();
    }

    private void ConfigureShipSettings()
    {
        //Give it the current players tag
        playerShip.tag = playerTag;
        //Make the Main Camera follow this newly minted ship
        Transform transform = playerShip.GetComponentInChildren<Transform>();
        foreach (Transform child in transform)
        {
            if (child.name == "LookAtPoint")
            {
                playerCameraScript.target = child.transform;
            }
        }
    }

    public void BuyShip()
    {
        if (playerShip != null)
        {
            //If the player has enough sol
            if (gameWorldControl.GetSol() >= shipCost)
            {
                gameWorldControl.SubtractSol(shipCost);
                //Enable PlayerControl and PlayerFlight
                playerShip.GetComponent<PlayerController>().enabled = true;
                playerShip.GetComponent<PlayerFlight>().enabled = true;
                if (playerShip.GetComponent<Mining>() != null)
                {
                    playerShip.GetComponent<Mining>().enabled = true;
                }
                //Make the Ship Selection Panel inactive
                NGUITools.SetActive(gameObject, false);
                //Send the global GameWorldControl script the player ship
                gameWorldControl.SendMessage("SetPlayerShip", playerShip);
                //Add the player ship to the game controller ship array
                playerShipArray.SendMessage("AddPlayer", playerShip);
            }
            else
            {
                NGUITools.SetActive(notEnoughSolObject, true);
            }
        }
    }
}
