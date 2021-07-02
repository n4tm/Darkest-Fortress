using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private PlayerAnimation _playerAnimation;

    protected void Awake()
    {
        _playerAnimation = GetComponent<PlayerAnimation>();
        _playerMovement = GetComponent<PlayerMovement>();
    }

    public void ActivatePush(Vector3 aggressorPosition)
    {
        _playerMovement.Push(aggressorPosition);
    }

    public void ActivateBlink(float duration)
    {
        _playerAnimation.Blink(duration);
    }
}
