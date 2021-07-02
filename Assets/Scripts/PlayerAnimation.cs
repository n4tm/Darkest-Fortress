using System.Collections;
using UnityEngine;

public class PlayerAnimation : Player
{
    [SerializeField] private float blinkIntensity;
    private Animator _animator;
    private SpriteRenderer _renderer;

    private enum Facing
    {
        Up,
        Down,
        Left,
        Right
    }

    private enum AnimList
    {
        IdleUp,
        IdleDown,
        IdleLeft,
        IdleRight,
        WalkUp,
        WalkDown,
        WalkLeft,
        WalkRight,
    }
    
    private Facing _facing;
    private string _currentAnimation;
    
    protected override void Awake()
    {
        base.Awake();
        _animator = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
        _facing = Facing.Down;
    }

    private void FixedUpdate()
    {
        ChangeAnimation();
    }

    private void ChangeAnimation()
    {
        if (Rb.velocity == Vector2.zero)
        {
            if (_facing == Facing.Up) PlayAnimation(AnimList.IdleUp);
            else if (_facing == Facing.Down) PlayAnimation(AnimList.IdleDown);
            else if (_facing == Facing.Left) PlayAnimation(AnimList.IdleLeft);
            else if (_facing == Facing.Right) PlayAnimation(AnimList.IdleRight);
        }
        else
        {
            if (Rb.velocity.x > 0.1f)
            {
                PlayAnimation(AnimList.WalkRight);
                _facing = Facing.Right;
            } 
            else if (Rb.velocity.x < -0.1f)
            {
                PlayAnimation(AnimList.WalkLeft);
                _facing = Facing.Left;
            }
            else if (Rb.velocity.y > -0.1f)
            {
                PlayAnimation(AnimList.WalkUp);
                _facing = Facing.Up;
            }
            else if (Rb.velocity.y < 0.1f)
            {
                PlayAnimation(AnimList.WalkDown);
                _facing = Facing.Down;
            }
        }

    }

    public void Blink(float duration)
    {
        StartCoroutine(ApplyBlinkEffect(duration));
    }

    IEnumerator ApplyBlinkEffect(float duration)
    {
        float timePassed = 0;
        while (timePassed < duration)
        {
            timePassed += blinkIntensity;
            _renderer.enabled = !_renderer.enabled;
            yield return new WaitForSeconds(blinkIntensity);
        }

        _renderer.enabled = true;
    }

    private void PlayAnimation(AnimList animationName)
    {
        if (_currentAnimation == animationName.ToString()) return;
        _currentAnimation = animationName.ToString();
        _animator.Play(_currentAnimation);
    }
}
