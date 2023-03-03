using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour
{

    public float speed;
    private Rigidbody2D rb2d;
    private SpriteRenderer sr;
    public Sprite[] WalkCycle;
    public Sprite Jump;
    private bool invincible = false;
    private int lives = 3;
    
    public float jumpForce = 35;
    public float groundDistance = .1f;

    float frameTimer;
    public float fps = 8;
    private int currentFrame = 0;

    [SerializeField]
    private LayerMask Platform;

    // Start is called before the first frame update
    void Start()
    {

        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float movementHorizontal = 0;
        Vector2 vel = rb2d.velocity;

        if (Input.GetKey("a"))
        {
            movementHorizontal = -speed;
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        if (Input.GetKey("d"))
        {
            movementHorizontal = speed;
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

        if (Input.GetKey("space") || Input.GetKey("w"))
        {
            Debug.Log(isGrounded());
            if (isGrounded())
            {
                vel.y = jumpForce;
            }
            /*
            else if (isWalled())
            {
                vel.y = jumpForce;
                movementHorizontal = -movementHorizontal;
            }
            */
            
        }

        vel = new Vector2(movementHorizontal, vel.y);
        rb2d.velocity = vel;
    }

    private bool isGrounded()
    {
        Vector3 rayStart = transform.position + Vector3.down * 0.5f;

        RaycastHit2D hit = Physics2D.Raycast(rayStart, Vector2.down, groundDistance, LayerMask.GetMask("Platform"));

        if (hit.collider != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    IEnumerator WalkCycler ()
    {
        while (true)
        {
            sr.sprite = WalkCycle[currentFrame];
            if (currentFrame > WalkCycle.Length - 1)
            {
                currentFrame = 0;
            }
            else
            {
                currentFrame++;
            }
            yield return new WaitForSeconds(1f / fps);
        }
    }
}
