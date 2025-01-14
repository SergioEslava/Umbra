using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootController : MonoBehaviour
{
    [SerializeField] Transform aimPoint;

    InputAction m_aimAction;
    InputAction m_attackAction;

    private Camera m_camera;
    private bool m_isAiming;
    private Vector3 m_targetPoint;

    public bool IsAiming { get => m_isAiming; }
    public Vector3 TargetPoint { get => m_targetPoint; }

    private void Start()
    {
        m_aimAction = InputSystem.actions.FindAction("Aim");
        m_attackAction = InputSystem.actions.FindAction("Attack");

        m_camera = Camera.main;
    }

    private void Update()
    {
        m_isAiming = m_aimAction.IsPressed();
    }

    private void FixedUpdate()
    {
        if (m_isAiming) aim();
    }

    private void aim()
    {
        Ray _ray = m_camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit _hit;

        if (Physics.Raycast(_ray, out _hit))
        {
           
            m_targetPoint = _hit.point;

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
