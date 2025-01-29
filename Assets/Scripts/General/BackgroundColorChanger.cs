using System;
using System.Collections;
using UnityEngine;

public class BackgroundColorChanger : MonoBehaviour
{
    [SerializeField] Color bgDieColor = Color.black;
    [SerializeField] Color bgHalfHPColor = Color.red;

    private Coroutine interpolationCoroutine;

    private Health m_playerHealth;
    private Color m_initialCameraColor;


    private void Start()
    {
        m_initialCameraColor = Camera.main.backgroundColor;
        m_playerHealth = Player.Instance.Health;

        m_playerHealth.OnDamage.AddListener(ReactToHealthChange);
        m_playerHealth.OnRestore.AddListener(ReactToHealthChange);
    }

    private void ReactToHealthChange(float _damage)
    {
        if (m_playerHealth.HealthPoint <= 0f) {
            StartInterpolation(Camera.main.backgroundColor, bgDieColor, 1f);
        }else if (m_playerHealth.HealthPoint <= m_playerHealth.HP_INITIAL / 2) {
            StartInterpolation(Camera.main.backgroundColor, bgHalfHPColor, 1f);
        }
        else
        {
            StartInterpolation(Camera.main.backgroundColor, m_initialCameraColor, 1f);
        }
    }

    // Método público para iniciar la interpolación
    public void StartInterpolation(Color startColor, Color endColor, float duration)
    {
        if (interpolationCoroutine != null)
        {
            StopCoroutine(interpolationCoroutine);
        }

        interpolationCoroutine = StartCoroutine(InterpolateColor(startColor, endColor, duration));
    }

    private IEnumerator InterpolateColor(Color startColor, Color endColor, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;

            Color currentColor = Color.Lerp(startColor, endColor, t);

            Camera.main.backgroundColor = currentColor;

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        Debug.Log("Final Color: " + endColor);


        interpolationCoroutine = null;
    }

    public void CancelInterpolation()
    {
        if (interpolationCoroutine != null)
        {
            StopCoroutine(interpolationCoroutine);
            interpolationCoroutine = null;
            Debug.Log("Interpolation canceled.");
        }
    }
}
