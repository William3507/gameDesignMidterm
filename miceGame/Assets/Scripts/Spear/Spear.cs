using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour
{

    public float speed = 1;
    public float lifeTime = 5;
    Vector2 direction = new Vector2();

    void Start()
    {
        direction = new Vector2(0, -1);
        direction.Normalize();
    }

	void Update()
    {
        transform.position = transform.position + new Vector3(direction.x, direction.y, 0) * speed * Time.deltaTime;

        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
