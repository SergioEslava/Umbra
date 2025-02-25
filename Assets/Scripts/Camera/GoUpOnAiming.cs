using Unity.Cinemachine;
using UnityEngine;

[RequireComponent (typeof(CinemachineThirdPersonFollow))]
public class GoUpOnAiming : MonoBehaviour
{

    [Tooltip("Arm Lenght destination when aiming")][SerializeField] private float armLenghtOnAiming = 9f;
    [SerializeField] private float cameraMovementSpeed = 4f;

    FiringController m_firingController;
    CinemachineThirdPersonFollow m_cinemachineThirdPerson;
    float m_initialArmLenght = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_firingController = GameObject.FindFirstObjectByType<FiringController>();
        m_cinemachineThirdPerson = GetComponent<CinemachineThirdPersonFollow>();

        m_initialArmLenght = m_cinemachineThirdPerson.VerticalArmLength;
    }

    // Update is called once per frame
    void Update()
    {
        float targetArmLength = m_firingController.IsAiming ? armLenghtOnAiming : m_initialArmLenght;

        if (m_cinemachineThirdPerson.VerticalArmLength != targetArmLength)
        {
            float direction = m_firingController.IsAiming ? 1f : -1f;
            m_cinemachineThirdPerson.VerticalArmLength += direction * cameraMovementSpeed * Time.deltaTime;

            // Checks that is inside boundaries
            m_cinemachineThirdPerson.VerticalArmLength = Mathf.Clamp(m_cinemachineThirdPerson.VerticalArmLength,
                                                                      Mathf.Min(m_initialArmLenght, armLenghtOnAiming),
                                                                      Mathf.Max(m_initialArmLenght, armLenghtOnAiming));
        }


    }
}
