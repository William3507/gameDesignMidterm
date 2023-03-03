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
}
