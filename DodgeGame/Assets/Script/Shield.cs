using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private GameObject Player;

    private void Start()
    {
        Player = gameObject.transform.parent.gameObject;
    }

    void Update()
    {
        orbitRotation();
    }

    private void orbitRotation()
    {
        transform.RotateAround(Player.transform.position, Vector3.forward, DataManager.instance.Load().shieldSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }

    private void ShieldHit()
    {
        GameManager.instance.ScoreUp();
        SoundManager.instance.PlayHitShieldSound();
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
