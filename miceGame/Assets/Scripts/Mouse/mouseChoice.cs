using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseChoice : MonoBehaviour
{

    public string nextScene;

    public GameObject text1;
    public GameObject text2;
    public GameObject choice;

    private void Start()
    {
        StartCoroutine(cutScene());
    }


    IEnumerator cutScene()
    {
        Instantiate(text1);

        yield return new WaitForSeconds(4f);

        Instantiate(text2);

        yield return new WaitForSeconds(4f);


        Instantiate(choice);

    }
}
