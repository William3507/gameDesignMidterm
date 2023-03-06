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
    Transform target;

    public float jumpForce = 5;
    public float groundDistance = .1f;

    public float health = 3;
    public bool invincible = false;
    public float hurtTime = 5;
    public float lengthRay = 0.9f;
    bool blinking = false;
    bool scurrying = false;

    private Sprite[] currentCycle;
    public float fps = 8;
    private int currentFrame = 0;

    //Mouse Magic Summon Fireball
    public GameObject fireBall;
    public GameObject cookieSprite;
    public GameObject theEnd;

    [SerializeField]
    private LayerMask Platform;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        currentCycle = idle;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(AnimationCycler());
        StartCoroutine(phaseCycler());
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

        if (target && !scurrying)
        {
            Vector3 direction = (target.position - transform.position).normalized;

            if (direction.x < 0)
            {
                sr.flipX = false;
            }
            else
            {
                sr.flipX = true;
            }

        }

        isHit();

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

    public void mouseJump(float towardsPlayer)
    {
        Vector3 direction = (target.position - transform.position).normalized;


        rb2d.velocity = new Vector2(towardsPlayer*direction.x*jumpForce, jumpForce);
    }

    public void mouseMagic()
    {
        blinking = true;

        Instantiate(fireBall, transform.position, transform.rotation);

    }

    public void Hurt()
    {
        if (!invincible)
            {

            StartCoroutine(invincibility());
            health -= 1;

            }


        if (health <= 0)
        {
            Instantiate(theEnd, transform.position, transform.rotation);
            Instantiate(cookieSprite, transform.position, transform.rotation);
            GameManager.instance.RestartGame();
            Destroy(gameObject);
        }
    }

    IEnumerator invincibility()
    {
        invincible = true;
        scurrying = true;
        AudioManager.instance.playSound(AudioManager.instance.enemyHit);

        Vector3 direction = (target.position - transform.position).normalized;

        if (direction.x < 0)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }


        mouseJump(-5f);
        yield return new WaitForSeconds(hurtTime/12);

        mouseJump(-5f);
        yield return new WaitForSeconds(hurtTime / 3);
       

        invincible = false;
        scurrying = false;

        yield break;

    }

    IEnumerator phaseCycler()
    {
        while (health > 0)
        {
            float move;
            if (health >= 3 && !scurrying)               //Mouse Phase 3
            {
                move = Random.Range(0, 3);

                if (move >= 1)
                {
                    mouseJump(1f);
                    yield return new WaitForSeconds(0.25f);
                    mouseJump(1f);
                }
                else
                {
                    mouseMagic();
                }


                yield return new WaitForSeconds(1.75f);
            }
            else if (health == 2)                        //Mouse Phase 2
            {
                move = Random.Range(0, 5);

                if (move >= 4)
                {
                    mouseJump(2.5f);
                    yield return new WaitForSeconds(0.4f);
                    mouseJump(1f);
                }
                else if(move == 3)
                {
                    mouseMagic();
                    yield return new WaitForSeconds(0.33f);
                    mouseMagic();
                }
                else if(move <= 2)
                {
                    mouseJump(1.5f);
                    mouseMagic();
                    mouseJump(1.5f);

                }

                yield return new WaitForSeconds(1.5f);

            }
            else if (health == 1)                       //Mouse Phase 1
            {
                move = Random.Range(0, 10);

                if (move >= 8)
                {
                    mouseJump(1f);
                    yield return new WaitForSeconds(0.25f);
                    mouseJump(1f);
                }
                else if (move >= 5 && move < 8)
                {
                    mouseJump(1f);
                    yield return new WaitForSeconds(0.25f);
                    mouseJump(1f);
                    yield return new WaitForSeconds(0.2f);
                    mouseMagic();
                }
                else if (move <= 2)
                {
                    mouseJump(1.5f);
                    mouseMagic();
                    yield return new WaitForSeconds(0.25f);
                    mouseJump(1.5f);
                }
                else if(move > 2 && move < 5)
                {
                    mouseMagic();
                    yield return new WaitForSeconds(0.33f);
                    mouseMagic();
                    yield return new WaitForSeconds(0.33f);
                    mouseMagic();

                }

                yield return new WaitForSeconds(1.0f);

            }

            yield return new WaitForSeconds(1);
        }

        yield break;
    }

}
