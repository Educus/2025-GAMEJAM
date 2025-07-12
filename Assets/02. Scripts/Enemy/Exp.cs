using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exp : MonoBehaviour
{
    [HideInInspector] public int exp;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerBuild.Instance.AddExp(exp);

            Destroy(gameObject);
        }
    }
}
