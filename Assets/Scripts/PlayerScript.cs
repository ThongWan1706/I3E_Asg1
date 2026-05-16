using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //Global variables for the game
    public int coinscollected = 0;
    public int specialitemcollected = 0;
    int totalspecialitem = 2;
    int totalcoins = 10;
    int health = 10;
    Vector3 checkpointPosition; //Variable for checkpoints for lvl 2 and 3
    GameObject currentCollectible;

    //Sounds
    public AudioSource audioSource;
    public AudioClip CoinSound;

    //At the start "remember" the player location
    void Start()
    {
        checkpointPosition = transform.position;
    }

    //When the player uses the "F" key to collect the special item
    void OnInteract()
    {
        //Check if we actually have something to collect
        if (currentCollectible == null) return;

        //Double-check the tag to make sure it's the specialItem
        if (currentCollectible.CompareTag("SpecialItem"))
        {
            specialitemcollected++;
            Debug.Log("Special Item Collected: " + specialitemcollected);

            //Find the reference to destroy it, then clean up the variable state
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