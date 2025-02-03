using UnityEngine;
using UnityEngine.SceneManagement;

public class Machine : MonoBehaviour
{
    public GameObject EndingMessage;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the machine");
            EndingMessage.SetActive(true);
            SceneManager.LoadScene("EndingScene");
        }
    }
}
