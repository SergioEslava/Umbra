using TheKiwiCoder;
using UnityEngine;

[RequireComponent (typeof(BehaviourTreeInstance))]
public class SpherePlayerDetection : MonoBehaviour
{
    [Header("Sphere Settings")]
    [Tooltip("The radius of the sphere.")]
    [SerializeField] private float sphereRadius = 5f;

    [Tooltip("Offset to the sphere to be drawn relative to the GameObject.")]
    [SerializeField] private Vector3 sphereOffset = Vector3.zero;

    [Header("Debug Options")]
    [Tooltip("Shows the sphere in the scene view for debugging.")]
    [SerializeField] private bool showDebugSphere = true;

    BehaviourTreeInstance m_behaviourTreeInstance;

    private void Awake()
    {
        m_behaviourTreeInstance = GetComponent<BehaviourTreeInstance>();
    }

    private void Update()
    {
        DetectCharacterControllers();
    }

    private void DetectCharacterControllers()
    {
        // Calculate the sphere's world position with rotation applied
        Vector3 rotatedOffset = transform.rotation * sphereOffset;
        Vector3 spherePosition = transform.position + rotatedOffset;

        // Find all colliders within the sphere
        Collider[] colliders = Physics.OverlapSphere(spherePosition, sphereRadius);

        foreach (Collider collider in colliders)
        {
            // Check if the object has a CharacterController component
            CharacterController characterController = collider.GetComponent<CharacterController>();

            if (characterController != null)
            {
                Debug.Log($"CharacterController detected on object: {collider.gameObject.name}");
                m_behaviourTreeInstance.SetBlackboardValue<GameObject>("player", collider.gameObject);
                return;
            }
        }
        m_behaviourTreeInstance.SetBlackboardValue<GameObject>("player", null);
    }

    private void OnDrawGizmos()
    {
        if (showDebugSphere)
        {
            Gizmos.color = Color.green;
            Vector3 rotatedOffset = transform.rotation * sphereOffset;
            Vector3 spherePosition = transform.position + rotatedOffset;
            Gizmos.DrawWireSphere(spherePosition, sphereRadius);
        }
    }
}
