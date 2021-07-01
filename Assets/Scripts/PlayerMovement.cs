using UnityEngine;

public class PlayerMovement : Player
{
    private InputMap m_InputMap;
    private Vector2 m_Direction;
    [SerializeField] private float speed;

    protected override void Awake()
    {
        base.Awake();
        m_InputMap = new InputMap();
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
