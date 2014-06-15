using UnityEngine;
using System.Collections;

public class MiningEnterColliderTest : MonoBehaviour {

    void onTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            IntegrationTest.Pass(gameObject);
        }
        else
        {
            IntegrationTest.Fail(gameObject, "GameObject not a mining object");
        }
    }
}
