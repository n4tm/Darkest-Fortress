using System.Collections;
using UnityEngine;

public class PlayerMovement : Player
{
    [SerializeField] private float speed;
    [SerializeField] private float pushForce;
    [SerializeField] private float pushDuration;
    
    private InputMap _inputMap;
    private Vector2 _direction;
    private bool _isBeingPushed;
    private Vector3 _pushDirection;
    
    protected override void Awake()
    {
        base.Awake();
        _inputMap = new InputMap();
        _inputMap.Player.Movement.performed += ctx => _direction = ctx.ReadValue<Vector2>();
        _inputMap.Player.Movement.canceled += ctx => _direction = Vector2.zero;
    }

    private void OnEnable() => _inputMap.Enable();

    private void OnDisable() => _inputMap.Disable();

    private void FixedUpdate()
    {
        if (_isBeingPushed) Rb.velocity = _pushDirection * pushForce;
        else Rb.velocity = _direction * speed;
    }

    public void Push(Vector3 aggressorPosition)
    {
        _pushDirection = (transform.position - aggressorPosition).normalized;
        _isBeingPushed = true;
        StartCoroutine(DeactivatePush());
    }

    private IEnumerator DeactivatePush()
    {
        yield return new WaitForSeconds(pushDuration);
        _isBeingPushed = false;
    }
}
