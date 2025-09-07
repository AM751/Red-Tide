using System;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] public GameObject redBait;
    [SerializeField] public GameObject greenBait;
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
            Debug.Log("Player interacted with the Bait");
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player stopped the interaction with Bait");
        }
    }
}
