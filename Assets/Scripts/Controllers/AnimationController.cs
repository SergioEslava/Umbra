using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField]  private Animator animator;

    // Method to play an animation with crossfade
    public void PlayAnimation(string animationState, float transitionDuration)
    {
        animator.CrossFade(animationState, transitionDuration);
    }

    public void PlayAnimationInLayer(string animationState, int layer) 
    {
        animator.Play(animationState, layer);
    }

    //Method to force an immediate animation play without blending
    public void PlayAnimationImmediate(string animationState)
    {
        animator.Play(animationState);
    }
}
