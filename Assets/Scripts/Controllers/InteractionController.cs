using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionController : MonoBehaviour
{
    [Header("Interaction Settings")]
    [Tooltip("The angle of the interaction fan in degrees.")]
    [SerializeField] private float interactionAngle = 90f;

    [Tooltip("The range of the interaction.")]
    [SerializeField] private float interactionRange = 5f;

    [Header("Debug Options")]
    [Tooltip("Shows the interaction area in the scene view for debugging.")]
    [SerializeField] private bool showDebugInteractionArea = true;

    private InputAction m_interactAction;
    private IInteractable m_interactable;

    private void Start()
    {
        m_interactAction = InputSystem.actions.FindAction("Interact");
    }

    private void Update()
    {
        DetectInteractableObjects();
        if (m_interactAction.WasPerformedThisFrame()) Interact();

        Debug.Log(m_interactable);
    }

    private void DetectInteractableObjects()
    {
        Collider[] _colliders = Physics.OverlapSphere(transform.position, interactionRange);

        foreach (Collider _collider in _colliders)
        {
            Vector3 _directionToTarget = (_collider.transform.position - transform.position).normalized;
            float _angleToTarget = Vector3.Angle(transform.forward, _directionToTarget);

            if (_angleToTarget <= interactionAngle / 2)
            {
                IInteractable _interactable = _collider.GetComponent<IInteractable>();
                if (_interactable != null)
                {
                    m_interactable = _interactable;
                    return;
                }
            }
        }

        m_interactable = null;
    }

    private void Interact()
    {
        if (m_interactable != null) m_interactable.Interact();
    }

    private void OnDrawGizmos()
    {
        if (showDebugInteractionArea)
        {
            Gizmos.color = Color.yellow;
            Vector3 _leftBoundary = Quaternion.Euler(0, -interactionAngle / 2, 0) * transform.forward;
            Vector3 _rightBoundary = Quaternion.Euler(0, interactionAngle / 2, 0) * transform.forward;

            Gizmos.DrawLine(transform.position, transform.position + _leftBoundary * interactionRange);
            Gizmos.DrawLine(transform.position, transform.position + _rightBoundary * interactionRange);
            Gizmos.DrawWireSphere(transform.position, interactionRange);
        }
    }
}

public interface IInteractable
{
    void Interact();
}
