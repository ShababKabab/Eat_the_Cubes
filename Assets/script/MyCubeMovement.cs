using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI; // Include this for UI elements
using UnityEngine.SceneManagement; // For scene management

public class MyCubeMovement : MonoBehaviour
{
    private AudioSource audioSource;
    private PlayerInput input = null;
    private Vector2 moveVector = Vector2.zero;
    private Rigidbody rb = null;
    private float moveSpeed = 5f;
    public float detectionRadius = 10f; // Radius to detect nearby cubes
    public LayerMask cubeLayer; // Layer for detecting other cubes
    // private float planeSize = 50f; // Plane size for boundary clamping
    public GameObject gameOverPanel; // Assign the Game Over Panel here in the Inspector
    public Text gameOverText; // Assign the Game Over Text here in the Inspector
    public float delayBeforeMainMenu = 3f; // Delay before returning to the main menu

    private void Awake()
    {
        input = new PlayerInput();
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        input.Enable();
        input.player.Move.performed += OnMovementPreformed;
        input.player.Move.canceled += OnMovementCancelled;
    }

    private void OnDisable()
    {
        input.Disable();
        input.player.Move.performed -= OnMovementPreformed;
        input.player.Move.canceled -= OnMovementCancelled;
    }

    private void FixedUpdate()
    {
        Vector3 moveDirection = new Vector3(moveVector.x, 0, moveVector.y);
        rb.velocity = moveDirection * moveSpeed;
        rb.freezeRotation = true;
    }

    private void OnMovementPreformed(InputAction.CallbackContext value)
    {
        moveVector = value.ReadValue<Vector2>();
    }

    private void OnMovementCancelled(InputAction.CallbackContext value)
    {
        moveVector = Vector2.zero;
    }

    private void OnCollisionEnter(Collision collision)
{
    GameObject collidedObject = collision.gameObject;

    // Check if the collided object has a Rigidbody to determine if it's a cube
    if (collidedObject.CompareTag("Cube"))
    {
        // Get the size of the controlled cube
        float controlledCubeSize = transform.localScale.x; // Assuming uniform scaling
        // Get the size of the collided cube
        float collidedCubeSize = collidedObject.transform.localScale.x; // Assuming uniform scaling

        if (collidedCubeSize < controlledCubeSize)
        {
            // Play sound effect
            audioSource.Play();
            // Small cube logic: Increase the size of the controlled cube by 0.10 and destroy the small cube
            transform.localScale += Vector3.one * 0.10f; // Increase the scale by 0.10
            Destroy(collidedObject); // Destroy the small cube
            ScoreManager.scoreCount += 1 ;
        }
        else
        {
            // Big cube logic: End the game
            EndGame(); // End the game
        }
    }
}

    private void EndGame()
    {
        // Handle game ending logic, e.g., showing game over screen, restarting level, etc.
        // Debug.Log("Game Over");
        // Application.Quit(); // Uncomment this line if you want to quit the application
        // UnityEditor.EditorApplication.isPlaying = false; // Uncomment this line if running in the editor
        // Show the Game Over panel
        gameOverPanel.SetActive(true);

        // Display the Game Over message
        gameOverText.text = "Game Over";

        // Start coroutine to return to main menu after a delay
        StartCoroutine(ReturnToMainMenuAfterDelay());
    }
    private IEnumerator ReturnToMainMenuAfterDelay()
    {
        // Wait for a few seconds
        yield return new WaitForSeconds(delayBeforeMainMenu);

        // Logic to return to the main menu (this is just an example)
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");
    }

    private void LoadMainMenu()
    {
        // Assuming the main menu scene is named "MainMenu"
        SceneManager.LoadScene("Main Menu");
    }
}