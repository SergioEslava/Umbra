using TheKiwiCoder;
using UnityEngine;

public class MoveAside : ActionNode
{
    BehaviourTreeInstance m_behaviourTreeInstance;
    private BlackboardKey<GameObject> m_playerKey;

    private Vector3 m_playerAimingDirection;
    private FiringController m_playerFiringController;

    protected override void OnStart()
    {
        m_behaviourTreeInstance = context.GetComponent<BehaviourTreeInstance>();
        m_playerKey = m_behaviourTreeInstance.FindBlackboardKey<GameObject>("player");

        if (m_playerKey == null)
        {
            Debug.LogError("CompanionDog: Player not found.");
            return;
        }

        m_playerFiringController = m_playerKey.value.GetComponent<FiringController>();
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        if (m_playerKey == null)
        {
            return State.Failure;
        }

        m_playerAimingDirection = m_playerFiringController.TargetDirection;
        Vector3 _crossDirection = Vector3.Cross(m_playerAimingDirection, Vector3.forward).normalized;
        context.agent.SetDestination(_crossDirection * context.agent.speed);

        if (Vector3.Distance(context.transform.position, context.agent.destination) < 1.1f)
        {
            return State.Success;
        }
        else
        {
            return State.Running;
        }
    }
}
