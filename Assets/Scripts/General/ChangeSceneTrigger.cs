using UnityEngine;

public class ChangeSceneTrigger : MonoBehaviour
{
    [SerializeField][Tooltip("Scene to switch to when player passes")] string sceneName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            GameManager.Instance.ChangeScene(sceneName);
        }
    }
}
