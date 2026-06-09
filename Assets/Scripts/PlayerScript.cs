/*
* Author: Lam Thong Wan
* Date: 9 Jun 2026
* Description: This code is specifically for the player interactions throughout the gameplay
*/

using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections; // Required for Coroutines
using UnityEngine.SceneManagement; // Add this to the top of your script

public class PlayerScript : MonoBehaviour
{
    //Global variables for the game
    public int coinscollected = 0;
    public int specialitemcollected = 0;
    int totalcoins = 30;
    int health = 10;
    Vector3 checkpointPosition; //Variable for checkpoints for lvl 2 and 3
    GameObject currentCollectible;
    private bool isRespawning = false; //To prevent double triggering for the hazard
    public GameObject gameOverPanel; // Drag your panel here in the Inspector

    //References for the UI display
    public TextMeshProUGUI healthTextDisplay;
    public TextMeshProUGUI coinsTextDisplay;
    public TextMeshProUGUI specialItemTextDisplay;
    public Image specialItemIconDisplay;
    public GameObject collectiblesPanelObject; // Reference to hold the background panel
    public Image blackoutPanel; //For the blackout while death

    //Sounds
    public AudioSource audioSource;
    public AudioClip CoinSound;

    // Cards
    public GameObject blueCardUI;
    public GameObject redCardUI;
    public GameObject securityCardUI;


    //At the start "remember" the player location
    void Start()
    {
        checkpointPosition = transform.position;
        UpdateUI();

        if (collectiblesPanelObject != null)
        {
            collectiblesPanelObject.SetActive(false);
        }

        // Hide UI cards at start
        if (blueCardUI != null) blueCardUI.SetActive(false);
        if (redCardUI != null) redCardUI.SetActive(false);
        if (securityCardUI != null) securityCardUI.SetActive(false);
    }

    void UpdateUI()
    {
        if (healthTextDisplay != null)
        {
            healthTextDisplay.text = "HP:" + health;
        }

        if (coinsTextDisplay != null)
        {
            coinsTextDisplay.text = "Coins: " + coinscollected + " / " + totalcoins;
        }

        if (specialItemTextDisplay != null)
        {
            specialItemTextDisplay.text = "Collectibles";
        }
    }

    //When the player uses the "F" key to collect the special item
    void OnInteract()
    {
        if (currentCollectible == null) return;

        // Handle Blue Card
        if (currentCollectible.CompareTag("BlueCard"))
        {
            HandleCardCollection("Blue", blueCardUI);
        }
        // Handle Red Card
        else if (currentCollectible.CompareTag("RedCard"))
        {
            HandleCardCollection("Red", redCardUI);
        }
        // Handle White Card
        else if (currentCollectible.CompareTag("WhiteCard"))
        {
            HandleCardCollection("White", securityCardUI);
        }

    }

    void HandleCardCollection(string cardName, GameObject cardUI)
    {
        specialitemcollected++;
        Debug.Log(cardName + " Card Collected!");

        if (collectiblesPanelObject != null && !collectiblesPanelObject.activeSelf)
            collectiblesPanelObject.SetActive(true);

        if (cardUI != null)
            cardUI.SetActive(true);

        UpdateUI();

        Destroy(currentCollectible);
        currentCollectible = null;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the object we hit has a Coins component
        if (collision.gameObject.CompareTag("Collectible"))
        {
            Coins coin = collision.gameObject.GetComponent<Coins>();

            if (coin != null)
            {
                // Add the score dynamically based on what the coin is worth
                coinscollected += coin.scoreValue;
                print("Coins: " + coinscollected + "/" + totalcoins);

                //Refresh the UI text
                UpdateUI();

                //Tell the coin to play its sound and destroy itself
                coin.Collect();
            }
        }
    }

    // When the player "touches" the item/trigger zones
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Touched: " + other.name);

        // If a player "touches" a card
        if (other.CompareTag("BlueCard") || other.CompareTag("RedCard") || other.CompareTag("WhiteCard"))
        {
            currentCollectible = other.gameObject;
            Debug.Log("Card detected: " + other.tag);
        }

        // If the player steps onto the GoalArea
        if (other.CompareTag("GoalArea") && coinscollected >= totalcoins)
        {
            print("Level Complete!");
            print("Total Coins Collected: " + coinscollected + "/" + totalcoins);
        }

        // If the player step onto water/lava and haven'tspawning
        if (other.CompareTag("Hazard") && !isRespawning)
        {
            Debug.Log("Hazard detected! Starting death sequence...");
            StartCoroutine(HandleDeath(isInstantDeath: false));
        }

        if (other.CompareTag("OneShot") && !isRespawning)
        {
            Debug.Log("Hazard detected! Starting death sequence...");
            StartCoroutine(HandleDeath(isInstantDeath: true));
        }

        if (other.CompareTag("Checkpoint"))
        {
            checkpointPosition = other.transform.position;
            Checkpoint cp = other.GetComponent<Checkpoint>();
            if (cp != null) cp.ActivateCheckpoint();
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Only clear the collectible if we are actually walking away from the one we targeted
        if (currentCollectible != null && other.gameObject == currentCollectible)
        {
            currentCollectible = null;
            Debug.Log("Walked away from special item.");
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1; // Unpause
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reloads current scene
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit requested");
    }


    //For the death scene blackout
    // Updated to accept a boolean parameter
    IEnumerator HandleDeath(bool isInstantDeath)
    {
        isRespawning = true; // Lock: prevent any other triggers while dying

        // Dynamic health reduction logic
        if (isInstantDeath)
        {
            health = 0; // Drop to instant 0 HP
            Debug.Log("Instant death triggered!");
        }
        else
        {
            health -= 2; // Subtract normal 2 HP
        }

        UpdateUI();
        Debug.Log("Health reduced to: " + health);

        if (blackoutPanel == null) yield break;

        float fadeDuration = 0.5f;
        float timer = 0f;

        // Fade into black
        Color startColor = new Color(0, 0, 0, 0); // Fully transparent
        Color endColor = new Color(0, 0, 0, 1);   // Fully opaque

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            blackoutPanel.color = Color.Lerp(startColor, endColor, timer / fadeDuration);
            yield return null;
        }

        // Stay out black for 3 seconds
        yield return new WaitForSeconds(3f);

        // Respawn or Game Over check
        if (health > 0)
        {
            CharacterController cc = GetComponent<CharacterController>();
            if (cc != null) cc.enabled = false;
            transform.position = checkpointPosition;
            if (cc != null) cc.enabled = true;
        }
        else
        {
            Time.timeScale = 0;
            if (gameOverPanel != null)
            {
                gameOverPanel.SetActive(true);
            }

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        // Fade out from black
        timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            blackoutPanel.color = Color.Lerp(endColor, startColor, timer / fadeDuration);
            yield return null;
        }

        isRespawning = false;
    }
}