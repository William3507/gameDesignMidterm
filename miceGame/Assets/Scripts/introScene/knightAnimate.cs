using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class knightAnimate : MonoBehaviour
{

    Rigidbody2D rb2d;
    public SpriteRenderer mySpriteRenderer;
    public float animationFPS;
    public Sprite[] framesRight = new Sprite[4];
    public Sprite[] idle = new Sprite[2];

    public GameObject mouse;
    float frameTimer;
    int currentFrame;
    public string nextScene = "Mouse Castle";

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();

        StartCoroutine(cutScene());
    }

    private void FixedUpdate()
    {
        if(rb2d.velocity.x <= 0)
        {
            walkingAnimation(idle);
        }
        else if(rb2d.velocity.x >= 0)
        {
            walkingAnimation(framesRight);
        }
        
    }

    IEnumerator cutScene()
    {

        yield return new WaitForSeconds(2f);

        Vector3 spawnPosition = new Vector3(9.32f, -3.9f, 0);

        Instantiate(mouse, spawnPosition, Quaternion.identity);


        yield return new WaitForSeconds(12f);

        rb2d.velocity = new Vector2(3, 0);

        yield return new WaitForSeconds(5);

        SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
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
