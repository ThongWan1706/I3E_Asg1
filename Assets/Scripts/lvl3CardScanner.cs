/*
* Author: Lam Thong Wan
* Date: 9 Jun 2026
* Description: This code is specifically for the lvl 3 door that requires the player to use the white card to open the door
*/

using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class lvl3CardScanner : MonoBehaviour
{
    public string requiredCardType = "White"; 
    public lvl3Door targetDoor;                  

    // UI Display settings
    public TextMeshProUGUI promptTextDisplay;
    public string lockedPrompt = "Press 'F' to scan white keycard";
    public string unlockedPrompt = "Press 'F' to toggle Door";

    private bool playerIsNearby = false;
    private bool isUnlocked = false; 
    private PlayerScript playerReference;

    void Start()
    {
        // Turn off the parent panel immediately on start so it's not stuck on screen
        if (promptTextDisplay != null)
        {
            promptTextDisplay.transform.parent.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (playerIsNearby && Keyboard.current != null && Keyboard.current.fKey.wasPressedThisFrame)
        {
            if (isUnlocked)
            {
                if (targetDoor != null) targetDoor.ToggleDoor();
            }
            else
            {
                TryManualUnlock();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerReference = other.GetComponent<PlayerScript>();
            playerIsNearby = true; 

            if (promptTextDisplay != null)
            {
                // Update text layout based on status
                promptTextDisplay.text = isUnlocked ? unlockedPrompt : lockedPrompt;
                promptTextDisplay.transform.parent.gameObject.SetActive(true);
            }
        }
    }

    void TryManualUnlock()
    {
        if (playerReference == null || targetDoor == null) return;

        // Verify if player has the white card item showing in inventory
        bool hasWhiteCard = playerReference.securityCardUI != null && playerReference.securityCardUI.activeSelf; 

        if (requiredCardType == "White" && hasWhiteCard)
        {
            isUnlocked = true; 
            targetDoor.ToggleDoor(); // Open up the door
            Debug.Log("Card detected! Access Granted.");

            if (promptTextDisplay != null)
            {
                promptTextDisplay.text = unlockedPrompt;
            }
        }
        else
        {
            Debug.Log("Access Denied: You need a " + requiredCardType + " keycard.");
            if (promptTextDisplay != null)
            {
                promptTextDisplay.text = "Access Denied: Requires White Card!";
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsNearby = false;
            playerReference = null;

            // Hide the entire panel UI box when walking away
            if (promptTextDisplay != null)
            {
                promptTextDisplay.transform.parent.gameObject.SetActive(false);
            }
        }
    }
}