using UnityEngine;

public class lvl1Door : MonoBehaviour
{
    public PlayerScript player;
    public int coinsNeeded = 3;

    bool opened = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (opened) return;

        if (player.coinscollected >= coinsNeeded)
        {
            OpenDoor();
        }
        else
        {
            int coinleft = coinsNeeded - player.coinscollected;
            Debug.Log("You still need to collect " + coinleft + " more coins!");
        }
    }

    void OpenDoor()
    {
        opened = true;
        Debug.Log("Door Opened!");
    }

    private void Update()
    {
        if (opened)
        {
            transform.position += Vector3.up * Time.deltaTime * 2f;
        }
    }
}