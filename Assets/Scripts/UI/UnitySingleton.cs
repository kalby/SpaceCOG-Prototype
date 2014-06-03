using UnityEngine;
using System.Collections;


//This code almost identical from http://answers.unity3d.com/questions/11314/audio-or-music-to-continue-playing-between-scene-c.html
//Answered by user jashan, we take no academic credit for its structure, only the manner of use.
public class UnitySingleton : MonoBehaviour
{
    private static UnitySingleton instance = null;
    public static UnitySingleton Instance
    {
        get { return instance; }
    }
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
    //end code from jashan
}