using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    public LayerMask platforms;
    public bool grounded;
    private bool invulnerable;

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
    void Update()
    {
        
    }



    private void Hurt(Vector3 impactDirection)
    { 
        if (impactDirection.y > 0.0f && !invulnerable)
            {
                StartCoroutine(invulnerability(1.2f));
                health -= 1;

        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        playerController controller = collision.gameObject.GetComponent<playerController>();
        if (controller != null)
        {
            Vector3 impactDirection = collision.gameObject.transform.position - transform.position;
            Hurt(impactDirection);
        }
    }

    IEnumerator invulnerability(float time)
    {
        invulnerable = true;

        for (int i = 0; i < time / 0.4f; i++)
        {
            spriteRenderer.color = Color.grey;
            yield return new WaitForSeconds(0.2f);
            spriteRenderer.color = Color.magenta;
            yield return new WaitForSeconds(0.2f);
        }

        invulnerable = false;

    }
}
