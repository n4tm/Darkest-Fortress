using System.Collections;
using UnityEngine;

namespace Enemy
{
    public class CactusMonster : Enemy
    {
        private enum AnimList
        {
            WalkUp,
            WalkDown,
            WalkLeft,
            WalkRight
        }

        [SerializeField] private float minimalDistanceToPlayer;
        [SerializeField] private float attackSpeed;
        
        private AnimList _currentAnimation;
        private bool _isAttacking;

        private void FixedUpdate()
        {
            if (_isAttacking || Player == null) return;
            if ((transform.position - Player.position).sqrMagnitude > Mathf.Pow(minimalDistanceToPlayer, 2))
            {
                Vector2 direction = (Player.position - transform.position).normalized * speed;
                Rb.velocity = direction;
            }
            else
            {
                if (Time.time > AttackDelayTimer)
                {
                    AttackDelayTimer = Time.time + attackDelay;
                    StartCoroutine(ChargeAttack());
                }
                else
                {
                    Rb.velocity = Vector2.zero;
                }
            }

            AnimateEnemy();
        }

        private void AnimateEnemy()
        {
            if (Mathf.Abs(Rb.velocity.x) > Mathf.Abs(Rb.velocity.y))
            {
                ChangeAnimation(Rb.velocity.x > 0 ? AnimList.WalkRight : AnimList.WalkLeft);
            }
            else
            {
                ChangeAnimation(Rb.velocity.y > 0 ? AnimList.WalkUp : AnimList.WalkDown);
            }
        }

        private void ChangeAnimation(AnimList animationName)
        {
            if (_currentAnimation == animationName) return;
            _currentAnimation = animationName;
            Animator.Play(animationName.ToString());
        }

        IEnumerator ChargeAttack()
        {
            _isAttacking = true;
            Vector2 originalPosition = transform.position;
            Vector2 targetPosition = Player.position;

            float percent = 0;
            while (percent <= 1)
            {
                percent += Time.deltaTime * attackSpeed;
                float formula = (-Mathf.Pow(percent, 2) + percent) * 4;
                Rb.position = Vector2.Lerp(originalPosition, targetPosition, formula);
                yield return null;
            }

            _isAttacking = false;
        }
    }
}
