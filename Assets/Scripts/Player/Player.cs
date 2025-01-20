using UnityEditor.Recorder.Input;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Inventory inventory;

    private static Player _instance;

    public static Player Instance
    {
        get
        {
            // No problem if we have an instance
            if (_instance != null) return _instance;

            // We can try to find it in the scene
            _instance = FindAnyObjectByType<Player>();
            if (_instance != null) return _instance;

            // If a Player instance doesn't exist in the scene we print an error.
            Debug.LogError("No Player instance found in the scene.");
            return null;
        }
    }

    public Inventory Inventory { get => inventory; set => inventory = value; }
}
