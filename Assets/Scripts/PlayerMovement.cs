using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Variables
    public GameObject player;
    public float speed;
    private Rigidbody2D rb;

    public bool canRoll;
    public float rollTime;
    public bool canWalk;

    public float rollSpeed;
    public float rollDistance;

    public Vector2 rollStartPosition;


    void Start()
    {
        //Variables
        canWalk = true;
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;

    }

   
    void Update()
    {
        //Basic Player Movement
        float verticalMovement = Input.GetAxisRaw("Vertical") * speed * Time.deltaTime;
        float horizontalMovement = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;

        if (canWalk)
        {
            player.transform.Translate(new Vector3(horizontalMovement, verticalMovement, 0));
        }
        //Roll Mechanic

        if (Input.GetKeyDown(KeyCode.R) && canRoll)
        {
            StartCoroutine(Roll());

        }


    }

    private IEnumerator Roll()
    {
        canWalk= false;
        canRoll = false;

        var mousePosition = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        rollStartPosition = transform.position;

        rb.velocity = direction * rollSpeed;

        yield return new WaitUntil(() => Vector2.Distance(rollStartPosition, transform.position) >= rollDistance);

        rb.velocity = Vector2.zero;
        canRoll = true;
        canWalk= true;
    }



}
