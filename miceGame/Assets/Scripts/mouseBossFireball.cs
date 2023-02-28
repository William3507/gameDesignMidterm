using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseBossFireball : MonoBehaviour
{
    public float health;
    public float jumpForce;
    public bool grounded;

    [SerializeField] float moveSpeed = 5f;
    Transform target;
    Vector2 moveDirection;

    Rigidbody2D rb2d;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }


    // Update is called once per frame
    void Update()
    {
        isGrounded();

        if(target)
        {
            Vector3 direction = (target.position - transform.position).normalized;

            moveDirection = direction;
        }


        //if (grounded)
       // {
        //    rb2d.AddForce(Vector2.up * jumpForce,ForceMode2D.Force);
        //}
        

    }


    private void FixedUpdate()
    {
        if(target != null)
        {
            rb2d.velocity = new Vector2(moveDirection.x, moveDirection.y) * moveSpeed;
        }
    }


    protected bool isGrounded()
    {
        Vector3 rayStart = transform.position + Vector3.down * 0.5f;

        RaycastHit2D hit = Physics2D.Raycast(rayStart, Vector2.down, 0.1f);

        if (hit.collider != null)
        {
            grounded = true;
            return true;
        }

        grounded = false;
        return false;

    }
}
