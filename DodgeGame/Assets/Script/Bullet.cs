using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //private void OnEnable()
    //{
    //    StartCoroutine(ReturnBullet());
    //}

    //private void OnDisable()
    //{
    //    StopAllCoroutines();
    //}
    //IEnumerator ReturnBullet()
    //{
    //    yield return new WaitForSeconds(5f);
    //    ObjectPool.instance.ReturnObject(this);
    //}

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("BackGround"))
        {
            ReturnThis();
        }
    }

    public void ReturnThis()
    {
        ObjectPool.instance.ReturnObject(gameObject);
    }
}
