using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] float healthPoint = 15f;

    [HideInInspector] public UnityEvent OnDie;
    [HideInInspector] public UnityEvent<float> OnDamage;
    [HideInInspector] public UnityEvent<float> OnRestore;
    [HideInInspector] public float HP_INITIAL = 15f;

    private Animator m_animator;

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
    }


    public void TakeDamage(float _damage)
    {
        healthPoint -= _damage;

        OnDamage?.Invoke(healthPoint);
        Debug.Log($"HEALTH: {gameObject.name} takes {_damage} damage. Remaining health points: {healthPoint}");
        if (m_animator != null) m_animator.SetTrigger("Damage");

        if (healthPoint < 0)
        {
            OnDie?.Invoke();
            Debug.Log($"HEALTH: {gameObject.name} dies.");
            if (m_animator != null) m_animator.SetTrigger("Die");
        }

    }

    public void RestoreHP(float _restorePoints)
    {
        healthPoint = Mathf.Clamp(healthPoint + _restorePoints, 0f, HP_INITIAL);

        OnRestore?.Invoke(_restorePoints);
        Debug.Log($"HEALTH: {gameObject.name} restore {_restorePoints} HP points. Remaining health points: {healthPoint}");
    } 


    public float HealthPoint { get => healthPoint; set => healthPoint = value; }
}
