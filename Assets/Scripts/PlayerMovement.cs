using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Variables
    public GameObject player;
    public float speed;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;

}

    // Update is called once per frame
    void Update()
    {

        float verticalMovement = Input.GetAxisRaw("Vertical") * speed * Time.deltaTime;
        float horizontalMovement = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;

        player.transform.Translate(new Vector3(horizontalMovement, verticalMovement, 0));
    }
}
