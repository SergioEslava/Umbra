using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] float healthPoint = 10f;

    [HideInInspector] public UnityEvent OnDie;
    [HideInInspector] public UnityEvent<float> OnDamage;

    public void TakeDamage(float _damage)
    {
        healthPoint -= _damage;

        OnDamage?.Invoke(healthPoint);
        Debug.Log($"HEALTH: {gameObject.name} takes {_damage} damage. Remaining health points: {healthPoint}");

        if (healthPoint < 0)
        {
            OnDie?.Invoke();
            Debug.Log($"HEALTH: {gameObject.name} dies.");
        }

    }
}
