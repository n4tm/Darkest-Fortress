using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Speed { get; set; }
    public float Damage { get; set; }
    private IEnumerator _projectileCoroutine;
    private void Update()
    {
        transform.Translate(Vector2.up * (Speed * Time.deltaTime));
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy")) other.GetComponent<Enemy.Enemy>().ReceiveHit(Damage);
        if (other.CompareTag("Wall") || other.CompareTag("Enemy")) gameObject.SetActive(false);
    }

    public void DeactivateProjectile(float projectileLifeTime)
    {
        if (_projectileCoroutine != null) StopCoroutine(_projectileCoroutine);
        _projectileCoroutine = DeactivateInTime(projectileLifeTime);
        StartCoroutine(_projectileCoroutine);
    }

    private IEnumerator DeactivateInTime(float projectileLifeTime)
    {
        yield return new WaitForSeconds(projectileLifeTime);
        gameObject.SetActive(false);
    }
}