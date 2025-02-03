using UnityEngine;
using UnityEngine.SceneManagement;

public class GetEnergy : MonoBehaviour
{
    public string sceneName;
    public GameObject fire;
    public PlayerMovement playerMovement;
    public GameObject message;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            fire.SetActive(false);
            playerMovement.Energy += 1000;
            message.SetActive(true);
            SceneManager.LoadScene(sceneName);
        }
    }
}
