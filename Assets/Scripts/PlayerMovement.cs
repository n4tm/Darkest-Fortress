using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private InputMap m_InputMap;
    private Vector2 m_Direction;
    private Rigidbody2D m_rb;
    [SerializeField] private float speed;

    private void Awake()
    {
        m_InputMap = new InputMap();
        m_rb = GetComponent<Rigidbody2D>();
        m_InputMap.Player.Movement.performed += ctx => m_Direction = ctx.ReadValue<Vector2>();
        m_InputMap.Player.Movement.canceled += ctx => m_Direction = Vector2.zero;
    }

    private void OnEnable() => m_InputMap.Enable();

    private void OnDisable() => m_InputMap.Disable();

    private void FixedUpdate()
    {
        m_rb.velocity = m_Direction * speed;
    }
}
