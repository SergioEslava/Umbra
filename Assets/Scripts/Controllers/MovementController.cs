using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class MovementController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField][Range(0.01f, 0.2f)] float movementSpeed;
    [SerializeField][Range(0.01f, 0.2f)] float sprintSpeed;
    
    private CharacterController m_characterController;

    private InputAction m_moveAction;
    private InputAction m_sprintAction;

    private Camera m_camera;

    private Vector2 m_moveInput = Vector2.zero;
    private bool m_sprintInput = false;
    private Vector3 m_moveDirection = Vector3.zero;

    public Vector3 MoveDirection { get => m_moveDirection; }

    void Start()
    {
        m_moveAction = InputSystem.actions.FindAction("Move");
        m_sprintAction = InputSystem.actions.FindAction("Sprint");

        m_camera = Camera.main;

        m_characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Reading input values
        m_moveInput = m_moveAction.ReadValue<Vector2>();
        m_sprintInput = m_sprintAction.IsPressed();
    }

    private void FixedUpdate()
    {
        // Casting the input
        Vector3 _movement = new Vector3(m_moveInput.x, 0f, m_moveInput.y);

        // Transforming movement direction to align with the camera's orientation
        Vector3 _cameraForward = m_camera.transform.forward; // Forward direction of the camera
        Vector3 _cameraRight = m_camera.transform.right;     // Right direction of the camera

        // Remove any vertical component to keep movement on the horizontal plane
        _cameraForward.y = 0f;
        _cameraRight.y = 0f;

        // Normalize the directions
        _cameraForward.Normalize();
        _cameraRight.Normalize();

        // Transform the input direction relative to the camera
        m_moveDirection = _cameraForward * _movement.z + _cameraRight * _movement.x;

        float _movementSpeed = m_sprintInput ? sprintSpeed : movementSpeed;

        // Moving Character
        m_characterController.Move(m_moveDirection * _movementSpeed);  
    }
}
