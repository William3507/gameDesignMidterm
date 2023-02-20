using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{

    public float speed;
    private Rigidbody2D rb2d;
    public SpriteRenderer mySpriteRenderer;
    
    public float movementHorizontal;
    public float movementVertical;
    public float jumpForce = 35;

    private bool canJump;

    float frameTimer;
    int currentFrame;

    // Start is called before the first frame update
    void Start()
    {

        rb2d = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        movementHorizontal = 0;
        movementVertical = 0;
        Vector2 vel = rb2d.velocity;

        if (Input.GetKey(KeyCode.A))
        {
            movementHorizontal = -speed;
        }
        

        if (Input.GetKey(KeyCode.D))
        {
            movementHorizontal = speed;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (canJump)
            {
                vel.y = jumpForce;
            }
        }

        vel = new Vector2(movementHorizontal, vel.y);
        rb2d.velocity = vel;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("canJump"))
        {
            canJump = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("canJump"))
        {
            canJump = false;

        }

    }

}
