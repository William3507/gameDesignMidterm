using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioClip jump;
    public AudioClip playerHit;
    public AudioClip enemyHit;
    public AudioClip fireball;
    public AudioClip playerDie;
    public AudioClip cheeseGet;

    private AudioSource audioSource;

    void Awake()
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

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void playSound (AudioClip sound)
    {
        audioSource.PlayOneShot(sound);
    }
}
