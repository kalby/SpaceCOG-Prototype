using UnityEngine;
using System.Collections;


//This code almost identical from http://answers.unity3d.com/questions/11314/audio-or-music-to-continue-playing-between-scene-c.html
//Answered by user jashan, we take no academic credit for its structure, only the manner of use.
public class UnitySingletonCursor : MonoBehaviour
{
    //Credit for this texture goes to Deviant Art user JJ-Ying
    //yingjunjiu@yahoo.com.cn
    //yingjunjiu.77g.net
    //http://jj-ying.deviantart.com/art/Azenis-Cursors-24338463
    //I added a glow

    //Cursor Texture
    public Texture2D cursorTexture;
    
    private static UnitySingletonCursor instance = null;
    public static UnitySingletonCursor Instance
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

    void Start()
    {
        //Set the cursor texture
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    }
}