using UnityEngine;

public class RemovesItemOnInteract : MonoBehaviour, IInteractable
{
    [SerializeField] private ItemData item;
    [SerializeField] private int quantity = 1;

    [Space]
    [Header("Lifecycle settings")]
    [SerializeField] private bool destroyOnInteract = true;

    private void Awake()
    {
        if (item == null || quantity <= 0)
            Debug.LogError($"Interactable Object that remove item has a invalid setting: {this.gameObject.name}");
    }

    public void Interact()
    {
        Player.Instance.Inventory.RemoveItem(item, quantity);
        Debug.Log($"Removes {quantity} {item} to the player.");

        if (destroyOnInteract) Destroy(gameObject);

    }
}
