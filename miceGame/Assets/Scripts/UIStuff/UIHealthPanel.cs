using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthPanel : MonoBehaviour
{
    public static UIHealthPanel instance;

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

    /// <summary>
    /// Updates the hearts by enabling/disabling them depending of the number of lives.
    /// </summary>
    /// <param name="lives">Lives.</param>
    public void SetLives(int lives)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < lives)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
}