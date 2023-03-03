using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    public LayerMask platforms;
    public bool grounded;
    public float lengthRay;
    private bool invincible = false;

    public float health = 3;


    protected Rigidbody2D rb2d;
    protected SpriteRenderer spriteRenderer;
    


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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

        for (int i = 0; i < time / 0.4f; i++)
        {
            spriteRenderer.color = Color.grey;
            yield return new WaitForSeconds(0.2f);
            spriteRenderer.color = Color.magenta;
            yield return new WaitForSeconds(0.2f);
        }

        invincible = false;

    }
}
