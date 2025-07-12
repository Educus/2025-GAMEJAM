using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Enemy_projectile : MonoBehaviour
{
    public int atk;

    private void Start()
    {
        GetComponent<Rigidbody2D>().gravityScale = 0;
        GetComponent<CapsuleCollider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<IHitable>().IHit(atk);

            Destroy(gameObject);
        }
    }

    private float time = 10f;
    private void Update()
    {
        time -= Time.deltaTime;

        if (time < 0) Destroy(gameObject);
    }
}
