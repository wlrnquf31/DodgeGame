using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBullet : MonoBehaviour
{
    public GameObject[] area;

    public GameObject bullet;

    public GameObject player;

    private float spawnTime = 0.7f;

    private void Start()
    {
        StartCoroutine("spawnBullet");
    }

    IEnumerator spawnBullet()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnTime);
        while(true)
        {
            if(spawnTime > 0.3f)
            {
                spawnTime -= Time.deltaTime;
                wait = new WaitForSeconds(spawnTime);
            }
            createBullet();
            yield return wait;
        }
    }

    private void createBullet()
    {
        int index = Random.Range(0, 4);

        Vector3 tmpPos = Vector3.zero;
        tmpPos.x = Random.Range(area[index].GetComponent<Collider2D>().bounds.min.x, area[index].GetComponent<Collider2D>().bounds.max.x);
        tmpPos.y = Random.Range(area[index].GetComponent<Collider2D>().bounds.min.y, area[index].GetComponent<Collider2D>().bounds.max.y);
        
        GameObject newBullet = Instantiate(bullet, tmpPos, Quaternion.identity);
        shotBullet(newBullet);
    }

    private void shotBullet(GameObject createdBullet)
    {
        createdBullet.GetComponent<Rigidbody2D>().velocity = (player.transform.position - createdBullet.transform.position) / 2;
    }
}
