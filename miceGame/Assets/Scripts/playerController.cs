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
    public Sprite WallJump;
    private bool invincible = false;
    private int lives = 3;
    
    public float jumpForce = 35;
    public float groundDistance = .1f;
    public float wallDistance = .6f;

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
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundDistance, Platform);
        if (hit.collider != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool isWalled()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, wallDistance, LayerMask.GetMask("Platform"));
        if (hit.collider != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Hurt()
    {
        if (invincible)
        {
            return;
        }
        lives--;
        if (lives < 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        StartCoroutine(invincibility(1));
        
    }

    IEnumerator invincibility(float time)
    {
        invincible = true;

        for (int i = 0; i < time / 0.5f; i++)
        {
            sr.color = Color.red;
            yield return new WaitForSeconds(0.25f);
            sr.color = Color.white;
            yield return new WaitForSeconds(0.25f);
        }

        invincible = false;

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
