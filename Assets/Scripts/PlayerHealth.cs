using Camera;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private float invulnerabilityAfterHit;
    [SerializeField] private int currentHealth;

    private PlayerController _playerController;

    private float _invulnerabilityTimer;

    protected void Awake()
    {
        currentHealth = health;
        _playerController = GetComponent<PlayerController>();
    }

    private void Start()
    {
        GameController.Instance.InsertNewHeart(health, true);
    }

    public void ReceiveHit(int damage, Vector3 aggressorPosition)
    {
        if (Time.time < _invulnerabilityTimer) return;
        _invulnerabilityTimer = Time.time + invulnerabilityAfterHit;
        currentHealth -= damage;
        GameController.Instance.UpdateHeartContainers(damage, false);
        if (currentHealth <= 0)
        {
            ActivateDeath();
        }
        else
        {
            HitFeedback(aggressorPosition);
        }
    }
    
    private void HitFeedback(Vector3 aggressorPosition)
    {
        _playerController.ActivatePush(aggressorPosition);
        _playerController.ActivateBlink(invulnerabilityAfterHit);
        CinemachineController.Instance.StartShake(10, .1f);
    }
    
    private void ActivateDeath()
    {
        Destroy(gameObject);
    }
}
