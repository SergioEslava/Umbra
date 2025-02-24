using TheKiwiCoder;
using UnityEngine;

[RequireComponent (typeof(Health))]
[RequireComponent (typeof (AnimationController))]
[RequireComponent(typeof (BehaviourTreeInstance))]
public class Zombie : MonoBehaviour
{
    private Health m_health;
    private AnimationController m_animationController;
    private BehaviourTreeInstance m_behaviourTree;

    private void Start()
    {
        m_health = GetComponent<Health>();
        m_animationController = GetComponent<AnimationController>();
        m_behaviourTree = GetComponent<BehaviourTreeInstance>();

        // TODO: This probably must be controlled by settings variables in the behaviour tree
        m_health.OnDamage.AddListener(PlayGetHitAnimation);
        m_health.OnDie.AddListener(Die);
    }

    private void Die()
    {
        m_behaviourTree.enabled = false;
        PlayDieAnimation();
    }

    void PlayGetHitAnimation(float _damage)
    {
        m_animationController.PlayAnimationImmediate(ZombieAnimation.GetHit.ToAnimationName());
    }

    void PlayDieAnimation()
    {
        m_animationController.PlayAnimationImmediate(ZombieAnimation.GetHitCrit.ToAnimationName());
    }


}
