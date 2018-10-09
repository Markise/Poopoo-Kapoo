using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyPatrol : MonoBehaviour {

    public twoDMovement player;
    public Transform wallCheck;
    public Transform edgeCheck;
    public LayerMask floor;
    public float wallCheckRadius;
    public float GroundCheckRadius;
    public float speed;

    private bool moveRight;
    private bool hittingWall;
    private bool closeToEdge;

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<twoDMovement>();
	}
	
	// Update is called once per frame
	void Update ()
    {

        hittingWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, floor);
        closeToEdge = Physics2D.OverlapCircle(edgeCheck.position, GroundCheckRadius, floor);

        if(hittingWall || !closeToEdge)
        {
            moveRight = !moveRight;
        }

        if (moveRight)
        {
            transform.localScale = new Vector2(1f, 1f);
            GetComponent<Rigidbody2D>().velocity = new Vector2(speed, GetComponent<Rigidbody2D>().velocity.y);
        } else
          {
            transform.localScale = new Vector2(-1f, 1f);
            GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, GetComponent<Rigidbody2D>().velocity.y);
          }
	}
}
