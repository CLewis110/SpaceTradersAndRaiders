using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroids : MonoBehaviour
{
    private float rotationSpeed;

    private Rigidbody2D rb;

    public float speed;

    private Vector2 direction;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rotationSpeed = Random.Range(-25, 25);
        speed = Random.Range(5, 15);
        direction = new Vector3(Random.Range(-359, 359), Random.Range(-359, 359), 0);
        rb.AddForce(direction * speed);
    }

    void Update()
    {

        transform.Rotate(new Vector3(0, 0, rotationSpeed) * Time.deltaTime);
        CheckPosition();
    }

    public void CheckPosition()
    {
        if(transform.position.x < -60 || transform.position.x > 210)
        {
            Destroy(gameObject);
        }

        if(transform.position.y < -70 || transform.position.y > 220)
        {
            Destroy(gameObject);
        }
    }
}
