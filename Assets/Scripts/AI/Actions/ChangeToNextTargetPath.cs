using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class ChangeToNextTargetPath : ActionNode
{
    BehaviourTreeInstance m_behaviourTreeInstance;

    BlackboardKey<int> m_targetPathIndex;

    protected override void OnStart()
    {
        m_behaviourTreeInstance = context.GetComponent<BehaviourTreeInstance>();
        m_targetPathIndex = m_behaviourTreeInstance.FindBlackboardKey<int>("targetPathIndex");
    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        if(m_targetPathIndex == null)
        {
            Debug.LogWarning("targetPathIndex blackboard variable not found.");
            return State.Failure;
        }

        m_targetPathIndex.value += 1;
        return State.Success;
    }
}
