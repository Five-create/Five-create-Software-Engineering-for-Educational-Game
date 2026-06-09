using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour
{
    public Transform player; // Reference to the player character
    public float sensitivity = 5.0f; // Mouse sensitivity
    private float cameraPitch = 0.0f; // Vertical rotation
    private float cameraYaw = 0.0f; // Horizontal rotation

    public float distanceFromPlayer = 3.0f; // Distance from player
    public float heightOffset = 0.5f; // Height offset from the player

    private bool isCursorLocked = true; // Control the cursor lock state
    public GameObject lockpanel;
    public GameObject unlockpanel;

    void Start()
    {
        // Lock the cursor initially
        lockpanel.SetActive(false);
        unlockpanel.SetActive(false);
        LockCursor();
    }

    void Update()
    {
        // Toggle cursor lock state when pressing Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isCursorLocked = !isCursorLocked;

            if (isCursorLocked)
            {
                LockCursor();
            }
            else
            {
                UnlockCursor();
            }
        }
    }

    void LateUpdate()
    {
        if (player != null)
        {
            // Get mouse input
            float mouseX = Input.GetAxis("Mouse X") * sensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

            // Update camera yaw and pitch
            cameraYaw += mouseX;
            cameraPitch -= mouseY;
            cameraPitch = Mathf.Clamp(cameraPitch, -10f, 45f);

            // Set camera rotation based on yaw and pitch
            transform.localRotation = Quaternion.Euler(cameraPitch, cameraYaw, 0f);

            // Calculate the new position for the camera
            Vector3 desiredPosition = player.position - transform.forward * distanceFromPlayer + Vector3.up * heightOffset;
            transform.position = desiredPosition;

            // Make the camera look at the player
            transform.LookAt(player.position + Vector3.up * heightOffset);
        }
    }

    private IEnumerator lockdelay()
    {
        yield return new WaitForSeconds(2f);
        lockpanel.SetActive(false);
    }
    private IEnumerator unlockdelay()
    {
        yield return new WaitForSeconds(2f);
        unlockpanel.SetActive(false);
    }
    public void LockCursor()
    {
        unlockpanel.SetActive(false);
        lockpanel.SetActive(true);
        StartCoroutine(lockdelay());
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center of the screen
        Cursor.visible = false; // Hide the cursor
    }

    public void UnlockCursor()
    {
        lockpanel.SetActive(false);
        unlockpanel.SetActive(true);
        StartCoroutine(unlockdelay());
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor
        Cursor.visible = true; // Show the cursor
    }

}
