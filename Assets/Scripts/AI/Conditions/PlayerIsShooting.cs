using TheKiwiCoder;
using UnityEngine;

public class PlayerIsShooting : ConditionNode
{
    BehaviourTreeInstance m_behaviourTreeInstance;
    private BlackboardKey<GameObject> m_playerKey;

    protected override void OnStart()
    {
        m_behaviourTreeInstance = context.GetComponent<BehaviourTreeInstance>();
        m_playerKey = m_behaviourTreeInstance.FindBlackboardKey<GameObject>("player");
    }

    protected override bool CheckCondition()
    {
        if(m_playerKey == null)
        {
            Debug.LogError("CompanionDog: Player not found.");
            return false;
        }

        return m_playerKey.value.GetComponent<FiringController>().IsAiming;
    }
}
