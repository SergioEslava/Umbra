using TheKiwiCoder;
using Unity.VisualScripting;
using UnityEngine;

public class Attack : ActionNode
{
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask collisionMask;
    [SerializeField] private float attackDamage;

    protected override void OnStart()
    {
        
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        Collider[] _objectsInSphere = Physics.OverlapSphere(context.gameObject.transform.position, attackRange, collisionMask);
        foreach (Collider _col in _objectsInSphere)
        {
            // Verify if object can be damageable
            IDamageable _damageable = _col.GetComponent<IDamageable>();
            if (_damageable != null && _col.gameObject != context.gameObject)
                _damageable.TakeDamage(attackDamage); // Apply damage

        }

        return State.Success;
    }

    public override void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(context.gameObject.transform.position, attackRange);
    }
}