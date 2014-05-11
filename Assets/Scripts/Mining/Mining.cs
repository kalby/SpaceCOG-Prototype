using UnityEngine;
using System.Collections;

public class Mining : MonoBehaviour {
    /*IN order for this class to work it requires a gameObject with a "Crystals" tag and a collider checked as "is trigger"
     * Attach this script to any ships capable of mining crystals. One the ships hits the gameObject tagged as "Crystals", a coroutine 
     *is called and add an x amount of crystals to the players account every y amount of seconds. (x and y are defined in the editor)
     *When the ship exits the collider the coroutine is stopped and the player will no longer mine any crystals.
     *
     * NOTE: To see that the player is mining crystals look at the consol for a "Crystal Count:"
     */

    //TODO Visual indication that the player is mining crystals. Was thinking of using a line renderer but that is cause some weird problems atm.

    public GameObject miningBeam;
    public float miningRate;

    private bool inRange;
    private LineRenderer lineRender;
    private GameObject mine;

    private int crystalCount;

	// Use this for initialization
	void Start () {
        Debug.Log("Start Function");
        inRange = false;
        crystalCount = 0;

	}
	
	// Update is called once per frame
	void Update () {
        if (inRange)
        {
            
            //Vector3 shipPos = gameObject.transform.position;
            
            //Instantiate(miningBeam, gameObject.transform.position, gameObject.transform.rotation);
            //Draw the line between the ship and the mine

            //lineRender.SetPosition(0, mine.transform.position);
           // lineRender.SetPosition(1, gameObject.transform.position);//Set first position as the ships position
            
            //lineRender.SetWidth(.45f, .45f);
            //lineRender.SetColors(Color.green, Color.green);      
        }
	}

    //Function sets inRange to true when the ships hits the collider
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Crystals")
        {
            mine = other.gameObject;
            //lineRender = gameObject.AddComponent<LineRenderer>();//Add a line renderer component to the gameObject 
            //lineRender.SetPosition(0, gameObject.transform.position);
            //lineRender.SetPosition(1, mine.transform.position);
            inRange = true;
            StartCoroutine("CrystalGathering");
            
            Debug.Log("In Range? " + inRange);
        }
    }

    //When the ships is no longer inside the sphere collider. 
    //Set the inRange variable to false
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Crystals")
        {
            inRange = false;
            Destroy(gameObject.GetComponent<LineRenderer>());
        }
    }

    //Once in range a player mines 1 crystal every X seconds
    IEnumerator CrystalGathering()
    {        
        while (inRange)
        {
            if (!inRange) break;
            crystalCount += 1;
            Debug.Log("Crystal Count:" + crystalCount);
            yield return new WaitForSeconds(miningRate);
        }        
    }
}
