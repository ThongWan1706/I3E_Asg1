using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class NormalDoor : MonoBehaviour
{
    Animator animator;
    private bool isOpen = false; 
    private bool playerIsNearby = false; // Tracks if the player is at the door

    //Audio Settings
    public AudioSource audioSource; 
    public AudioClip openDoorSound; 

    //UI Display settings
    public TextMeshProUGUI promptTextDisplay;
    public string unlockedPrompt = "Press 'F' to toggle Door";

    void Start()
    {
        animator = GetComponentInParent<Animator>();
        if (promptTextDisplay != null)
        {
            promptTextDisplay.transform.parent.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // Checks if the player is nearby and presses 'F' to toggle the door
        if (playerIsNearby && Keyboard.current != null && Keyboard.current.fKey.wasPressedThisFrame)
        {
            ToggleDoor();
        }
    }

    public void ToggleDoor()
    {
        isOpen = !isOpen; 
        
        // Play the sound effect
        if (audioSource != null && openDoorSound != null)
        {
            audioSource.PlayOneShot(openDoorSound);
        }

        // Run the animations
        if (isOpen)
        {
            animator.SetTrigger("OpenDoor");
            Debug.Log("Door opening animation fired.");
        }
        else
        {
            animator.SetTrigger("CloseDoor");
            Debug.Log("Door closing animation fired.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsNearby = true; 

            // Show the ui display that the door can be open
            if (promptTextDisplay != null)
            {
                promptTextDisplay.text = unlockedPrompt;
                promptTextDisplay.transform.parent.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsNearby = false;

            // Hide the text panel if the player leave the trigger zone
            if (promptTextDisplay != null)
            {
                promptTextDisplay.transform.parent.gameObject.SetActive(false);
            }
        }
    }
}