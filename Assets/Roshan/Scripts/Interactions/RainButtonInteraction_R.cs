using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class RainButtonInteraction_R : MonoBehaviour
{
    [Header("Rain System Reference")]
    public GameObject rainSystem;

    [Header("Optional Prompt UI")]
    public GameObject promptTextObject;      // CHANGED from TextMeshProUGUI
    private TextMeshProUGUI promptText;

    private bool playerInRange = false;
    private PlayerInputActions_R inputActions;

    private void Awake()
    {
        if (rainSystem != null)
            rainSystem.SetActive(false);

        inputActions = new PlayerInputActions_R();
        inputActions.Player.Interact.performed += ctx => TryToggleRain();

        // Get the TMP component
        if (promptTextObject != null)
            promptText = promptTextObject.GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        inputActions.Enable();
        if (promptTextObject != null)
            promptTextObject.SetActive(false);
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (promptTextObject != null && promptText != null)
            {
                promptText.text = "Press Square to Toggle Rain";
                promptTextObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (promptTextObject != null)
                promptTextObject.SetActive(false);
        }
    }

    private void TryToggleRain()
    {
        if (!playerInRange) return;
        if (rainSystem == null) return;

        bool isActive = rainSystem.activeSelf;
        rainSystem.SetActive(!isActive);
    }
}