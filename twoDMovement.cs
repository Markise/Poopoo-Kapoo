using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class twoDMovement : MonoBehaviour
{
    public int jumpCount;
    public float speed;
    public float jumpHeight;
    public float groundRadius;
    public bool reversed;
    public Transform groundCheck;
    public Transform groundCheck2;
    public LayerMask ground;


    private bool grounded;
    private bool grounded2;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //left to right movement
        GetComponent<Rigidbody2D>().velocity = new Vector2(0f, GetComponent<Rigidbody2D>().velocity.y);
        if (Input.GetKey(KeyCode.A))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, GetComponent<Rigidbody2D>().velocity.y);

        }
        if (Input.GetKey(KeyCode.D))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector3(speed, GetComponent<Rigidbody2D>().velocity.y);

        }
        //Check for direction and flip player object
        if (GetComponent<Rigidbody2D>().velocity.x > 0)
        {
            transform.localScale = new Vector2(1f, 1f);
            
        }
        else if (GetComponent<Rigidbody2D>().velocity.x < 0)
        {
            transform.localScale = new Vector2(-1f, 1f);
        }

        //check if grounded for jump
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, ground);
        grounded2 = Physics2D.OverlapCircle(groundCheck2.position, groundRadius, ground);
        //jump
        if (Input.GetKeyDown(KeyCode.J) && grounded || Input.GetKeyDown(KeyCode.J) && grounded2)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
        }

        //accelerated falling
        if (GetComponent<Rigidbody2D>().velocity.y <= 0f)
        {
            GetComponent<Rigidbody2D>().velocity += Vector2.up * Physics2D.gravity.y * (2.5f) * Time.deltaTime;
        }

    } 


}
