using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    public LayerMask platforms;
    public float lengthRay = .9f;
    private bool invincible = false;
    public int hurtTime = 1;

    public float health = 3;

    public Sprite[] idle;
    public Sprite[] WalkCycle;
    public Sprite[] Hide;

    private Sprite[] currentCycle;
    public float fps = 8;
    private int currentFrame = 0;
    bool blinking = false;

    protected Rigidbody2D rb2d;
    protected SpriteRenderer sr;
    


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

        RaycastHit2D upHit = Physics2D.Raycast(transform.position, Vector2.up, lengthRay, LayerMask.GetMask("Player"));
        RaycastHit2D leftUpHit = Physics2D.Raycast(transform.position, new Vector2(-0.5f, 1f), lengthRay, LayerMask.GetMask("Player"));
        RaycastHit2D rightUpHit = Physics2D.Raycast(transform.position, new Vector2(0.5f, 1f), lengthRay, LayerMask.GetMask("Player"));

        Debug.DrawLine(transform.position, Vector2.up * lengthRay * 2, Color.blue);



        if (upHit.collider != null || leftUpHit.collider != null || rightUpHit.collider != null)
        {
            Hurt();
        }

        if (rb2d.velocity.x > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (rb2d.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        if (rb2d.velocity.x != 0)
        {
            currentCycle = WalkCycle;

        }
        else if (health == 1)
        {
            currentCycle = Hide;
        }
        else
        {
            currentCycle = idle;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    IEnumerator invincibility(float time)
    {
        invincible = true;

        yield return new WaitForSeconds(hurtTime);

        invincible = false;

    }
    IEnumerator AnimationCycler()
    {
        while (true)
        {
            if (invincible)
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
}
