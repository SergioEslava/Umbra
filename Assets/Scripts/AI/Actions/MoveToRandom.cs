using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
using UnityEngine.AI;

[System.Serializable]
public class MoveToRandom : ActionNode
{
    [Tooltip("Range of distance for the random position")] public float randomRange = 5f;


    protected override void OnStart() {

        context.agent.destination = GetRandomPointOnNavMesh(context.transform.position, randomRange);
    }

    protected override void OnStop() {

    }

    protected override State OnUpdate() {
        if (Vector3.Distance(context.transform.position, context.agent.destination) < 1.1f)
        {
            return State.Success;
        }
        else
        {
            return State.Running;
        }
    }

    public Vector3 GetRandomPointOnNavMesh(Vector3 _center, float _range)
    {
        Vector3 _randomPoint = _center + Random.insideUnitSphere * _range;
        _randomPoint.y = 0;
        NavMeshHit _hit;

        // Navmesh point projection
        if (NavMesh.SamplePosition(_randomPoint, out _hit, _range, NavMesh.AllAreas))
        {
            return _hit.position;
        }

        // if a valid position wasn't find, return to the center
        return _center;
    }
}
