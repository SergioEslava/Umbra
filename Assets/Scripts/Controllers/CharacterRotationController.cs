using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(ShootController))]
public class CharacterRotationController : MonoBehaviour
{
    [Header("Rotation")]
    [SerializeField][Range(0f, 3f)] float rotationSpeed;
    [SerializeField][Range(0f, 10f)] float aimingRotationSpeed;

    private MovementController m_movementController;
    private ShootController m_shootController;

    Quaternion m_targetRotation = Quaternion.identity;
    Vector3 m_rotationDirection = Vector3.zero;
    CinemachineInputAxisController m_inputAxisController;

    private void Start()
    {
        m_movementController = GetComponent<MovementController>();
        m_shootController = GetComponent<ShootController>();
        m_inputAxisController = FindAnyObjectByType<CinemachineInputAxisController>();
    }


    private void FixedUpdate()
    {
        if (m_inputAxisController != null)
            m_inputAxisController.enabled = !m_shootController.IsAiming;

        // We aplly rotation just if the player is aiming or if is moving with a threshold of 0.1f
        if (m_shootController.IsAiming)
        {
            m_rotationDirection = m_shootController.TargetDirection;
        }
        else if (m_movementController.MoveDirection.magnitude > 0.1f)
        {
            m_rotationDirection = m_movementController.MoveDirection;
        }
        else
        {
            return;
        }

        m_targetRotation = Quaternion.LookRotation(m_rotationDirection);

        // We only want the player to rotate in the Y-Axis
        m_targetRotation = Quaternion.Euler(0, m_targetRotation.eulerAngles.y, 0);

        float _finalRotationSpeed = (m_shootController.IsAiming) ? aimingRotationSpeed : rotationSpeed;

        transform.rotation = Quaternion.Slerp(transform.rotation, m_targetRotation, _finalRotationSpeed * Time.deltaTime);
    }
}
