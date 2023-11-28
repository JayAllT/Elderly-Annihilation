using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldPerson : MonoBehaviour
{
    public int dir = 0;  // 0 = right, 1 = down, 2 = left, 3 = up
    Vector2 velocity = Vector2.zero;
    Rigidbody2D rb;
    int attackWaitTime = 1;
    float attackChance = 0.5f;
    
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


    }

    void Attack()
    {
        Debug.Log("attack");


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
