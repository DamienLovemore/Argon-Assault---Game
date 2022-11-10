using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [Header("Player Control Keys")]
    [Tooltip("The inputs that the game should use for movement")] [SerializeField] private InputAction playerMovement;
    [Tooltip("The inputs that the game should use for firing")] [SerializeField] private InputAction playerFiring;

    [Header("Laser Cannons")]
    [Tooltip("The GameObjects that contains the ship laser cannons")] [SerializeField] private GameObject[] lasers;

    [Header("Movement Tuning")]
    [Tooltip("How fast the ship moves with the user input")] [SerializeField] private float movementSpeed = 30.43f;
    [Tooltip("How far in the horizontal axis the ship can go")] [SerializeField] private float xRange = 10f;
    [Tooltip("How far in the vertical axis the ship can go")] [SerializeField] private float yRange = 7f;

    [Header("Rotation Tuning")]
    [SerializeField] private float positionPitchFactor = -2f;
    [SerializeField] private float controlPitchFactor = -15f;    
    [SerializeField] private float positionYawFactor = -2.2f;
    [SerializeField] private float controlRollFactor = -20f;  
    
    private float xThrow;
    private float yThrow;
        
    //When the script becomes enabled and active, sets the keys
    //of inputs to be listened
    void OnEnable()
    {
        playerMovement.Enable();
        playerFiring.Enable();
    }

    //When the script is disabled/unloaded disables the keys
    //listening for player inputs
    void OnDisable()
    {
        playerMovement.Disable();
        playerFiring.Disable();
    }

    void FixedUpdate()
    {
        this.ShipMovement();
        this.ShipRotation();
        this.ShipFiring();
    }

    private void ShipMovement()
    {
        //Reads how much left, right and up, down the player wants to go
        Vector2 playerInput = this.playerMovement.ReadValue<Vector2>();

        //Stores the values of input for movement, so that rotation
        //function can use it
        this.xThrow = playerInput.x;
        this.yThrow = playerInput.y;
    
        //Separates the values into horizontal and vertical axis,
        //and tunes it with the speed and makes it framerate independent
        float horizontalMovement = this.xThrow * this.movementSpeed * Time.deltaTime;
        float verticalMovement = this.yThrow * this.movementSpeed * Time.deltaTime;

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

    private void ShipRotation()
    {
        //Calculates the pitch accordinly to the position of the ship
        float pitchDueToPosition = transform.localPosition.y * this.positionPitchFactor;
        //Calculates the pitch accordinly to the input of the user
        //(How much it holds the key)
        float pitchDueToControlThrow = this.yThrow * this.controlPitchFactor;

        //Rotation for the ship in the x axis
        float pitch = pitchDueToPosition + pitchDueToControlThrow;

        //Rotation for the ship in the y axis
        //(For y axis we use just the calculation DueToPosition)
        float yaw = transform.localPosition.x * this.positionYawFactor;

        //Rotation for the ship in the z axis
        //(For the z axis we use just the calculation DueToControlThrow)
        float roll = this.xThrow * this.controlRollFactor;

        //Uses local instead of rotation because the object is inside a parent object,
        //it is not a free object
        //(Euler is used to represent rotations)
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll); 
    }

    private void ShipFiring()
    {
        //Reads how much the fire key is being pressed. (For keyboards is just 0 or 1,
        //but for controllers it is a value between 0 and 1, like 0.5 for example)
        float playerInput = this.playerFiring.ReadValue<float>();

        //If it is being pressed (indepentendly if it is keyboard or controller)
        if (playerInput > 0.5)
        {
            this.SetLasersActive(true);
        }
        else
        {
            this.SetLasersActive(false);
        }
    }

    private void SetLasersActive(bool isShootingLasers)
    {
        foreach (GameObject laserCannon in this.lasers)
        {
            //As we are not sure which type is the module of a component, we use
            //var so the system take care of the type for us
            var laserShooter = laserCannon.GetComponent<ParticleSystem>().emission;
            laserShooter.enabled = isShootingLasers;
        }
    }
}
