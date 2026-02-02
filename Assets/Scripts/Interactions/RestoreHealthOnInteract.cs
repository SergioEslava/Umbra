using UnityEngine;

public class RestoreHealthOnInteract : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Player.Instance.Health.RestoreHP(Player.Instance.Health.HP_INITIAL);
    }
}
