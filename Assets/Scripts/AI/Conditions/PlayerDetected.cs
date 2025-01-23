using System;
using TheKiwiCoder;
using UnityEngine;


public class PlayerDetected : ConditionNode
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
        if (m_playerKey == null)
        {
            Debug.LogError("player key not found in blackboard.");
            return false;
        }

        return m_playerKey.value != null;
    }
}