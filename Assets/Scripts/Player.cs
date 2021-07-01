using UnityEngine;

public class Player : MonoBehaviour
{
    protected Rigidbody2D m_rb;

    protected virtual void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }
}
