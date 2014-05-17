using UnityEngine;
using System.Collections;

//This code very similar to the script on http://answers.unity3d.com/questions/11314/audio-or-music-to-continue-playing-between-scene-c.html
//Answered by user Rimrook, we take no academic credit for its structure, only the manner of use and choice to use it in the design.
public class MusicManager : MonoBehaviour
{

    //create the audio field
    public AudioClip backgroundMusic;

    void Awake()
    {
        //Get the Game Music persistent singleton
        GameObject gameMusic = GameObject.FindWithTag("PersistentSingleton");

        //Make sure the same track isn't already playing.
        if (gameMusic.audio.clip != backgroundMusic)
        {
            //Assign whatever was placed in the Music Manager, even none.
            gameMusic.audio.clip = backgroundMusic;
            //Play that music now.
            gameMusic.audio.Play();
        }
    }
}
