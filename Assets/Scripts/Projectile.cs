using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Speed { get; set; }
    public float Damage { get; set; }

    private void Update()
    {
        transform.Translate(Vector2.up * (Speed * Time.deltaTime));
    }
}
