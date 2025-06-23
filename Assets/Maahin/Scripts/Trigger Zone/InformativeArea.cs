using TMPro;
using UnityEngine;
public class InformativeArea : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private GameObject theTool;
    [SerializeField] private Canvas infoCanvas;

    private bool isInfoCame;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        theTool.SetActive(false);
        isInfoCame = false;
        infoCanvas.enabled = false;
    }

    public void InformationToPlayer()
    {
        isInfoCame = true;
        infoCanvas.enabled = true;
        infoText.text = "Press T to bring uo the Tool for fishing.";
    }
    private void OnTriggerEnter(Collider infoZone)
    { 
        if (infoZone.CompareTag ("Player") && Input.GetKeyDown(KeyCode.T))
        {
                theTool.SetActive(true);
                InformationToPlayer();
        }
    }

    // private void OnTriggerExit(Collider infoZone)
    // {
    //     if (infoZone.CompareTag ("Player"))
    //     {
    //         theTool.SetActive(false);
    //         infoText.text = "";
    //     }
    //     
    // }
}
