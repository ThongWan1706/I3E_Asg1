using UnityEngine;

public class SpecialItem : MonoBehaviour
{
    [Header("Item Settings")]
    public string cardType; // "Blue", "Red", or "Security"
    public Sprite itemIcon;
    public float rotateSpeed = 100f;
    public int score = 1;

    void Update()
    {
        transform.Rotate(0f, rotateSpeed * Time.deltaTime, 0f);
    }
}
