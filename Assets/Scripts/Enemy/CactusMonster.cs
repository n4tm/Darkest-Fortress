using UnityEngine;

namespace Enemy
{
    public class CactusMonster : Enemy
    {
        private enum AnimList
        {
            walk_up,
            walk_down,
            walk_left,
            walk_right
        }
        
        private AnimList m_CurrentAnimation;
        
        private void FixedUpdate()
        {
            Vector2 direction = (player.position - transform.position).normalized * speed;
            m_rb.velocity = direction;
            
            AnimateEnemy();
        }

        private void AnimateEnemy()
        {
            if (Mathf.Abs(m_rb.velocity.x) > Mathf.Abs(m_rb.velocity.y))
            {
                if (m_rb.velocity.x > 0) ChangeAnimation(AnimList.walk_right);
                else ChangeAnimation(AnimList.walk_left);
            }
            else
            {
                if (m_rb.velocity.y > 0) ChangeAnimation(AnimList.walk_up);
                else ChangeAnimation(AnimList.walk_down);
            }
        }

        private void ChangeAnimation(AnimList animationName)
        {
            if (m_CurrentAnimation == animationName) return;
            m_CurrentAnimation = animationName;
            m_Animator.Play(animationName.ToString());
        }
    }
}
