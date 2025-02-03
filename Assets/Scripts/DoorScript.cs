using UnityEngine;

public class DoorScript : MonoBehaviour
{
    private Animator anim;
    private bool isOpen = false;
    public ChestController chestController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isOpen == true && Input.GetKeyDown(KeyCode.E) && chestController.KeyAmount > 0)
        {
            anim.SetBool("OpenDoor", true);
            isOpen = true;
        }
        else if(isOpen == true && Input.GetKeyDown(KeyCode.E) && chestController.KeyAmount == 0)
        {
            anim.SetBool("OpenDoor", false);
            isOpen = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isOpen = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isOpen = false;
            
        }
    }
}
