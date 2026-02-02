using UnityEngine;

[RequireComponent (typeof(Health))]
[RequireComponent (typeof(Animator))]
public class TriggerAnimationOnDie : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] string triggerName;
    
    private Health m_health;
    private Animator m_animator;

    private void Awake()
    {
        m_health = GetComponent<Health>();
        m_animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        m_health.OnDie.AddListener(TriggerAnimation);
    }

    private void OnDisable()
    {
        m_health.OnDie.RemoveListener(TriggerAnimation);
    }

    private void TriggerAnimation()
    {
        m_animator.SetTrigger(triggerName);
    }
}