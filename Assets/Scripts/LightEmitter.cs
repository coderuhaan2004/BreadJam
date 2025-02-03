using UnityEngine;
using UnityEngine.SceneManagement;

public class LightEmitter : MonoBehaviour
{
    public string sceneName;
    public int theta = 0;
    public GameObject PlayerCamera;
    public GameObject Camera2;
    public PlayerMovement playerMovement;
    public GameObject message;
    public GameObject EnergyMessage;

    private bool hasIncreasedEnergy = false; // Flag to ensure energy increases only once

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            addAngle(15);
            Debug.Log("Theta: " + theta);
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            subtractAngle(15);
            Debug.Log("Theta: " + theta);
        }

        // Check if theta is -90 and energy hasn't been increased yet
        if (theta == -90 && !hasIncreasedEnergy)
        {
            increaseEnergy();
            hasIncreasedEnergy = true; // Set the flag to prevent further increases
        }

        // Optional: Reset the flag if theta changes back from -90
        if (theta != -90)
        {
            hasIncreasedEnergy = false;
        }
    }

    public void increaseEnergy()
    {
        playerMovement.Energy += 10000;
        Debug.Log("Energy: " + playerMovement.Energy);
        EnergyMessage.SetActive(true);
        SceneManager.LoadScene(sceneName);
    }

    public void addAngle(int angle)
    {
        theta -= angle;
        transform.rotation = Quaternion.Euler(theta, 0, 0);
    }

    public void subtractAngle(int angle)
    {
        theta += angle;
        transform.rotation = Quaternion.Euler(theta, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the light emitter");
            PlayerCamera.SetActive(false);
            Camera2.SetActive(true);
            message.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited the light emitter");
            PlayerCamera.SetActive(true);
            Camera2.SetActive(false);
            message.SetActive(false);
        }
    }
}
