using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //Global variables for the game
    public int coinscollected = 0;
    int specialitemcollected = 0;
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
    void OnInteract() { 
        if (currentCollectible != null) { 
            //Refer to the Collectibles script where is the score for each collectible in the group 
            SpecialItem collectible = currentCollectible.GetComponent<SpecialItem>(); 
            specialitemcollected++;
            print("Special Item Collected: " + specialitemcollected + "/ " + totalspecialitem); 
            Destroy(currentCollectible); 
        } 
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Collectible")
        {
            currentCollectible = collision.gameObject;
            coinscollected++;
            print("Coins: " + coinscollected + "/" + totalcoins);

            audioSource.PlayOneShot(CoinSound);

            Destroy(collision.gameObject);
        }
    }

     void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject == currentCollectible)
        {
            currentCollectible = null;
        }
    }

    //When the player "touches" the item/trigger zones
    void OnTriggerEnter(Collider other)
    {

        Debug.Log("Touched: " + other.name);
        
        //If the player collide with a coin (collectible)
        if (other.CompareTag("Collectible"))
        {
            coinscollected++;
            print("Coins: " + coinscollected + "/" + totalcoins);
            Destroy(other.gameObject);
        }
        
        //If the player collides on a special item (SpecialItem)
        if (other.CompareTag("SpecialItem"))
        {
            currentCollectible = other.gameObject;
        }

        //If the player steps onto the GoalArea
        if (other.CompareTag("GoalArea") && coinscollected >= totalcoins)
        {
            print("Level Complete!");
            print("Total Coins Collected: " + coinscollected + "/" + totalcoins);
        }

        //If the player step onto water/lava
        if (other.CompareTag("Hazard"))
        {
            health--;

        // Check for Character Controller
        CharacterController cc = GetComponent<CharacterController>();
        if (cc != null)
            {
                cc.enabled = false; // Turn it off
                transform.position = checkpointPosition; // Move the player
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

        //If the player steps onto a checkpoint
        if (other.CompareTag("Checkpoint"))
        {
            checkpointPosition = other.transform.position;
            print("Checkpoint Saved!");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == currentCollectible)
        {
            currentCollectible = null;
        }
    }
}