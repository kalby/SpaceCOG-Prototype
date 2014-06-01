using UnityEngine;
using System.Collections;

public class Mining : MonoBehaviour {
    /*IN order for this class to work it requires a gameObject with a "Crystals" tag and a collider checked as "is trigger"
     * Attach this script to any ships capable of mining crystals. Once the ships is in range of the gameObject tagged as "Crystals", a coroutine 
     *is called which adds x amount of crystals to the players statistics every y amount of seconds. (x and y are defined in the editor)
     *When the ship exits the collider the coroutine is stopped and the player will no longer mine any crystals.
     *
     * Visually a line renderer is used to indicate to the player that they are actually mining crystals as oppose to just watching a counter tick over
     *
     * NOTE: To see that the player is mining crystals look at the console for a "Crystal Count:"
     */


    public float miningRate;//Rate at which a player mines crystals

    private bool inRange;
    private LineRenderer lineRender;
    private GameObject mine;

    //The GameWorldControl script which contains player's Sol crystal count
    private GameWorldControl gameWorldControl;


	// Use this for initialization
	void Start () {
        //Get The background object
        GameObject background = GameObject.FindWithTag("Background");
        gameWorldControl = background.GetComponent<GameWorldControl>();

        inRange = false;
        lineRender = gameObject.GetComponentInChildren<LineRenderer>();
        lineRender.enabled = false;

	}
	
	// Update is called once per frame
	void Update () {
        if (inRange)
        {

            //Draw the line between the ship and the mine
            Ray ray = new Ray(transform.position, transform.forward);
            lineRender.SetPosition(0, ray.origin);//Set first position as the ships position
            lineRender.SetPosition(1, mine.transform.position);//Set second position as the crystals position    
        }
	}

    //Function sets inRange to true when the ships hits the collider
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Crystals")
        {
            mine = other.gameObject;
            inRange = true;
            StartCoroutine("CrystalGathering");
            lineRender.enabled = true;
            //Debug.Log("In Range? " + inRange);
        }
    }

    //When the ships is no longer inside the sphere collider. 
    //Set the inRange variable to false and turn of the line renderer
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Crystals")
        {
            inRange = false;
            lineRender.enabled = false;
        }
    }

    //Once in range a player mines 1 crystal every X seconds
    IEnumerator CrystalGathering()
    {        
        while (inRange)
        {
            if (!inRange) break;
            gameWorldControl.SendMessage("AddSol", 1);
            yield return new WaitForSeconds(miningRate);
        }
    }
}
