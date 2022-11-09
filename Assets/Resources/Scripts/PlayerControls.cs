using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [Header("Player Control Keys")]
    [SerializeField] private InputAction playerMovement;

    [Header("Movement Tuning")]
    [SerializeField] private float movementSpeed = 30.43f;
    [SerializeField] private float xRange = 10f;
    [SerializeField] private float yRange = 7f;
        
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
        float rawXPosition = transform.localPosition.x + horizontalMovement;
        float rawYPosition = transform.localPosition.y + verticalMovement;

        //Clamps the values so that they stay in a range where the player
        //cannot fly of the screen bounds
        float clampedXPosition = Mathf.Clamp(rawXPosition, -xRange, xRange);
        float clampedYPosition = Mathf.Clamp(rawYPosition, -yRange, yRange);

        //Uses localPosition instead of position because the player is not a free object,
        //it is inside a parent object so it moves its position within the parents position
        transform.localPosition = new Vector3(clampedXPosition, clampedYPosition, transform.localPosition.z);
    }
}
