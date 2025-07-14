using UnityEngine;

public class IndicatorAppear : MonoBehaviour
{
    [SerializeField] private GameObject indicatorPrefab;
    
    private GameObject _anotherIndicator;
    
    private InputSystem_Actions _playerControl;

    private void Awake()
    {
        _playerControl = new InputSystem_Actions();

        _playerControl.Gameplay.Interaction.performed += ctx => objectAppear();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnEnable()
    {
        _playerControl.Gameplay.Enable();
    }

    private void OnDisable()
    {
        _playerControl.Gameplay.Disable();
    }

    private void objectAppear()
    {
        if (_anotherIndicator == null) return;
        
        _anotherIndicator = Instantiate(indicatorPrefab, transform.position, Quaternion.identity);
    }
     

    // Update is called once per frame
    // void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Z))
    //     {
    //         
    //         if (_anotherIndicator) return;
    //         
    //         
    //         Debug.Log ("Something Appears");
    //         _anotherIndicator = Instantiate(indicatorPrefab, transform.position, Quaternion.identity);
    //         Debug.Log(_anotherIndicator);
    //     }
    // }
}
