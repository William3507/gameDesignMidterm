using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseBossFireball : MonoBehaviour
{

    [SerializeField] float moveSpeed = 5f;
    Transform target;
    Vector2 moveDirection;
    Vector3 scaling;

    public SpriteRenderer mySpriteRenderer;
    public float animationFPS;
    public Sprite[] fireball = new Sprite[3];
    Rigidbody2D rb2d;

    float frameTimer;
    int currentFrame;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();

    }

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        frameTimer = 0;
        currentFrame = 0;
    }


    // Update is called once per frame
    void Update()
    {

        if(target)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            float faceTowardsPlayer = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;

            rb2d.rotation = faceTowardsPlayer;
            moveDirection = direction;
        }
        

    }


    private void FixedUpdate()
    {
        if(target != null)
        {
            rb2d.velocity = new Vector2(moveDirection.x, moveDirection.y) * moveSpeed;
        }

        walkingAnimation(fireball);
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

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            StartCoroutine(squish(0.33f));
        }
        else if (col.gameObject.CompareTag("Water"))
        {

        }
        else
        {
            StartCoroutine(squish(0.75f));
        }
    }


    IEnumerator squish(float time)
    {

        for (int i = 0; i < time / 0.2f; i++)
        {
            moveSpeed /= 1.6f;

            scaling = transform.localScale;
            scaling.y /= 1.5f;


            transform.localScale = scaling;

            yield return new WaitForSeconds(0.2f);
        }

        Destroy(gameObject);
    }
        
    
}
