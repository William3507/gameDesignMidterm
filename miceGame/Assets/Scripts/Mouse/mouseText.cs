using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseText : MonoBehaviour
{

    public float lifeTime = 10;

    private void Start()
    {

        StartCoroutine(cutScene());
    }



    IEnumerator cutScene()
    {

        yield return new WaitForSeconds(lifeTime);

        Destroy(gameObject);
    }
}
