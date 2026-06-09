/*
* Author: Lam Thong Wan
* Date: 9 Jun 2026
* Description: This code is specifically for the lvl 1 door that contains the door indicator that shows whether the door is locked or not, it also includes in the open and close door animation
*/

using UnityEngine;

public class lvl1Door : MonoBehaviour
{
    Animator animator;
    private bool isOpen = false; 

    //Indicator Settings
    public MeshRenderer indicatorRenderer; 
    public Color lockedColor = Color.red;
    public Color unlockedColor = Color.green;

    //Audio Settungs
    public AudioSource audioSource; 
    public AudioClip openDoorSound; 

    void Start()
    {
        animator = GetComponentInParent<Animator>();
        SetIndicatorColor(lockedColor);
    }

    public void ToggleDoor()
    {
        isOpen = !isOpen; 
        
        // Turn indicator green permanently on the first toggle
        SetIndicatorColor(unlockedColor);

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

    private void SetIndicatorColor(Color targetColor)
    {
        if (indicatorRenderer != null)
        {
            indicatorRenderer.material.color = targetColor;
        }
    }
}