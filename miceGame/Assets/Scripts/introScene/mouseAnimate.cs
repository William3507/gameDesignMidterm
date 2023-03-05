using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseAnimate : MonoBehaviour
{

    Rigidbody2D rb2d;
    public SpriteRenderer mySpriteRenderer;
    public float animationFPS;

    public Sprite[] mouseSprite = new Sprite[2];

    float frameTimer;
    int currentFrame;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();

        StartCoroutine(cutScene());
    }

    private void FixedUpdate()
    {

    }

    IEnumerator cutScene()
    {
        rb2d.velocity = new Vector2(-3, 0);

        yield return new WaitForSeconds(1);

        rb2d.velocity = new Vector2(0, 0);

        yield return new WaitForSeconds(1.5f);

        rb2d.velocity = new Vector2(-2, 0);

        yield return new WaitForSeconds(2.5f);

        rb2d.velocity = new Vector2(0, 0);

        yield return new WaitForSeconds(1);

        rb2d.velocity = new Vector2(-2, 0);

        yield return new WaitForSeconds(1);

        rb2d.velocity = new Vector2(0, 0);

        mySpriteRenderer.sprite = mouseSprite[1];

        yield return new WaitForSeconds(1.75f);

        mySpriteRenderer.sprite = mouseSprite[0];

        mySpriteRenderer.flipX = true;

        rb2d.velocity = new Vector2(3.5f, 0);

        yield return new WaitForSeconds(7.5f);


        yield break;
    }


    public void walkingAnimation(Sprite[] spriteList)
    {
        frameTimer -= Time.deltaTime;
        if (frameTimer <= 0)
        {
            mySpriteRenderer.sprite = spriteList[currentFrame];
            frameTimer = 1 / animationFPS;
            currentFrame++;
            if (currentFrame >= spriteList.Length)
            {
                currentFrame = 0;
            }
        }
    }



}
