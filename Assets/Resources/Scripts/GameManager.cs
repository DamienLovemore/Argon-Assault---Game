using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    //Handles the game shortcuts press
    private void OnProcessShortcuts()
    {
        if (Keyboard.current.escapeKey.isPressed)
        {
            this.CloseGame();
        }
        else if (Keyboard.current.f11Key.isPressed)
        {
            this.ToggleScreenMode();
        }
    }

    //Switch between windowed and fullscreen mode
    private void ToggleScreenMode()
    {
        //Gets the current screen mode of the game
        FullScreenMode currentScreenMode = Screen.fullScreenMode;

        //If the game is in fullscren mode then it switches to windowed mode
        if ((currentScreenMode != FullScreenMode.Windowed) && (currentScreenMode != FullScreenMode.MaximizedWindow))
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
        //If not the game is in windowed mode, and it must switch to fullscreen mode
        else
        {
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        }
    }

    //Closes the game
    private void CloseGame()
    {
        Application.Quit();
    }
}
