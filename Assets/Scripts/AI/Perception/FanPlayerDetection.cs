using TheKiwiCoder;
using UnityEngine;

[RequireComponent(typeof(BehaviourTreeInstance))]
public class FanPlayerDetection : MonoBehaviour
{
    [Header("Fan Settings")]
    [Tooltip("The angle of the fan in degrees.")]
    [SerializeField] private float fanAngle = 90f;

    [Tooltip("The range of the fan vision.")]
    [SerializeField] private float fanRange = 10f;

    [Header("Debug Options")]
    [Tooltip("Shows the fan area in the scene view for debugging.")]
    [SerializeField] private bool showDebugFan = true;

    private BehaviourTreeInstance behaviourTreeInstance;

    private void Awake()
    {
        behaviourTreeInstance = GetComponent<BehaviourTreeInstance>();
    }

    private void Update()
    {
        DetectCharacterControllers();
    }

    private void DetectCharacterControllers()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, fanRange);

        foreach (Collider collider in colliders)
        {
            Vector3 directionToTarget = (collider.transform.position - transform.position).normalized;
            float angleToTarget = Vector3.Angle(transform.forward, directionToTarget);

            if (angleToTarget <= fanAngle / 2)
            {
                CharacterController characterController = collider.GetComponent<CharacterController>();

                if (characterController != null)
                {
                    Debug.Log($"CharacterController detected on object: {collider.gameObject.name}");
                    behaviourTreeInstance.SetBlackboardValue<GameObject>("player", collider.gameObject);
                    return;
                }
            }
        }

        behaviourTreeInstance.SetBlackboardValue<GameObject>("player", null);
        Debug.Log("CharacterController not detected.");
    }

    private void OnDrawGizmos()
    {
        if (showDebugFan)
        {
            Gizmos.color = Color.blue;
            Vector3 leftBoundary = Quaternion.Euler(0, -fanAngle / 2, 0) * transform.forward;
            Vector3 rightBoundary = Quaternion.Euler(0, fanAngle / 2, 0) * transform.forward;

            Gizmos.DrawLine(transform.position, transform.position + leftBoundary * fanRange);
            Gizmos.DrawLine(transform.position, transform.position + rightBoundary * fanRange);
            Gizmos.DrawWireSphere(transform.position, fanRange);
        }
    }
}
