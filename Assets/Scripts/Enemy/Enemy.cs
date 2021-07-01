using UnityEngine;

namespace Enemy
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] protected float speed;
        [SerializeField] protected float health;
        [SerializeField] protected float damage;

        protected Transform player;
        protected Rigidbody2D m_rb;
        protected Animator m_Animator;
        
        private float m_CurrentHealth;

        protected virtual void Awake()
        {
            m_CurrentHealth = health;
            m_rb = GetComponent<Rigidbody2D>();
            m_Animator = GetComponent<Animator>();
            
            player = FindObjectOfType<PlayerMovement>().transform;
        }

        public virtual void ReceiveHit(float dmg)
        {
            m_CurrentHealth -= dmg;
            if (m_CurrentHealth <= 0)
            {
                StartDeath();
            }
        }

        protected virtual void StartDeath()
        {
            Destroy(gameObject);
        }
    }
}
