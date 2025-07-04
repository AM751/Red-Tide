using System;
using TMPro;
using UnityEngine;
public class InformativeArea : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private GameObject theTool;
    [SerializeField] private Canvas infoCanvas;

    private bool isInfoCame;

    private bool isPlayerInside;

    private bool hasToolCame; //To check whether the tool appeared or not on the screen.
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        theTool.SetActive(false);
        isInfoCame = false;
        isPlayerInside = false;
        hasToolCame = false;
        infoCanvas.enabled = false;
    }

    public void InformationToPlayer()
    
    {
        isInfoCame = true;
        infoCanvas.enabled = true;
        infoText.text = "Press T to bring uo the Tool for fishing.";
    }

    void Update()
    {
        if (isPlayerInside == true && hasToolCame == false && Input.GetKeyDown(KeyCode.T))
        {
            hasToolCame = true;
            theTool.SetActive(true);
            infoCanvas.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider infoZone)
    { 
        if (infoZone.CompareTag ("Player") && hasToolCame == false)
        {
            isPlayerInside = true;
            InformationToPlayer();
        }
    }

    private void OnTriggerExit(Collider infoZone)
    {
        if (infoZone.CompareTag ("Player"))
        {
           isPlayerInside = false;
           infoCanvas.enabled = false;
        }
        
    }
    
   
}
