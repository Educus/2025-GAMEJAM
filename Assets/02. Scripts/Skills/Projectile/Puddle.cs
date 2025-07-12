using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puddle : MonoBehaviour
{
    [SerializeField] private float tickInterval = 0.1f;
    [SerializeField] private float lifetime = 3f;
    [SerializeField] private int damage = 5;

    private Dictionary<Collider2D, float> damageTimers = new Dictionary<Collider2D, float>();

    private void Start()
    {
        // 3초 뒤에 자동 파괴
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) return;

        if (!damageTimers.ContainsKey(collision))
            damageTimers.Add(collision, 0f);

        damageTimers[collision] += Time.deltaTime;

        if (damageTimers[collision] >= tickInterval)
        {
            damageTimers[collision] = 0f;

            IHitable hit = collision.GetComponent<IHitable>();
            if (hit != null)
            {
                hit.IHit(damage);
                // Debug.Log($"{collision.name} hit for {damage}!");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (damageTimers.ContainsKey(collision))
        {
            damageTimers.Remove(collision);
        }
    }
}
