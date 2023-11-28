using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class OldPerson : MonoBehaviour
{
    public int dir = 0;  // 0 = right, 1 = down, 2 = left, 3 = up
    float speed = 3;
    Vector2 projectileVelocity = Vector2.zero;
    Rigidbody2D rb;
    int attackWaitTime = 1;
    float attackChance = 1f;
    public OldProjectile projectilePrefab;
    public GameObject player;
    float basicAngle;  // used for player location
    float trueAngle;
    float xDistance;
    float yDistance;
    OldProjectile projectile;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // get rigidbody

        StartCoroutine(Movement());
        StartCoroutine(AttackWait());
    }

    void Update()
    {
        // change direction
        switch (dir)
        {
            case 0:
                rb.velocity = new Vector2(1, 0);
                break;

            case 1:
                rb.velocity = new Vector2(0, -1);
                break;

            case 2:
                rb.velocity = new Vector2(-1, 0);
                break;

            case 3:
                rb.velocity = new Vector2(0, 1);
                break;
        }

        // apply speed
        rb.velocity *= speed;
    }

    void Attack()
    {
        // create projectile and get angle from the old person to the player
        projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectile.direction = GetBulletRotation();
    }

    // radians
    float GetBulletRotation()  // locates player in order to get velocity of projectile pointing in player's direction
    {
        xDistance = player.transform.position.x - transform.position.x;
        yDistance = player.transform.position.y - transform.position.y;

        basicAngle = Mathf.Atan(Mathf.Abs(yDistance / xDistance));

        if (xDistance > 0 && yDistance > 0)  // top right
            trueAngle = basicAngle;

        else if (xDistance < 0 && yDistance > 0)  // top left
            trueAngle = Mathf.PI - basicAngle;

        else if (xDistance < 0 && yDistance < 0)  // bottom left
            trueAngle = Mathf.PI + basicAngle;

        else if (xDistance > 0 && yDistance < 0)  // bottom right
            trueAngle = 2 * Mathf.PI - basicAngle;

        return trueAngle;
    }

    IEnumerator Movement()
    {
        yield return new WaitForSeconds(1);

        dir++;  // update direction

        // reset once dir reaches 4
        if (dir == 4)
            dir = 0;

        StartCoroutine(Movement());
    }

    IEnumerator AttackWait()  // have a random chance of old person attacking
    {
        yield return new WaitForSeconds(attackWaitTime);

        if (UnityEngine.Random.value < attackChance)
            Attack();

        StartCoroutine(AttackWait());
    }
}
