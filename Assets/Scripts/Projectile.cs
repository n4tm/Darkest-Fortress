using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Speed { get; set; }
    public float Damage { get; set; }

    private void Update()
    {
        transform.Translate(Vector2.up * (Speed * Time.deltaTime));
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;
        other.GetComponent<Enemy.Enemy>().ReceiveHit(Damage);
        gameObject.SetActive(false);
    }
}
