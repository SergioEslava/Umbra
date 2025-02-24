using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.ProBuilder.MeshOperations;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(AnimationController))]
public class MovementController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField][Range(0.01f, 0.2f)] float movementSpeed;
    [SerializeField][Range(0.01f, 0.2f)] float sprintSpeed;
    [Header("Gravity")]
    [SerializeField][Range(0f, 9.8f)] float gravitySpeed = 4f;
    
    private CharacterController m_characterController;
    private AnimationController m_animationController;

    private InputAction m_moveAction;
    private InputAction m_sprintAction;

    private Camera m_camera;

    private Vector2 m_moveInput = Vector2.zero;
    private bool m_sprintInput = false;
    private Vector3 m_moveDirection = Vector3.zero;
    private float m_verticalSpeed = 0f;

    public Vector3 MoveDirection { get => m_moveDirection; }

    void Start()
    {
        m_moveAction = InputSystem.actions.FindAction("Move");
        m_sprintAction = InputSystem.actions.FindAction("Sprint");

        m_camera = Camera.main;

        m_characterController = GetComponent<CharacterController>();
        m_animationController = GetComponent<AnimationController>();
    }

    void Update()
    {
        // Reading input values
        m_moveInput = m_moveAction.ReadValue<Vector2>();
        m_sprintInput = m_sprintAction.IsPressed();
    }

    private void FixedUpdate()
    {
        //Apply gravity acceleration if player is grounded
        m_verticalSpeed = (m_characterController.isGrounded) ? 0f : -gravitySpeed;

        // Casting the input
        Vector3 _movement = new Vector3(m_moveInput.x, m_verticalSpeed, m_moveInput.y);

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
        m_moveDirection.y = _movement.y;

        float _movementSpeed = m_sprintInput ? sprintSpeed : movementSpeed;

        // Setting the animations values
        if (Mathf.Abs(m_moveDirection.x) < 0.1f && Mathf.Abs(m_moveDirection.z) < 0.1f)
            m_animationController.PlayAnimationInLayer(PlayerAnimation.IdleMelee.ToAnimationName(), 0);
        else
            m_animationController.PlayAnimationInLayer(m_sprintInput ? PlayerAnimation.Run.ToAnimationName() : PlayerAnimation.Walk.ToAnimationName(), 0);

        // Moving Character
        m_characterController.Move(m_moveDirection * _movementSpeed);  
    }
}
