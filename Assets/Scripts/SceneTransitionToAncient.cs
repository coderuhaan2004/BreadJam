using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionToAncient : MonoBehaviour
{
    public string sceneName;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Player has entered the Ancient scene");
            SceneManager.LoadScene(sceneName);
        }
    }
}
