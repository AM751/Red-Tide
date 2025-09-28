using System;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] public GameObject redBait;
    [SerializeField] public GameObject greenBait;

    [SerializeField] public GameObject fishingRod;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        
        if (other.gameObject.CompareTag("Player"))
        {
            if (fishingRod != null || redBait != null || greenBait != null)
            {
                Debug.Log("You need a FishingRod!!!");
            }
        }
    }
    

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (fishingRod == null || redBait == null || greenBait == null)
            {
                Debug.Log("Player collected the Bait");   
            }
            
        }
    }
}
