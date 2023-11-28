using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldProjectile : MonoBehaviour
{
    public float direction = 0;  // radians
    float speed = 1f;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = new Vector2(Mathf.Cos(direction) * speed, Mathf.Sin(direction) * speed);
        transform.rotation = Quaternion.Euler(0, 0, direction * (180 / Mathf.PI));
    }

    
    void Update()
    {
        
    }
}
