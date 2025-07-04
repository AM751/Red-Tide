using UnityEngine;

public class IndicatorAppear : MonoBehaviour
{
    [SerializeField] private GameObject indicatorPrefab;
    
    private GameObject _anotherIndicator;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
     

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            
            if (_anotherIndicator) return;
            
            
            Debug.Log ("Something Appears");
            _anotherIndicator = Instantiate(indicatorPrefab, transform.position, Quaternion.identity);
            Debug.Log(_anotherIndicator);
        }
    }
}
