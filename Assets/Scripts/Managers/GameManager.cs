using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // TODO: Change to a more stable reference system
    [Header("UI")]
    [SerializeField] private Animator transitionAnimator;
    [SerializeField] private float transitionTime = 3f;

    public void ChangeScene(string scene)
    {
        // Comprueba si la escena existe antes de intentar cambiar a ella
        if (Application.CanStreamedLevelBeLoaded(scene))
        {
            StartCoroutine(LoadScene(scene));
        }
        else
        {
            Debug.LogError("The scene" + scene + " doesn`t exits or weren't found in the scene build list");
        }
    }

    IEnumerator LoadScene(string scene)
    {
        transitionAnimator.SetTrigger("Fade");
        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(scene);
    }

    internal void ResetScene()
    {
        ChangeScene(SceneManager.GetActiveScene().name);
    }

    #region Singleton Thinks
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                // Busca si hay alguna instancia en la escena actual
                instance = FindFirstObjectByType<GameManager>();

                // Si no existe ninguna instancia, crea una nueva
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject("GameManager");
                    instance = singletonObject.AddComponent<GameManager>();
                }
            }
            return instance;
        }
        private set { instance = value; }
    }
    #endregion
}
