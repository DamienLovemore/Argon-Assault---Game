using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private InputAction playerMovement;
    [SerializeField] private float movementSpeed = 10f;
        
    void Start()
    {
        
    }

    //When the script becomes enabled and active, sets the keys
    //of movement to be listened
    void OnEnable()
    {
        playerMovement.Enable();
    }

    //When the script is disabled/unloaded disables the keys
    //listening for player movement
    void OnDisable()
    {
        playerMovement.Disable();
    }

    void FixedUpdate()
    {
        //Reads how much left, right and up, down the player wants to go
        Vector2 playerInput = this.playerMovement.ReadValue<Vector2>();

        //Separates the values into horizontal and vertical axis,
        //and tunes it with the speed and makes it framerate independent
        float horizontalMovement = playerInput.x * this.movementSpeed * Time.deltaTime;
        float verticalMovement = playerInput.y * this.movementSpeed * Time.deltaTime;

        //Calculates where the player new position should be
        float newXPosition = transform.localPosition.x + horizontalMovement;
        float newYPosition = transform.localPosition.y + verticalMovement;

        //Uses localPosition instead of position because the player is not a free object,
        //it is inside a parent object so it moves its position within the parents position
        transform.localPosition = new Vector3(newXPosition, newYPosition, transform.localPosition.z);
    }
}
