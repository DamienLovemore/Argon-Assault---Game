using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    void Awake()
    {
        //Gets the number of how many music players that are in the scene
        MusicPlayer[] listMusicPlayers = FindObjectsOfType<MusicPlayer>();
        //If this is the first music player then set it to not be destroyed
        if (listMusicPlayers.Length == 1)
        {
            DontDestroyOnLoad(this.gameObject);
        }
        //If there is more then one music player then destroy this new one
        else
        {
            Destroy(this.gameObject);
        }
    }
}
