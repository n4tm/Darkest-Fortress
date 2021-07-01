using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject shootPrefab;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float projectileDamage;
    [SerializeField] private float projectilePoolSize;
    
    private InputMap m_InputMap;
    private Queue<GameObject> projectilePool;

    void Awake()
    {
        m_InputMap = new InputMap();
        m_InputMap.Player.Shoot.performed += Shoot;
        projectilePool = new Queue<GameObject>();
        for (int i = 0; i < projectilePoolSize; i++)
        {
            GameObject newProjectile = Instantiate(shootPrefab, spawnPoint.position, transform.rotation);
            Projectile projectileComponent = newProjectile.GetComponent<Projectile>();
            projectileComponent.Speed = projectileSpeed;
            projectileComponent.Damage = projectileDamage;
            projectilePool.Enqueue(newProjectile);
            newProjectile.SetActive(false);
        }
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
        GameObject projectile = projectilePool.Dequeue();
        projectile.transform.position = spawnPoint.position;
        projectile.transform.rotation = transform.rotation;
        
        projectile.SetActive(true);
        projectilePool.Enqueue(projectile);
    }
}
