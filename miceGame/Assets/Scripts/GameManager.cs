using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;
	public GameObject[] checkpoints;

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

    private void Update()
    {
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
	}
    public void RestartGameAtCheckpoint(float seconds)
	{
		StartCoroutine(LoadSceneAfterSeconds(seconds));
	}

	IEnumerator LoadSceneAfterSeconds(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void RestartGame()
    {
		StartCoroutine(RestartGameAfter(5));
    }
	private IEnumerator RestartGameAfter(int seconds)
    {
		yield return new WaitForSeconds(seconds);
		SceneManager.LoadScene("introScene");
	}
}
