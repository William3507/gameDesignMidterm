using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneTransfer : MonoBehaviour
{

    public string fightScene;
    public string cheeseTalk;


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (PlayerData.hasCheese)
            {
                SceneManager.LoadScene(cheeseTalk, LoadSceneMode.Single);
            }
            else
            {
                SceneManager.LoadScene(fightScene, LoadSceneMode.Single);

            }
        }
    }
}
