using UnityEngine;
using UnityEngine.Events;

public class EventOnInteract : MonoBehaviour, IInteractable
{
    [Space]
    [SerializeField] private UnityEvent OnInteract;

    [Space]
    [Header("Lifecycle settings")]
    [SerializeField] private bool destroyOnInteract = true;

    public void Interact()
    {
        OnInteract?.Invoke();

        if (destroyOnInteract) Destroy(gameObject);
    }
}
