using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[RequireComponent(typeof(AudioSource))]
[RequireComponent (typeof(AnimationController))]
public class FiringController : MonoBehaviour
{
    [Header("Weapon Data")]
    [SerializeField] WeaponData weaponData;

    [Header("Aiming Settings")]
    [SerializeField] GameObject bulletHole;
    [SerializeField] Transform aimPoint;
    [SerializeField] LayerMask collisionMask;

    [Header("Effects")]
    [SerializeField] GameObject shotParticlesFX;


    InputAction m_aimAction;
    InputAction m_attackAction;
    InputAction m_lookAction;

    private AudioSource m_audioSource;
    private AnimationController m_animationController;
    private Camera m_camera;
    private bool m_isAiming;
    private bool m_isFiring;
    private Vector2 m_lookInput;
    private Vector3 m_targetPoint;
    private Vector3 m_targetPointNormal;
    private Vector3 m_targetDirection;
    private Vector3 m_lastTargetDirection = Vector3.zero;
    private float m_shootTimer = 0f;

    public bool IsAiming { get => m_isAiming; }
    public Vector3 TargetDirection { get => m_targetDirection; }

    private void Start()
    {
        m_aimAction = InputSystem.actions.FindAction("Aim");
        m_attackAction = InputSystem.actions.FindAction("Attack");
        m_lookAction = InputSystem.actions.FindAction("Look");

        m_audioSource = GetComponent<AudioSource>();
        m_animationController = GetComponent<AnimationController>();

        m_camera = Camera.main;

        m_animationController.PlayAnimationInLayer(weaponData.idleAnimation.name, 1);
    }

    private void Update()
    {
        m_isAiming = m_aimAction.IsPressed();
        m_isFiring = (m_isAiming) ? m_attackAction.IsPressed() : false;

        var device = m_lookAction.activeControl.device;

        
        if (device is Gamepad)
            m_lookInput = (m_isAiming) ? m_lookAction.ReadValue<Vector2>() : m_lastTargetDirection;
        else if(device is Mouse)
        {
            // TODO: Implement priority of Gamepad over Mouse controls.
        }
    }

    private void FixedUpdate()
    {
        if (m_isAiming) aim();

        // Timer for the weapon fire rate
        m_shootTimer += Time.deltaTime;
        if (m_shootTimer < weaponData.fireRate)
            return;

        if (m_isFiring) fire(); 
    }

    private void fire()
    {
        // Reseting the shoot timer
        m_shootTimer = 0f;

        if (!weaponData)
        {
            Debug.LogError("No weapon data added to ShootController.");
            return;
        }

        Collider[] _objectsInSphere = Physics.OverlapSphere(m_targetPoint, weaponData.impactRadius, collisionMask);
        foreach (Collider _col in _objectsInSphere)
        {
            // Verify if object can be damageable
            IDamageable _damageable = _col.GetComponent<IDamageable>();
            if (_damageable != null && _col.gameObject != gameObject)
                _damageable.TakeDamage(weaponData.damage); // Apply damage

        }

        m_animationController.PlayAnimationInLayer(weaponData.fireAnimation.name, 1);
        m_audioSource.PlayOneShot(weaponData.attackSound);
        GameObject _shotParticleFX = Instantiate<GameObject>(shotParticlesFX, m_targetPoint, Quaternion.LookRotation(m_targetPointNormal));
    }

    private void aim()
    {
        // Transforming movement direction to align with the camera's orientation
        Vector3 _cameraForward = m_camera.transform.forward; // Forward direction of the camera
        Vector3 _cameraRight = m_camera.transform.right;     // Right direction of the camera

        // Remove any vertical component to keep movement on the horizontal plane
        _cameraForward.y = 0f;
        _cameraRight.y = 0f;

        // Normalize the directions
        _cameraForward.Normalize();
        _cameraRight.Normalize();

        m_targetDirection = _cameraForward * m_lookInput.y + _cameraRight * m_lookInput.x;
        m_lastTargetDirection = m_targetDirection;

        Ray _ray = new Ray(aimPoint.position, m_targetDirection);
        RaycastHit _hit;

        if (Physics.Raycast(_ray, out _hit))
        {
           
            m_targetPoint = _hit.point;
            m_targetPointNormal = _hit.normal;

            // Draw a plane around the target point
            DrawDebugPlane(m_targetPoint, _hit.normal, 0.2f);
            // Draw the aiming line
            Debug.DrawLine(aimPoint.position, m_targetPoint, Color.red, Time.deltaTime);
        }
    }

    void DrawDebugPlane(Vector3 _center, Vector3 _normal, float _size)
    {
        // Calcula los ejes perpendiculares al normal
        Vector3 _right = Vector3.Cross(_normal, Vector3.up).normalized;
        if (_right == Vector3.zero) // Caso especial cuando el normal es (0, 1, 0)
            _right = Vector3.Cross(_normal, Vector3.forward).normalized;

        Vector3 _forward = Vector3.Cross(_normal, _right).normalized;

        // Escala los vectores por el tamaño del plano
        _right *= _size;
        _forward *= _size;

        // Define las esquinas del plano
        Vector3 _topLeft = _center + _forward - _right;
        Vector3 _topRight = _center + _forward + _right;
        Vector3 _bottomLeft = _center - _forward - _right;
        Vector3 _bottomRight = _center - _forward + _right;

        // Dibuja las líneas del plano
        Debug.DrawLine(_topLeft, _topRight, Color.red, Time.deltaTime);
        Debug.DrawLine(_topRight, _bottomRight, Color.red, Time.deltaTime);
        Debug.DrawLine(_bottomRight, _bottomLeft, Color.red, Time.deltaTime);
        Debug.DrawLine(_bottomLeft, _topLeft, Color.red, Time.deltaTime);
    }
}
public interface IDamageable
{
    void TakeDamage(float _damage);
}
