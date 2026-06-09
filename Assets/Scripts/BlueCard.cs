/*
* Author: Lam Thong Wan
* Date: 9 Jun 2026
* Description: This code is specifically for the blue card where players will be collecting it at lvl 1 in order to open the door
*/

using UnityEngine;
using TMPro;

public class Bluecard : MonoBehaviour
{
    //Item Settings
    public string cardType = "Blue"; // "Blue", "Red", or "Security"
    public Sprite itemIcon;
    public float rotateSpeed = 100f;
    public int score = 1; 

    //UI Display
    [Header("Assign 'InteractiveText' Here")]
    public TextMeshProUGUI promptTextDisplay;
    public string pickupPrompt = "Press 'F' to pick up Blue Keycard";

    void Start()
    {
        // Safely turn off the parent panel at game start
        if (promptTextDisplay != null && promptTextDisplay.transform.parent != null)
        {
            promptTextDisplay.transform.parent.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        transform.Rotate(0f, rotateSpeed * Time.deltaTime, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (promptTextDisplay != null && promptTextDisplay.transform.parent != null)
            {
                promptTextDisplay.text = pickupPrompt;
                // Turn on the parent background image panel
                promptTextDisplay.transform.parent.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HidePromptPanel();
        }
    }

    private void OnDestroy()
    {
        // Clean up UI instantly if object gets collected/destroyed
        HidePromptPanel();
    }

    void HidePromptPanel()
    {
        if (promptTextDisplay != null && promptTextDisplay.transform.parent != null)
        {
            promptTextDisplay.transform.parent.gameObject.SetActive(false);
        }
    }
}