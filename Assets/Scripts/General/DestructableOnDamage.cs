using UnityEngine;

public class DestructableOnDamage : MonoBehaviour, IDamageable
{
    [Header("Settings")]
    [SerializeField] private float health;

    public void TakeDamage(float _damage)
    {
        health -= _damage;
        if (health < 0) Destroy(this.gameObject);
        Debug.Log($"{gameObject.name} destroyed.");
    }
}
