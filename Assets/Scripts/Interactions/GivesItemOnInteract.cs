using UnityEngine;

public class GivesItemOnInteract : MonoBehaviour, IInteractable
{
    [Header("Item Settings")]
    [SerializeField] private ItemData item;
    [SerializeField] private int quantity = 1;

    [Space]
    [Header("Lifecycle settings")]
    [SerializeField] private bool destroyOnInteract = true;

    private void Awake()
    {
        if (item == null || quantity <= 0)
            Debug.LogError($"Interactable Object that gives item has a invalid setting: {this.gameObject.name}");
    }

    public void Interact()
    {
        Player.Instance.Inventory.AddItem(item, quantity);
        Debug.Log($"Gives {quantity} {item} to the player.");

        if (destroyOnInteract) Destroy(gameObject);
    }
}
