using UnityEngine;

public class DestroyAtTime : MonoBehaviour
{
    [SerializeField] float timeOfLife = 5.0f;

    private void Start()
    {
        Destroy(gameObject, timeOfLife);
    }
}