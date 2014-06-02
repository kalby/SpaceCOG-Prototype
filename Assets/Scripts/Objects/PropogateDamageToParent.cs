using UnityEngine;
using System.Collections;

public class PropogateDamageToParent : MonoBehaviour {

    //The root parent
    GameObject root;

    // Use this for initialization
    void Start()
    {
        root = UnityEditor.PrefabUtility.FindRootGameObjectWithSameParentPrefab(gameObject);
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
