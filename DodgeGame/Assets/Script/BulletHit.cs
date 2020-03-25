using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHit : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine("destoryBullet");
    }

    IEnumerator destoryBullet()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Shield"))
        {
            GameManager.instance.AddScore(100);
            Destroy(gameObject);
        }
        else if(collision.CompareTag("Player"))
        {
            GameManager.instance.AddHp(-1);
            Destroy(gameObject);
        }
    }
}
