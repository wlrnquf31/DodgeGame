using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
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
}
