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
    [SerializeField] private float lifeTime;
    [SerializeField] private float fireRate;
    
    private InputMap _inputMap;
    private Queue<GameObject> _projectilePool;
    private float _fireRateTimer;
    private bool _shootPerformed;
    
    private void Awake()
    {
        _inputMap = new InputMap();
        _inputMap.Player.Shoot.performed += ShootPerformed;
        _inputMap.Player.Shoot.canceled += ShootPerformed;
        _projectilePool = new Queue<GameObject>();
        for (int i = 0; i < projectilePoolSize; i++)
        {
            GameObject newProjectile = Instantiate(shootPrefab, spawnPoint.position, transform.rotation);
            Projectile projectileComponent = newProjectile.GetComponent<Projectile>();
            projectileComponent.Speed = projectileSpeed;
            projectileComponent.Damage = projectileDamage;
            _projectilePool.Enqueue(newProjectile);
            newProjectile.SetActive(false);
        }
    }

    private void OnEnable() => _inputMap.Enable();
    private void OnDisable() => _inputMap.Disable();
    
    private void Update()
    {
        AdjustRotation();
        _fireRateTimer += Time.deltaTime;
        if (_fireRateTimer > fireRate && _shootPerformed)
        {
            Shoot();
            _fireRateTimer = 0;
        }
    }

    private void AdjustRotation()
    {
        Vector2 direction = UnityEngine.Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = rotation;
    }

    private void ShootPerformed(InputAction.CallbackContext ctx)
    {
        if (ctx.performed) _shootPerformed = true;
        else if (ctx.canceled) _shootPerformed = false;
    }

    private void Shoot()
    {
        GameObject projectile = _projectilePool.Dequeue();
        projectile.transform.position = spawnPoint.position;
        projectile.transform.rotation = transform.rotation;
        projectile.SetActive(true);
        projectile.GetComponent<Projectile>().DeactivateProjectile(lifeTime);
        _projectilePool.Enqueue(projectile);
    }
}
