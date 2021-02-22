using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AntiMissiles : MonoBehaviour
{
    public Transform target;

    public float speed = 30f;
    public float rotateSpeed = 800f;

    public GameObject explosion;

    private Rigidbody2D rb;


    void Start()
    {
        FindTarget();
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        FindTarget();
        Vector2 direction = (Vector2)target.position - rb.position;

        direction.Normalize();

        float rotateAmount = Vector3.Cross(direction, transform.up).z;

        rb.angularVelocity = -rotateAmount * rotateSpeed;

        rb.velocity = transform.up * speed;
    }

    void FindTarget()
    { 
        target = GameObject.FindGameObjectWithTag("Missile").transform;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Missile")
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }

    }
}

