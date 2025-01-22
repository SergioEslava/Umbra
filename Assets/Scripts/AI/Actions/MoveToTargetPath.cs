using UnityEngine;
using TheKiwiCoder;
using UnityEngine.AI;

[System.Serializable]
public class MoveToTargetPath : ActionNode
{
    BehaviourTreeInstance m_behaviourTreeInstance;

    BlackboardKey<int> m_targetPathIndex;
    BlackboardKey<PatrolPath> m_targetPath;

    protected override void OnStart()
    {
        m_behaviourTreeInstance = context.GetComponent<BehaviourTreeInstance>();
        m_targetPathIndex = m_behaviourTreeInstance.FindBlackboardKey<int>("targetPathIndex");
        m_targetPath = m_behaviourTreeInstance.FindBlackboardKey<PatrolPath>("path");
    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        if (m_targetPath == null)
        {
            Debug.LogWarning("targetPath blackboard variable not found.");
            return State.Failure;
        }

        if (m_targetPathIndex == null)
        {
            Debug.LogWarning("targetPathIndex blackboard variable not found.");
            return State.Failure;
        }

        context.agent.destination = m_targetPath.value.GetTargetAtIndex(m_targetPathIndex.value).position;

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
