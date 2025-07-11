using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject start;
    public int damage;
    public float range;

    private void Update()
    {
        if(Vector2.Distance(start.transform.position, transform.position) > range)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            IHitable hitable = collision.gameObject.GetComponent<IHitable>();
            if (hitable != null)
            {
                hitable.IHit(damage);
            }
            Destroy(gameObject); 
        }
    }
}
