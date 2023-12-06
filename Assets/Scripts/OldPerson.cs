using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class OldPerson : MonoBehaviour
{
    int dir = 0;  // 0 = right, 1 = down, 2 = left, 3 = up
    float moveChance = 0.75f;
    float moveTimeMin = 0.25f;
    float moveTimeMax = 1f;
    float waitTimeMin = 0.25f;
    float waitTimeMax = 0.75f;
    float moveTime;
    float waitTime;
    float activedXVicinity = 11f;  // how close old person has to be to the player to start attacking and moving around (x distance)
    float activedYVicinity = 6f;  // how close old person has to be to the player to start attacking and moving around (y distance)
    float maxVicinity = 4f;  // how far old person can wander from player (both x and y)
    bool checkingForVicinity = true;  // will stop checking if the old person is near the player once they have started attacking and wandering
    bool moving = false;
    bool justWaited = false;  // stops old person from waiting to move multiple times in a row
    float returnTime = 1;  // how long elderly person spends travelling back to player after it has wandered off outisde of active vicinity
    float speed = 3;
    Rigidbody2D rb;
    float attackWaitTimeMin = 0.75f;
    float attackWaitTimeMax = 2f;
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
    }

    void Update()
    {
        // change direction
        if (moving)
        {
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

        else rb.velocity = Vector2.zero;


        // start wandering around and attacking when in vicinity of player
        if (checkingForVicinity)
            if (Math.Abs(player.transform.position.x - transform.position.x) < activedXVicinity && Math.Abs(player.transform.position.y - transform.position.y) < activedYVicinity)
            {
                StartCoroutine(AttackWait());
                StartCoroutine(Movement());

                checkingForVicinity = false;
            }
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
        // if old person is in vicinity of player then wander randomly
        if (Math.Abs(player.transform.position.x - transform.position.x) < maxVicinity && Math.Abs(player.transform.position.y - transform.position.y) < maxVicinity)
        {
            // moveChance chance of moving instead waiting
            if (Random.value > moveChance && !justWaited) moving = false;  // old person will wait before moving
            else moving = true;

            if (moving)
            {
                justWaited = false;
                dir = Random.Range(0, 4);
                moveTime = Random.Range(moveTimeMin, moveTimeMax);
                yield return new WaitForSeconds(moveTime);
            }

            else
            {
                justWaited = true;
                waitTime = Random.Range(waitTimeMin, waitTimeMax);  // how long old person will wait before moving again
                yield return new WaitForSeconds(waitTime);
            }
        }

        // if outside of x range, figure out where elderly person needs to go to get back within vicinity of player
        else if (Math.Abs(player.transform.position.y - transform.position.y) < maxVicinity)
        {
            switch (player.transform.position.x - transform.position.x > 0)
            {
                case true:  // elderly person is on left of player
                    dir = 0; break;

                case false:  // elderly person is on right of player
                    dir = 2; break;
            }

            moving = true;
            yield return new WaitForSeconds(returnTime);
        }

        // if outside of y range
        else
        {
            switch (player.transform.position.y - transform.position.y > 0)
            {
                case true:  // elderly person is below player
                    dir = 3; break;

                case false:  // elderly person is above player
                    dir = 1; break;
            }

            moving = true;
            yield return new WaitForSeconds(returnTime);
        }

        StartCoroutine(Movement());
    }

    IEnumerator AttackWait()  // have a random chance of old person attacking
    {
        yield return new WaitForSeconds(Random.Range(attackWaitTimeMin, attackWaitTimeMax));
        
        Attack();
        StartCoroutine(AttackWait());
    }
}
