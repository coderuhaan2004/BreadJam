using UnityEngine;

public class DoorTriggerButton : MonoBehaviour
{
    [SerializeField] private DoorSetActive door;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            door.OpenDoor();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            door.CloseDoor();
        }
    }
}
