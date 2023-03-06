using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mouseChoice : MonoBehaviour
{

    public string fightScene;

    public GameObject text1;
    public GameObject text2;
    public GameObject choice;
    public GameObject square;
    public GameObject fine;
    public GameObject cookieSprite;
    public GameObject theEnd;

    Rigidbody2D rb2d;

    public int toggle = -1;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        StartCoroutine(cutScene());
    }

    private void Update()
    {
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
        Instantiate(text1);

        yield return new WaitForSeconds(4f);

        Instantiate(text2);

        yield return new WaitForSeconds(4f);

        Instantiate(choice);
        Instantiate(square);

        yield return new WaitUntil(() => toggle >= 2);

        if(toggle == 5)
        {
            Instantiate(fine);
            rb2d.velocity = new Vector2(4,0);
            yield return new WaitForSeconds(3f);

            SceneManager.LoadScene(fightScene, LoadSceneMode.Single);
        }
        else
        {

            rb2d.velocity = new Vector2(-2, 0);
            yield return new WaitForSeconds(1.66f);

            rb2d.velocity = new Vector2(0, 0);
            Instantiate(cookieSprite, transform.position + new Vector3(-1.5f, 0.25f, 0), transform.rotation);

            yield return new WaitForSeconds(1.5f);
            Instantiate(theEnd);
        }

    }
}
