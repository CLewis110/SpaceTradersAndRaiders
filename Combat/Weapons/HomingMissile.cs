using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HomingMissile : MonoBehaviour
{
    public Transform player;
    public Transform enemy;
    public Transform target;

    public float speed = 10f;
    public float rotateSpeed = 400f;

    public int damage = 15;

    public GameObject explosion;

    public GameObject battleSystem;
    private CameraShake camShake;

    private Rigidbody2D rb;

    void Start()
    {
        camShake = GameObject.Find("Fight Window Camera").GetComponent<CameraShake>();
        rb = GetComponent<Rigidbody2D>();
        battleSystem = GameObject.Find("Battle System");
        player = GameObject.Find("PlayerBattleStation").transform;
        enemy = GameObject.Find("EnemyBattleStation").transform;

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
        if(battleSystem.GetComponent<BattleSystem>().state == BattleState.PLAYERTURN)
        {
            target = player;
        }

        else if(battleSystem.GetComponent<BattleSystem>().state == BattleState.ENEMYTURN)
        {
            target = enemy;
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(Vector2.Distance(transform.position, collision.transform.position) > 0.3)
        {
            Instantiate(explosion, transform.position, transform.rotation);
            camShake.isHit = true;
            Destroy(gameObject);
        }

    }
}
