using UnityEngine;

namespace Enemy
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] protected float speed;
        [SerializeField] protected float health;
        [SerializeField] protected float damage;
        [SerializeField] protected float attackDelay;
        protected float AttackDelayTimer;

        protected Transform Player;
        protected Rigidbody2D Rb;
        protected Animator Animator;
        
        private float _currentHealth;

        protected virtual void Awake()
        {
            _currentHealth = health;
            Rb = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();
        }

        protected virtual void Start()
        {
            Player = FindObjectOfType<PlayerController>().transform;
        }

        public virtual void ReceiveHit(float dmg)
        {
            _currentHealth -= dmg;
            if (_currentHealth <= 0)
            {
                StartDeath();
            }
        }

        protected virtual void StartDeath()
        {
            Destroy(gameObject);
        }

        protected virtual void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.transform.CompareTag("Player")) return;
            other.transform.GetComponent<PlayerHealth>().ReceiveHit(damage, transform.position);
        }
    }
}
