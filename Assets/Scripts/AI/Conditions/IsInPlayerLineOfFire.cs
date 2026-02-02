using TheKiwiCoder;
using UnityEngine;

public class IsInPlayerLineOfFire : ConditionNode
{
    [Tooltip("Angle tolerance to determine if the dog is in the line of fire")] public float tolerance = 15f;


    BehaviourTreeInstance m_behaviourTreeInstance;
    private BlackboardKey<GameObject> m_playerKey;

    private Vector3 m_playerAimingDirection;
    private FiringController m_playerFiringController;

    protected override bool CheckCondition()
    {
        Vector3 _playerPos = blackboard.GetValue<GameObject>("player").transform.position;
        Vector3 _shotDirection = blackboard.GetValue<GameObject>("player").GetComponent<FiringController>().TargetDirection;
        
        // Direction vector to dog
        Vector3 _directionToDog = context.transform.position - _playerPos;

        // We dont want to take into account the Y-Axis
        _shotDirection.y = 0f;
        _directionToDog.y = 0f;

        // If object is forward.
        if (Vector3.Dot(_shotDirection, _directionToDog) < 0) return false;

        return (Vector3.Angle(_shotDirection, _directionToDog) < tolerance);
    }
}
