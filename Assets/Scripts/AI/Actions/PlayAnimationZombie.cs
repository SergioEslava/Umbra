using TheKiwiCoder;
using UnityEngine;

public class PlayAnimationZombie : ActionNode
{
    [Tooltip("Animation to be played")] public ZombieAnimation animation;
    public float crossfadeTime = 1f;

    protected override void OnStart()
    {
        context.animator.CrossFade(animation.ToAnimationName(), crossfadeTime);
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        return State.Success;
    }
}