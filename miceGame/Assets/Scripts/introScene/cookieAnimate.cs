using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cookieAnimate : MonoBehaviour
{

    Rigidbody2D rb2d;
    public SpriteRenderer mySpriteRenderer;
    public float animationFPS;
    public float lifeTime = 10;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();

        StartCoroutine(cutScene());
    }



    IEnumerator cutScene()
    {

        yield return new WaitForSeconds(lifeTime);

        Destroy(gameObject);
    }
}
