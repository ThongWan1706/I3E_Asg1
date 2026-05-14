using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //Global variables for the game
    int coinscollected = 0;
    int totalcoins = 10;
    int health = 10;
    Vector3 checkpointPosition; //Variable for checkpoints for lvl 2 and 3
    GameObject currentCollectible;

    //At the start "remember" the player location
    void Start()
    {
        checkpointPosition = transform.position;
    }

    //When the player uses the "F" key to collect things
    void OnInteract() { 
        if (currentCollectible != null) { 
            //Refer to the Collectibles script where is the score for each collectible in the group 
            Collectibles collectible = currentCollectible.GetComponent<Collectibles>(); 
            coinscollected++; 
            print("Coins Collected: " + coinscollected + "/ " + totalcoins); 
            Destroy(currentCollectible); 
        } 
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectible"))
        {
            currentCollectible = other.gameObject;
        }

        if (other.CompareTag("GoalArea") && coinscollected >= totalcoins)
        {
            print("Level Complete!");
            print("Total Coins Collected: " + coinscollected + "/" + totalcoins);
        }

        if (other.CompareTag("Hazard"))
        {
            health--;

            transform.position = checkpointPosition;

            print("Respawned!");
            print("Health: " + health);

            if (health <= 0)
            {
                print("Game Over!");
            }
        }

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