/*
* Author: Lam Thong Wan
* Date: 9 Jun 2026
* Description: This code is specifically for the checkpoint where whenever the player die due to a hazard, they will respawn at that location
*/

using UnityEngine;
using TMPro;

public class Checkpoint : MonoBehaviour
{
    public Color activeColor = Color.green;

    //UI Display
    public TextMeshProUGUI promptTextDisplay;
    public string saveMessage = "Checkpoint Saved!";

    private MeshRenderer meshRenderer;
    private bool isActivated = false;

    void Start()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
    
        if (meshRenderer == null) {
            Debug.LogError("Checkpoint Error: Could not find MeshRenderer on " + gameObject.name);
        }
        else
        {
            // Set initial color to red
            meshRenderer.material.color = Color.red;
        }

        // Hide at start
        if (promptTextDisplay != null && promptTextDisplay.transform.parent != null)
        {
            promptTextDisplay.transform.parent.gameObject.SetActive(false);
        }
    }

    public void ActivateCheckpoint()
    {
        if (isActivated) return;
        isActivated = true;

        if (meshRenderer != null)
        {
            meshRenderer.material.color = activeColor;
        }

        if (promptTextDisplay != null && promptTextDisplay.transform.parent != null)
        {
            promptTextDisplay.text = saveMessage;
            promptTextDisplay.transform.parent.gameObject.SetActive(true);
            
            CancelInvoke("HideCheckpointUI");
            Invoke("HideCheckpointUI", 2f);
        }
    }

    void HideCheckpointUI()
    {
        if (promptTextDisplay != null && promptTextDisplay.transform.parent != null)
        {
            promptTextDisplay.transform.parent.gameObject.SetActive(false);
        }
    }
}