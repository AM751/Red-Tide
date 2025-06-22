using UnityEngine;
public class PlayerRotation : MonoBehaviour
{
     [SerializeField] private float playerRotating = 250f;
     void Update() 
     {
        transform.Rotate (Vector3.down, playerRotating * Time.deltaTime); 
     }
}
