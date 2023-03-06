using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static HealthManager instance;
    public int maxPlayerHealth;
    public bool isHurting = false;
    public float hurtTime = 1;


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
        if (PlayerData.playerHealth <= 0)
        {
            PlayerData.playerHealth = maxPlayerHealth;
        }
    }

    public IEnumerator Hurt()
    {
        if (!isHurting)
        {
            AudioManager.instance.playSound(AudioManager.instance.playerHit);
            isHurting = true;
            PlayerData.playerHealth--;
            UIHealthPanel.instance.SetLives(PlayerData.playerHealth);
            if (PlayerData.playerHealth <= 0)
            {
                AudioManager.instance.playSound(AudioManager.instance.playerDie);
                Die();
            }
            yield return new WaitForSeconds(hurtTime);
            isHurting = false;
        }
    }

    public void Die()
    {
        GameManager.instance.RestartGameAtCheckpoint(3);
    }
}
