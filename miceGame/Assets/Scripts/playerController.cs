using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour
{

    public float speed;
    private Rigidbody2D rb2d;
    private SpriteRenderer sr;
    public Sprite[] idle;
    public Sprite[] WalkCycle;
    public Sprite[] Jump;
    
    public float jumpForce = 35;
    public float groundDistance = .1f;


    private Sprite[] currentCycle;
    public float fps = 8;
    private int currentFrame = 0;
    bool blinking = false;

    [SerializeField]
    private LayerMask Platform;

    // Start is called before the first frame update
    void Start()
    {

        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        currentCycle = idle;
        StartCoroutine(AnimationCycler());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float movementHorizontal = 0;
        Vector2 vel = rb2d.velocity;

        if (Input.GetKey("a"))
        {
            movementHorizontal = -speed;
        }
        if (Input.GetKey("d"))
        {
            movementHorizontal = speed;
        }

        if (Input.GetKey("space") || Input.GetKey("w"))
        {
            if (isGrounded())
            {
                vel.y = jumpForce;
            }
            
        }

        vel = new Vector2(movementHorizontal, vel.y);
        rb2d.velocity = vel;

        if(rb2d.velocity.y != 0)
        {
            currentCycle = Jump;
        }
        else if (rb2d.velocity.x != 0)
        {
            currentCycle = WalkCycle;
            
        }
        else
        {
            currentCycle = idle;
        }
        
        if (rb2d.velocity.x > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (rb2d.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
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

    IEnumerator AnimationCycler ()
    {
        while (true)
        {
            if (HealthManager.instance.isHurting)
            {
                blinking = !blinking;
            }
            else
            {
                blinking = false;
            }

            if (blinking)
            {
                sr.color = Color.red;
            }
            else
            {
                sr.color = Color.white;
            }

            if (currentFrame >= currentCycle.Length - 1)
            {
                currentFrame = 0;
            }
            else
            {
                currentFrame++;
            }
            sr.sprite = currentCycle[currentFrame];
            yield return new WaitForSeconds(1f / fps);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Cheese"))
        {
            InventoryManager.instance.GotCheese();
            Destroy(collision.gameObject);
        }
    }
}
