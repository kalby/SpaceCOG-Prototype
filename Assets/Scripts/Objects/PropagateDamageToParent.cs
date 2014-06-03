using UnityEngine;
using System.Collections;

public class PropagateDamageToParent : MonoBehaviour {

    //The root GameObject
    GameObject root;

    // Use this for initialization
    void Start()
    {
        root = transform.root.gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Hit(int damage)
    {
        root.SendMessage("Hit", damage);
    }

    //Receive a check on hit that determines a killing blow
    void CheckForKill(GameObject killingPlayer)
    {
        root.SendMessage("CheckForKill", killingPlayer);
    }
}
