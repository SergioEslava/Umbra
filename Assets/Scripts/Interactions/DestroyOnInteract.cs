using UnityEngine;

public class DestroyOnInteract : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Destroy(gameObject);
    }
}
