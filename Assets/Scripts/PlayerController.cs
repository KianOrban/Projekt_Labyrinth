using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 moveInput;
    private CharacterController characterController;
    [SerializeField] private float speed = 12f;

    public Transform groundCheck;
    public LayerMask groundMask;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
    }

    private void PlayerMove()
    {
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        characterController.Move(move * speed * Time.deltaTime);

        RaycastHit hit;

        if (Physics.Raycast(groundCheck.position, transform.TransformDirection(Vector3.down), out hit, 0.4f, groundMask))
        {
            string floorType = hit.collider.gameObject.tag;

            switch (floorType)
            {
                default:
                    speed = 12f;
                    break;
                case "Slow":
                    speed = 3f;
                    break;
                case "Fast":
                    speed = 20f;
                    break;

            }

        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
}
