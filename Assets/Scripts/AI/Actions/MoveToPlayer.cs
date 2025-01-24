using UnityEngine;
using TheKiwiCoder;
using Unity.VisualScripting;

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
        Vector3 _playerDirection = (m_player.transform.position - context.gameObject.transform.position).normalized;
        context.agent.SetDestination(m_player.transform.position - _playerDirection * reachingRange);

        float _distance = DistanceXZ(context.transform.position, context.agent.destination);

        if (_distance > persecutionRange)
        {
            blackboard.SetValue<GameObject>("player", null);
            return State.Failure;
        }
        else if (_distance <= context.agent.stoppingDistance+0.1f)
            return State.Success;
        else
            return State.Running;
    }

    public override void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(context.agent.destination, 0.2f);
    }

    public static float DistanceXZ(Vector3 pointA, Vector3 pointB)
    {
        float deltaX = pointB.x - pointA.x;
        float deltaZ = pointB.z - pointA.z;

        // Retorna la raíz cuadrada de la suma de los cuadrados de deltaX y deltaZ
        return Mathf.Sqrt(deltaX * deltaX + deltaZ * deltaZ);
    }
}
