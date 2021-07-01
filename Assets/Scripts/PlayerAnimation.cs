using UnityEngine;

public class PlayerAnimation : Player
{
    private Animator m_Animator;

    private enum Facing
    {
        up,
        down,
        left,
        right
    }

    private enum AnimList
    {
        idle_up,
        idle_down,
        idle_left,
        idle_right,
        walk_up,
        walk_down,
        walk_left,
        walk_right,
    }
    
    private Facing m_Facing;
    private string m_CurrentAnimation;
    
    protected override void Awake()
    {
        base.Awake();
        m_Animator = GetComponent<Animator>();
        m_Facing = Facing.down;
    }

    private void FixedUpdate()
    {
        ChangeAnimation();
    }

    private void ChangeAnimation()
    {
        if (m_rb.velocity == Vector2.zero)
        {
            if (m_Facing == Facing.up) PlayAnimation(AnimList.idle_up);
            else if (m_Facing == Facing.down) PlayAnimation(AnimList.idle_down);
            else if (m_Facing == Facing.left) PlayAnimation(AnimList.idle_left);
            else if (m_Facing == Facing.right) PlayAnimation(AnimList.idle_right);
        }
        else
        {
            if (m_rb.velocity.x > 0.1f)
            {
                PlayAnimation(AnimList.walk_right);
                m_Facing = Facing.right;
            } 
            else if (m_rb.velocity.x < -0.1f)
            {
                PlayAnimation(AnimList.walk_left);
                m_Facing = Facing.left;
            }
            else if (m_rb.velocity.y > -0.1f)
            {
                PlayAnimation(AnimList.walk_up);
                m_Facing = Facing.up;
            }
            else if (m_rb.velocity.y < 0.1f)
            {
                PlayAnimation(AnimList.walk_down);
                m_Facing = Facing.down;
            }
        }

    }

    private void PlayAnimation(AnimList animationName)
    {
        if (m_CurrentAnimation == animationName.ToString()) return;
        m_CurrentAnimation = animationName.ToString();
        m_Animator.Play(m_CurrentAnimation);
    }
}
