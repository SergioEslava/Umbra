using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
using UnityEngine.AI;

[System.Serializable]
public class MoveToRandom : ActionNode
{
    protected override void OnStart() {

        context.agent.destination = GetRandomPointOnNavMesh(context.transform.position, 5f);
        Debug.Log("START");
    }

    protected override void OnStop() {
        Debug.Log("STOP");
    }

    protected override State OnUpdate() {
        if (Vector3.Distance(context.transform.position, context.agent.destination) < 1.1f)
        {
            Debug.Log("UPDATE: Success");
            return State.Success;
        }
        else
        {
            Debug.Log("UPDATE: Running");
            return State.Running;
        }
    }

    public Vector3 GetRandomPointOnNavMesh(Vector3 center, float range)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        randomPoint.y = 0;
        NavMeshHit hit;

        // Navmesh point projection
        if (NavMesh.SamplePosition(randomPoint, out hit, range, NavMesh.AllAreas))
        {
            return hit.position;
        }

        // if a valid position wasn't find, return to the center
        return center;
    }
}
