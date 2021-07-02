using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float invulnerabilityAfterHit;
    [SerializeField] private float currentHealth;

    private PlayerController _playerController;

    private float _invulnerabilityTimer;

    protected void Awake()
    {
        currentHealth = health;
        _playerController = GetComponent<PlayerController>();
    }

    public void ReceiveHit(float damage, Vector3 aggressorPosition)
    {
        if (Time.time < _invulnerabilityTimer) return;
        _invulnerabilityTimer = Time.time + invulnerabilityAfterHit;
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            ActivateDeath();
        }
        else
        {
            StunPlayer(aggressorPosition);
        }
    }
    
    private void StunPlayer(Vector3 aggressorPosition)
    {
        _playerController.ActivatePush(aggressorPosition);
        _playerController.ActivateBlink(invulnerabilityAfterHit);
    }
    
    private void ActivateDeath()
    {
        Destroy(gameObject);
    }
}
