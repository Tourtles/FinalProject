using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    Rigidbody2D rigidbody2d;
    // Start is called before the first frame update
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction * force);
    }
    
    // Update is called once per frame
    void Update()
    {
        if(transform.position.magnitude > 1000.0f)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        EnemyController e = other.collider.GetComponent<EnemyController>();
        if (e != null)
        {
            e.Fix();
        }

        EnemyController2 a = other.collider.GetComponent<EnemyController2>();
        if (a != null)
        {
            a.Fix();
        }

        EnemyController3 b = other.collider.GetComponent<EnemyController3>();
        if (b != null)
        {
            b.Fix();
        }

        EnemyController4 k = other.collider.GetComponent<EnemyController4>();
        if (k != null)
        {
            k.Fix();
        }

        //we also add a debug log to know what the projectile touch
        Destroy(gameObject);
    }
}
