using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthPanel : MonoBehaviour
{
    public static UIHealthPanel instance;

    public float flashFps = 5;
    private bool flash;

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
        SetLives(PlayerData.playerHealth);
    }

    public void SetLives(int lives)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i == lives)
            {
                StartCoroutine(FlashLife(i));
            }
            else if (i < lives)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    IEnumerator FlashLife(int lostHeart)
    {
        while (HealthManager.instance.isHurting)
        {
            if (flash)
            {
                hearts[lostHeart].color = Color.grey;
            }
            else
            {
                hearts[lostHeart].color = Color.white;
            }
            flash = !flash;
            yield return new WaitForSeconds(1/flashFps);
        }
        hearts[lostHeart].enabled = false;
    }
}
