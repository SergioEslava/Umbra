using UnityEngine;
using UnityEngine.Events;

public class RequiresItemToEventOnInteract : MonoBehaviour, IInteractable
{
    [SerializeField] private ItemData item;
    [SerializeField] private int quantity = 1;

    [Space]
    [SerializeField] private UnityEvent OnInteract;
    [SerializeField] private bool removesItemAfterInteract = false;

    [Space]
    [Header("Lifecycle settings")]
    [SerializeField] private bool destroyOnInteract = true;

    private void Awake()
    {
        if (item == null || quantity <= 0)
            Debug.LogError($"Interactable Object that require item has a invalid setting: {this.gameObject.name}");
    }

    public void Interact()
    {
        if (!Player.Instance.Inventory.HasItem(item))
        {
            Debug.Log($"{item.itemName} was required but the player dont have one in the inventory");
            return;
        }

        OnInteract?.Invoke();

        if (removesItemAfterInteract)
        {
            Player.Instance.Inventory.RemoveItem(item, quantity);
            Debug.Log($"Removes {quantity} {item} to the player.");
        }


        if (destroyOnInteract) Destroy(gameObject);
    }
}
