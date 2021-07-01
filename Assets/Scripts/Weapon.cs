using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject shootPrefab;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float projectileDamage;
    private InputMap m_InputMap;

    void Awake()
    {
        m_InputMap = new InputMap();
        m_InputMap.Player.Shoot.performed += ctx => Shoot(ctx);
    }

    private void OnEnable() => m_InputMap.Enable();
    private void OnDisable() => m_InputMap.Disable();
    
    private void Update()
    {
        AdjustRotation();
    }

    private void AdjustRotation()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = rotation;
    }

    private void Shoot(InputAction.CallbackContext ctx)
    {
        Debug.Log("shooted");
        GameObject newProjectile = Instantiate(shootPrefab, spawnPoint.position, transform.rotation);
        Projectile projectileComponent = newProjectile.GetComponent<Projectile>();
        projectileComponent.Speed = projectileSpeed;
        projectileComponent.Damage = projectileDamage;
    }
}
