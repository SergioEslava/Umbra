using TheKiwiCoder;
using UnityEngine;

[RequireComponent (typeof(Health))]
[RequireComponent(typeof(BehaviourTreeInstance))]
public class OnDamageDetection : MonoBehaviour
{
    Health m_health;
    BehaviourTreeInstance m_behaviourTreeInstance;

    private void Awake()
    {
        m_health = GetComponent<Health>();
        m_behaviourTreeInstance = GetComponent<BehaviourTreeInstance>();
    }

    private void OnEnable()
    {
        m_health.OnDamage.AddListener(DetectPlayer);
    }

    private void OnDisable()
    {
        m_health.OnDamage.RemoveListener(DetectPlayer);
    }

    private void DetectPlayer(float _damage)
    {
        m_behaviourTreeInstance.SetBlackboardValue<GameObject>("player", Player.Instance.gameObject);
    }
}