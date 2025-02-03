using UnityEngine;
using TMPro;

public class ChestController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private bool isAtChest = false;
    [SerializeField] private TextMeshProUGUI CodeText;
    string codeTextValue = "";
    public string safeCode;
    public GameObject CodePanel;
    public GameObject key;
    public int KeyAmount = 0;
    public GameObject keyText;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        CodeText.text = codeTextValue;
        if(codeTextValue == safeCode)
        {
            animator.SetBool("Open", true);
            CodePanel.SetActive(false);
            takeKey();
        }
        
        if(codeTextValue.Length >= 5)
        {
            codeTextValue = "";
        }

        // Keep cursor visible while code panel is active
        if (CodePanel.activeSelf)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        if(Input.GetKeyDown(KeyCode.E) && isAtChest)
        {
            CodePanel.SetActive(true);
            keyText.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isAtChest = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isAtChest = false;
            CodePanel.SetActive(false);
            animator.SetBool("Open", false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void AddDigit(string digit)
    {
        codeTextValue += digit;
    }

    public void takeKey() //this function will get called after the player opens the chest
    {
        Debug.Log("Key Taken");
        KeyAmount++;
        key.SetActive(false); //hide the key
    }
}
