using TheKiwiCoder;

public class StopMovement : ActionNode
{
    protected override void OnStart()
    {
        
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        context.agent.destination = context.gameObject.transform.position;
        return State.Success;
    }
}