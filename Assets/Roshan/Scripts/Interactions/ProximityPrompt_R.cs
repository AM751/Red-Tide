using UnityEngine;
using TMPro;

public class ProximityPrompt_R : MonoBehaviour
{
    [Header("UI Reference")]
    public GameObject promptText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (promptText != null)
            {
                promptText.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (promptText != null)
            {
                promptText.SetActive(false);
            }
        }
    }
}