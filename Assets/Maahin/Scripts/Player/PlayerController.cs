  using UnityEngine;
 

  public class PlayerController : MonoBehaviour
  {
      public float movementSpeed;
      public float playerJump;
      private Rigidbody playerRB;
      private bool isPlayerGrounded;

      private InputSystem_Actions playerControls;
      private Vector2 moveInput;
      
      void Awake()
      {
          playerControls = new InputSystem_Actions();
          
          playerControls.Gameplay.JoystickMove.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
      }

      void OnEnable()
      {
          playerControls.Gameplay.Enable();
      }

      void OnDisable()
      {
          playerControls.Gameplay.Disable();
      }
      void Start () 
      {
          playerRB = GetComponent<Rigidbody> ();
          isPlayerGrounded = true;
      }
      
      void Update () 
      {
            Vector3 movement = new Vector3(moveInput.x, 0, moveInput.y);
            playerRB.MovePosition(transform.position + movement * movementSpeed * Time.deltaTime);
          //For Straight, Back, Left and Right movements:
          // if(Input.GetKey(KeyCode.W)) 
          // {
          //     playerRB.position += Vector3.forward * Time.deltaTime * movementSpeed;
          // }
          // else if(Input.GetKey(KeyCode.S))
          // {
          //     playerRB.position += Vector3.back * Time.deltaTime * movementSpeed;
          // }
          // else if(Input.GetKey(KeyCode.A)) 
          // {
          //     playerRB.position += Vector3.left * Time.deltaTime * movementSpeed;
          // }
          // else if(Input.GetKey(KeyCode.D)) 
          // {
          //     playerRB.position += Vector3.right * Time.deltaTime * movementSpeed;
          // } 
          // if (Input.GetKey(KeyCode.Space) && isPlayerGrounded)
          // {
          //     playerRB.AddForce(Vector3.up * playerJump, ForceMode.Impulse);
          //     isPlayerGrounded = false;
          // }
      }

      private void OnCollisionEnter(Collision collision)
      {
          if (collision.gameObject.CompareTag("Ground"))
          {
              isPlayerGrounded = true;
          }
      }
  }
