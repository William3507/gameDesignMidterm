using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knightAnimateFinalScene : MonoBehaviour
{
    Rigidbody2D rb2d;
    public SpriteRenderer mySpriteRenderer;
    public float animationFPS;
    public Sprite[] framesRight = new Sprite[4];

    public GameObject cheese;
    float frameTimer;
    int currentFrame;
    public string nextScene;

    public int toggle = -1;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();

        StartCoroutine(cutScene());
    }

    private void Update()
    {

        if (rb2d.velocity.x >= 0)
        {
            walkingAnimation(framesRight);
        }


        if (Input.GetKeyDown("a") || Input.GetKeyDown("d"))
        {
            toggle /= -1;
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            toggle += 4;
        }

    }

    IEnumerator cutScene()
    {

        yield return new WaitUntil(() => toggle == 3 && Input.GetKeyDown(KeyCode.Space));

        rb2d.velocity = new Vector2(2f, 0);

        yield return new WaitForSeconds(1.5f);

        rb2d.velocity = new Vector2(0, 0);

        Instantiate(cheese, transform.position + new Vector3(1.5f, 0.25f, 0), transform.rotation);
        
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
