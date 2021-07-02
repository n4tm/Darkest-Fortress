using UnityEngine;

public class Player : MonoBehaviour
{
    protected Rigidbody2D Rb;

    protected virtual void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
    }
}
