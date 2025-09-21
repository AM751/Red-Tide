using Unity.VisualScripting;
using UnityEngine;

public class FishingRod : MonoBehaviour
{
    //For the Rod collection by the player:
    void OnTriggerEnter(Collider fishRod)
    {
        if (fishRod.CompareTag("Player")); 
        {
            Destroy(gameObject);
            Debug.Log("Rod Collected"); 
        }
    }
}
