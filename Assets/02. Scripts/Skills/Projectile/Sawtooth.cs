using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sawtooth : MonoBehaviour, IHitable
{
    [SerializeField] private GameObject bulletPrefab;
    [HideInInspector] public GameObject target;
    [HideInInspector] public int damage; 
    [HideInInspector] public bool ultmit = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            IHitable hitable = collision.gameObject.GetComponent<IHitable>();
            if (hitable != null)
            {
                hitable.IHit(damage);
            }
        }
    }

    public void IHit(int damage)
    {
        if (!ultmit) return;

        GameObject bullet = Instantiate(bulletPrefab, transform.position + new Vector3(0, 1.6f), Quaternion.identity);
        bullet.GetComponent<Bullet>().start = gameObject;
        bullet.GetComponent<Bullet>().damage = 5;
        bullet.GetComponent<Bullet>().range = 10f;

        Vector3 direction = (target.transform.position - bullet.transform.position).normalized;

        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        if (rigid != null)
        {
            rigid.velocity = direction * 5f;
        }
        else
        {
            bullet.transform.forward = direction;
        }
    }
}
