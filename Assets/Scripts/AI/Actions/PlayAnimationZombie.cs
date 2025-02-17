using TheKiwiCoder;
using UnityEngine;

public class PlayAnimationZombie : ActionNode
{
    [Tooltip("Animation to be played")] public ZombieAnimation animation;

    protected override void OnStart()
    {
        context.animator.Play(animation.ToAnimationName());
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        return State.Success;
    }
}