using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthPanel : MonoBehaviour
{
    public static UIHealthPanel instance;

    public float flashFps = 5;
    private bool flash;
    private float hurtTime = 1;

    [SerializeField] Image[] hearts;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(instance.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void SetLives(int lives)
    {
        for (int i = 0; i < hearts.Length; i++)
        {

            if (i < lives - 1)
            {
                hearts[i].enabled = true;
            }
            else if (i == lives - 1)
            {
                StartCoroutine(FlashLife(i));
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    IEnumerator FlashLife(int lostHeart)
    {
        while (hurtTime > 0)
        {
            hurtTime -= hurtTime / flashFps;
            if (flash)
            {
                hearts[lostHeart].color = Color.blue;
            }
            else
            {
                hearts[lostHeart].color = Color.white;
            }
            yield return new WaitForSeconds(hurtTime / flashFps);
        }
        hearts[lostHeart].enabled = false;
    }
}
