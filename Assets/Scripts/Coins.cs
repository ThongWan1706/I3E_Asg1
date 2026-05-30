using UnityEngine;

public class Coins : MonoBehaviour
{
    public float rotateSpeed = 100f;
    public int scoreValue = 1; // How many points this coin gives
    AudioSource collectibleAudio; //Assign the audio variable

    [SerializeField] 
    private AudioClip collectibleAudioClip;

void Start()
    {
        collectibleAudio = GetComponent<AudioSource>(); //Get the audio source in Unity
    }

    void Update()
    {
        // Smoothly rotate the coin
        transform.Rotate(0f, rotateSpeed * Time.deltaTime, 0f);
    }

    // This is called by the player script when they hit the coin
    public void Collect()
    {
        if (collectibleAudioClip != null)
        {
            // Plays the sound at the coin's position and automatically deletes itself when done
            AudioSource.PlayClipAtPoint(collectibleAudioClip, transform.position);
        }

        // Destroy the coin
        Destroy(gameObject);
    }
}
