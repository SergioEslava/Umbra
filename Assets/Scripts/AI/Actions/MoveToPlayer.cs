using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class MoveToPlayer : ActionNode
{
    [Tooltip("Distance for stop following the player")] public float persecutionRange = 5f;
    [Tooltip("Distance for reaching the player")] public float reachingRange = 1.1f;


    Player m_player;

    protected override void OnStart()
    {

        m_player = Player.Instance;
    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        context.agent.destination = m_player.transform.position;

        if (Vector3.Distance(context.transform.position, context.agent.destination) > persecutionRange)
            return State.Failure;
        else if (Vector3.Distance(context.transform.position, context.agent.destination) < reachingRange)
            return State.Success;
        else
            return State.Running;
    }
}
