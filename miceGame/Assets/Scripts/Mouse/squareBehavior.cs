using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class squareBehavior : MonoBehaviour
{
    Rigidbody2D rb2d;
    public int toggle = -1;
    public Renderer rend;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rend = GetComponent<Renderer>();
        StartCoroutine(waitUntil());
    }


    private void Update()
    {
        if (Input.GetKeyDown("a") || Input.GetKeyDown("d"))
        {
            toggle /= -1;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Destroy(gameObject);
        }


        if (toggle == -1)
        {
            rb2d.position = new Vector2(-9.32f, 0.66f);
        }
        else
        {
            rb2d.position = new Vector2(-7.5f, 0.66f);
        }
    }

    IEnumerator waitUntil()
    {
        rend.enabled = false;
        yield return new WaitForSeconds(8f);

        rend.enabled = true;
    }
}
