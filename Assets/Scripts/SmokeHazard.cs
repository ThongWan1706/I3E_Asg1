/*
* Author: Lam Thong Wan
* Date: 9 Jun 2026
* Description: This code is for the smoke where it deals gradual damage to the player while they stand in the area.
*/

using UnityEngine;
using System.Collections;

public class SmokeHazard : MonoBehaviour
{
    //Damage settings for the smoke
    [SerializeField] private int damageAmount = 1;
    [SerializeField] private float damageInterval = 2.0f; // Time in seconds between ticks

    private Coroutine damageCoroutine;

    //When the player enter in the "trigger zone" of the smoke
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (damageCoroutine == null)
            {
                damageCoroutine = StartCoroutine(DealDamageOverTime(other.gameObject));
            }
        }
    }

    //When the player left the area
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
        }
    }

    //When the player continues to stay in the area, it will start to gradually lose their hp
    private IEnumerator DealDamageOverTime(GameObject player)
    {
        PlayerScript playerHealth = player.GetComponent<PlayerScript>();

        while (playerHealth != null)
        {
            // Call the function in the PlayerScript to start minusing hp
            playerHealth.TakeDamage(damageAmount); 

            // Wait for 2 seconds before looping to deal damage again
            yield return new WaitForSeconds(damageInterval);
        }
    }
}