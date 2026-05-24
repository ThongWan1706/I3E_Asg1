using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    //Global variables for the game
    public int coinscollected = 0;
    public int specialitemcollected = 0;
    int totalspecialitem = 3;
    int totalcoins = 10;
    int health = 10;
    Vector3 checkpointPosition; //Variable for checkpoints for lvl 2 and 3
    GameObject currentCollectible;

    //References for the UI display
    public TextMeshProUGUI healthTextDisplay;
    public TextMeshProUGUI coinsTextDisplay;
    public TextMeshProUGUI specialItemTextDisplay;
    public Image specialItemIconDisplay;
    public GameObject collectiblesPanelObject; // Reference to hold the background panel

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
            coinsTextDisplay.text = "Points:" + coinscollected;
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

        if (currentCollectible.CompareTag("SpecialItem"))
        {
            specialitemcollected++;
            Debug.Log("Special Item Collected: " + specialitemcollected);

            if (collectiblesPanelObject != null && !collectiblesPanelObject.activeSelf)
            {
                collectiblesPanelObject.SetActive(true);
            }

            SpecialItem itemData = currentCollectible.GetComponent<SpecialItem>();
            if (itemData != null)
            {
                // 1. Set the background display image sprite if needed
                if (specialItemIconDisplay != null)
                {
                    specialItemIconDisplay.sprite = itemData.itemIcon;
                }


                Debug.Log("Processing item type: " + itemData.cardType);

                if (itemData.cardType == "Blue" && blueCardUI != null)
                {
                    blueCardUI.SetActive(true);
                }
                else if (itemData.cardType == "Red" && redCardUI != null)
                {
                    redCardUI.SetActive(true);
                }
                else if (itemData.cardType == "Security" && securityCardUI != null)
                {
                    securityCardUI.SetActive(true);
                }
            }

            UpdateUI();

            GameObject itemToDestroy = currentCollectible;
            currentCollectible = null;
            Destroy(itemToDestroy);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Collectible"))
        {
            coinscollected++;
            print("Coins: " + coinscollected + "/" + totalcoins);

            UpdateUI();

            audioSource.PlayOneShot(CoinSound);
            Destroy(collision.gameObject);
        }
    }

    // When the player "touches" the item/trigger zones
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Touched: " + other.name);

        // If a player "touches" a special item
        if (other.CompareTag("SpecialItem"))
        {
            currentCollectible = other.gameObject;
            Debug.Log("Special item detected and targeted.");
        }

        // If the player steps onto the GoalArea
        if (other.CompareTag("GoalArea") && coinscollected >= totalcoins)
        {
            print("Level Complete!");
            print("Total Coins Collected: " + coinscollected + "/" + totalcoins);
        }

        // If the player step onto water/lava
        if (other.CompareTag("Hazard"))
        {
            health--;

            UpdateUI();

            // Check for Character Controller
            CharacterController cc = GetComponent<CharacterController>();
            if (cc != null)
            {
                cc.enabled = false; // Turn the freeze transform off from the rigidbody
                transform.position = checkpointPosition; // Move the player back to the checkpoint
                cc.enabled = true; // Turn it back on
            }
            else
            {
                transform.position = checkpointPosition;
            }

            print("Respawned!");
            print("Health: " + health);

            if (health <= 0)
            {
                print("Game Over!");
            }
        }

        // If the player steps onto a checkpoint
        if (other.CompareTag("Checkpoint"))
        {
            checkpointPosition = other.transform.position;
            print("Checkpoint Saved!");
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
}