using UnityEngine;

public class Door : MonoBehaviour
{
    Animator animator;
    
    private bool isOpen = false; 

    void Start()
    {
        animator = GetComponentInParent<Animator>();
    }

    public void ToggleDoor()
    {
        isOpen = !isOpen; 

        if (isOpen)
        {
            animator.SetTrigger("OpenDoor");
            Debug.Log("Door opened");
        }
        else
        {
            animator.SetTrigger("CloseDoor");
            Debug.Log("Door closed");
        }
    }
}