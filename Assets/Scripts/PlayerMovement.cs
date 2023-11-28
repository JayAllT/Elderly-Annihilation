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
    public float canWalk;


    void Start()
    {
        //Variables
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;

    }

   
    void Update()
    {
        //Basic Player Movement
        float verticalMovement = Input.GetAxisRaw("Vertical") * speed * Time.deltaTime;
        float horizontalMovement = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;

        if (canWalk = true)
        {
            player.transform.Translate(new Vector3(horizontalMovement, verticalMovement, 0));
        }
        //Roll Mechanic

        if (Input.GetMouseButtonDown(1) && canRoll)
        {
            var mousePosition = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);

            canWalk = false; 

            player.transform.Translate(direction)
        }


    }



}
