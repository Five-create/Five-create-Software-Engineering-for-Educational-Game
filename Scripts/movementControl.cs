using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class movementControl : MonoBehaviour
{
    // References
    PlayerInput playerInput;
    CharacterController characterController;
    Animator animator;

    // Variables for player inputs
    Vector2 currentMovementInput;
    bool isMovementPressed;
    float rotationPerFrame = 10.0f;

    // Speed
    [SerializeField] private float movementSpeed = 7.0f;
    [SerializeField] private float hackSpeed = 20.0f;
    [SerializeField] private bool SpeedHack = false;

    // Gravity
    bool isGrounded;
    Vector3 velocity;
    float jumpHeight = 1.0f;      // Controls how high the player jumps
    bool isJumpPressed = false;    // Track if jump is pressed

    //player movement permission
    bool canMove = true;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        playerInput.CharacterControl.Move.started += onMovementInput;
        playerInput.CharacterControl.Move.canceled += onMovementInput;
        playerInput.CharacterControl.Move.performed += onMovementInput;

        // Bind jump action
        playerInput.CharacterControl.jump.started += onJumpInput;
        playerInput.CharacterControl.jump.canceled += onJumpInput;
        playerInput.CharacterControl.jump.performed += onJumpInput;
    }

    void onJumpInput(InputAction.CallbackContext context)
    {
        if (context.started && isGrounded)
        {
            isJumpPressed = true; // Set jump pressed flag
        }
    }


    void onMovementInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }

    //enable and disable movement is accessed in playerNameInput calss
    public void EnableMovement()
    {
        canMove = true;
    }

    public void DisableMovement()
    {
        canMove = false;
    }

    void directionPosition()
    {
        if (canMove && isMovementPressed)
        {
            Vector3 movementDirection = new Vector3(currentMovementInput.x, 0, currentMovementInput.y);
            movementDirection = Camera.main.transform.TransformDirection(movementDirection);
            movementDirection.y = 0; // Keep movement flat

            if (movementDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationPerFrame * Time.deltaTime);
            }

            characterController.Move(movementDirection * movementSpeed * Time.deltaTime);
        }
    }

    void animationStates()
    {
        // Reset states at the beginning
        animator.SetBool("isRunning", false);
        animator.SetBool("isWalking", false);
        animator.SetBool("isJumping", false);

        if (playerInput.CharacterControl.jump.IsPressed() && isJumpPressed && canMove)
        {
            animator.SetBool("isJumping", true); // Trigger jump animation
            isJumpPressed = false; // Reset jump input after handling
        }

        // Check for walking input (when movement is pressed)
        if (playerInput.CharacterControl.Move.IsPressed() && canMove)
        {
            if(SpeedHack)
            {
                movementSpeed = hackSpeed;
            }
            else
            {
                movementSpeed = 7.0f;
            }
            animator.SetBool("isWalking", true);
//7, 12
            // Check for running input (use a new input action if you're using the Input System)
            if (playerInput.CharacterControl.run.IsPressed())
            {
                movementSpeed = 12.0f;
                animator.SetBool("isRunning", true);
            }
        }

        // Reset jump animation when grounded
        if (isGrounded)
        {
            animator.SetBool("isJumping", false); // Stop jump animation when grounded
        }
    }

    IEnumerator HideWarningAfterDelay(float wait)
    {
        yield return new WaitForSeconds(wait); // Wait for the specified time
        isJumpPressed = false; // Reset jump input after applying jump
    }

    void gravity()
    {
        isGrounded = characterController.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0;
        }

        // Handle jumping
        if (isGrounded && isJumpPressed && canMove)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
            StartCoroutine(HideWarningAfterDelay(1f));
        }

        velocity.y += Physics.gravity.y * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    void Update()
    {
        gravity();
        directionPosition();
        animationStates();
    }

    void OnEnable()
    {
        playerInput.CharacterControl.Enable();
    }

    void OnDisable()
    {
        playerInput.CharacterControl.Disable();
    }
}
