using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(FiringController))]
public class LaunchBallController : MonoBehaviour
{
    [SerializeField] private GameObject launchablePrefab;

    [Header("Settings")]
    [SerializeField] private float launchCooldown = 15f;
    [SerializeField] [Range(5.0f, 20.0f)] private float launchForce = 60.0f;

    private FiringController m_firingController;

    private InputAction m_launchAction;

    private bool m_launchInput = false;
    private float m_shootTimer = 0f;
    private GameObject m_launchedObject = null;


    private void Awake()
    {
        m_firingController = GetComponent<FiringController>();

        // Initial setting of cooldown
        m_shootTimer = launchCooldown;
    }

    private void Start()
    {
        m_launchAction = InputSystem.actions.FindAction("Launch");
    }

    private void Update()
    {
        // Reading input values
        m_launchInput = (m_firingController.IsAiming) ? m_launchAction.IsPressed() : false;
    }

    private void FixedUpdate()
    {
        // Timer for the launch fire rate
        m_shootTimer += Time.deltaTime;
        if (m_shootTimer < launchCooldown)
            return;

        if (m_launchInput) launch();
    }

    private void launch()
    {
        if(m_launchedObject == null)
        {
            m_launchedObject = GameObject.Instantiate(launchablePrefab, transform.position, Quaternion.identity);
        }

        // Always resetting the launchable position
        m_launchedObject.transform.position = transform.position;

        // Setting the force to the launchable Rigigbody
        Rigidbody _launchedRg = m_launchedObject.GetComponent<Rigidbody>();
        if(_launchedRg == null)
        {
            Debug.LogError("Launchable Object has no Rigidbody");
            return;
        }


        _launchedRg.linearVelocity = Vector3.zero;
        _launchedRg.angularVelocity = Vector3.zero;
        _launchedRg.AddForce(m_firingController.TargetDirection * launchForce, ForceMode.Impulse);
    }
}
