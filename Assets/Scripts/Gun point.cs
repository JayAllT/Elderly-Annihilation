using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunpoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var mousePosition = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        Vector2 direction = new Vector2(mousePosition.x - transform.position.x,mousePosition.y - transform.position.y);

        transform.right = direction;
        
        
        
    }
}
