using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exp : MonoBehaviour
{
    public int exp;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerBuild.Instance.AddExp(exp);

            Destroy(gameObject);
        }
    }

    public void SetExp(int value)
    {
        exp = value;
    }
}
