using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseBossController : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb2d;
    private SpriteRenderer sr;
    public Sprite[] idle;
    public Sprite[] WalkCycle;
    public Sprite[] Jump;

    public float jumpForce = 35;
    public float groundDistance = .1f;

    public float health = 3;
    public bool invincible = false;
    public float hurtTime = 5;
    public float lengthRay = 0.9f;

    private Sprite[] currentCycle;
    public float fps = 8;
    private int currentFrame = 0;

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

        isHit();
    }

    IEnumerator AnimationCycler()
    {
        while (true)
        {
            if (currentFrame >= currentCycle.Length - 2)
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

    public void isHit()
    {
        RaycastHit2D upHit = Physics2D.Raycast(transform.position + new Vector3(0,1,0), Vector2.up, lengthRay, LayerMask.GetMask("Player"));
        RaycastHit2D leftUpHit = Physics2D.Raycast(transform.position + new Vector3(0, 1, 0), new Vector2(-1f, 1f), lengthRay, LayerMask.GetMask("Player"));
        RaycastHit2D rightUpHit = Physics2D.Raycast(transform.position + new Vector3(0, 1, 0), new Vector2(1f, 1f), lengthRay, LayerMask.GetMask("Player"));

        if (upHit.collider != null || leftUpHit.collider != null || rightUpHit.collider != null)
        {
            Hurt();
        }
    }

    public void Hurt()
    {
        if (!invincible)
            {
                StartCoroutine(invincibility(1.2f));

                health -= 1;

            }


        if (health <= 0)
            {
                Destroy(gameObject);
            }
        }

    IEnumerator invincibility(float time)
    {
        invincible = true;

        yield return new WaitForSeconds(hurtTime);

        invincible = false;

    }
}
