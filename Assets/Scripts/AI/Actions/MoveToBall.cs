using TheKiwiCoder;
using UnityEngine;

class MoveToBall : ActionNode
{

    private CompanionDogSM m_companionDogSM;

    protected override void OnStart()
    {
        m_companionDogSM = blackboard.Find<CompanionDogSM>("shared memory").value;
        context.agent.SetDestination(m_companionDogSM.BallObject.destination);
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        if (Vector3.Distance(context.transform.position, context.agent.destination) < 1.1f)
        {
            m_companionDogSM.BallObject.isLaunched = false;
            return State.Success;
        }
        else
        {
            return State.Running;
        }
    }
}
