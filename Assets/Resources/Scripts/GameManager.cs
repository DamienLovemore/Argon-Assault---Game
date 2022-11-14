using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [Header("Game Shortcuts")]
    [SerializeField] private InputAction toggleScreenModeKey;
    [SerializeField] private InputAction closeGameKey;

    //When the script becomes enabled and active, sets the keys
    //of inputs to be listened
    void OnEnable() 
    {
        toggleScreenModeKey.Enable();
        closeGameKey.Enable();
    }

    //When the script is disabled/unloaded disables the keys
    //listening for player inputs
    void OnDisable() 
    {
        toggleScreenModeKey.Disable();
        closeGameKey.Disable();
    }

    void FixedUpdate() 
    {
        this.ProcessShortcuts();
    }

    //Handles the game shortcuts press
    private void ProcessShortcuts()
    {
        //Listen if the player has pressed the keys assigned to one of the
        //input actions of the shortcuts (0 not pressed, 1 pressed)
        float toggleScreenInput = this.toggleScreenModeKey.ReadValue<float>();
        float closeGameInput = this.closeGameKey.ReadValue<float>();
        
        if (toggleScreenInput > 0.5)
        {
            this.ToggleScreenMode();
        }
        else if (closeGameInput > 0.5)
        {
            this.CloseGame();
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
