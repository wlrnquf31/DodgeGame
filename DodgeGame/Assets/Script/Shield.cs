using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private GameObject player;

    private void Start()
    {
        player = gameObject.transform.parent.gameObject;
    }

    void Update()
    {
        orbitRotation();
    }

    private void orbitRotation()
    {
        transform.RotateAround(player.transform.localPosition, Vector3.forward, DataManager.instance.Load().shieldSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }

    private void ShieldHit()
    {
        RandomHpDrop();
        GameManager.instance.ScoreUp();
        SoundManager.instance.PlayHitShieldSound();
    }

    private void RandomHpDrop()
    {
        float randomPoint = Random.value;
        if(DataManager.instance.Load().hpCureChance > randomPoint)
        {
            player.GetComponent<Player>().Hp += 1;
            UiManager.instance.HpUiController();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            ShieldHit();
            Destroy(collision.gameObject);
        }
    }
}
