using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cash : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(destoryCash());
    }

    IEnumerator destoryCash()
    {
        yield return new WaitForSeconds(6f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.CashHit();
            Destroy(gameObject);
        }
    }
}
