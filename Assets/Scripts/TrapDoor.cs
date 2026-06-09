/*
* Author: Lam Thong Wan (Modified for Trapdoor)
* Date: 9 Jun 2026
* Description: This code handles a trap door that auto-closes and pushes the player forward.
*/

using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using System.Collections;

public class TrapDoor : MonoBehaviour
{
    Animator animator;
    private bool isOpen = false; 
    private bool playerIsNearby = false; 
    private bool isTrapping = false; // Prevents double activation during sequence

    //Trap setting
    [SerializeField] 
    private float openDelayBeforeClose = 0.5f; // Time player stands there before door slams shut

    // Audio Settings
    public AudioSource audioSource; 
    public AudioClip openDoorSound; 

    // UI Display settings
    public TextMeshProUGUI promptTextDisplay;
    public string unlockedPrompt = "Press 'F' to Open Door";

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
        // Only allow opening if the player is nearby and the trap sequence hasn't started
        if (playerIsNearby && !isOpen && !isTrapping && Keyboard.current != null && Keyboard.current.fKey.wasPressedThisFrame)
        {
            StartCoroutine(TriggerTrapSequence());
        }
    }

    private IEnumerator TriggerTrapSequence()
    {
        isTrapping = true;
        isOpen = true;

        if (promptTextDisplay != null)
        {
            promptTextDisplay.transform.parent.gameObject.SetActive(false);
        }

        //Play Open Sound and Animation
        if (audioSource != null && openDoorSound != null)
        {
            audioSource.PlayOneShot(openDoorSound);
        }
        animator.SetTrigger("OpenDoor");
        Debug.Log("Trap door opening...");

        //Wait for the player to notice or walk slightly forward
        yield return new WaitForSeconds(openDelayBeforeClose);

        // Slam the door shut to push them in
        isOpen = false;
        
        animator.SetTrigger("CloseDoor");
        Debug.Log("Trap door slamming shut to push player!");

        // 5. Keep the door locked forever (or reset after a long delay if needed)
        // Leaving isTrapping = true makes this a one-time trap per scene load.
    }

    private void OnTriggerEnter(Collider other)
    {
        // Only show prompt if the trap hasn't been sprung yet
        if (other.CompareTag("Player") && !isTrapping)
        {
            playerIsNearby = true; 

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

            if (promptTextDisplay != null && !isTrapping)
            {
                promptTextDisplay.transform.parent.gameObject.SetActive(false);
            }
        }
    }
}
