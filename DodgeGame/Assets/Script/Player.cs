using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataInfo;

public class Player : MonoBehaviour
{

    public readonly int MAX_HP = 2;
    private int hp;
    public int Hp
    {
        get
        {
            return hp;
        }
        set
        {
            hp = value;
            if (hp > MAX_HP)
            {
                hp = MAX_HP;
            }
        }
    }
    public bool isGodMode = false;
    public bool isOneIgnore = false;
    private GameData data = new GameData();

    private float halfSizeX;
    private float halfSizeY;

    void Update()
    {
        playerMove();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //위치 바꿔야함
            GameManager.instance.GamePlayToggle();
        }
    }

    private void Start()
    {
        Init();
        data = DataManager.instance.Load();
    }

    public void Init()
    {
        Hp = 2;
        GetComponent<SpriteRenderer>().sprite = GameManager.instance.skin;

        halfSizeX = (GetComponent<SpriteRenderer>().sprite.rect.size.x * gameObject.transform.localScale.x) * 0.01f * 0.5f;
        halfSizeY = (GetComponent<SpriteRenderer>().sprite.rect.size.y * gameObject.transform.localScale.y) * 0.01f * 0.5f;

        //GameManager.instance.player = this;
    }

    private void PlayerHit()
    {
        Hp -= 1;
        UiManager.instance.HpUiController();
        SoundManager.instance.PlayHitCharacterSound();
        if (Hp < 1)
        {
            GameManager.instance.GameOver();
        }
    }

    private void playerMove()
    {
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * data.speed * Time.deltaTime, 0f, 0f));
        }
        if (Input.GetAxisRaw("Vertical") != 0)
        {
            transform.Translate(new Vector3(0f, Input.GetAxisRaw("Vertical") * data.speed * Time.deltaTime, 0f));
        }

        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);

        if (pos.x < halfSizeX ) pos.x = halfSizeX;
        if (pos.x > 1f - halfSizeX) pos.x = 1f - halfSizeX;
        if (pos.y < halfSizeY) pos.y = halfSizeY;
        if (pos.y > 1f - halfSizeY) pos.y = 1f - halfSizeY;

        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            if(!isGodMode)
            {
                if(!isOneIgnore)
                {
                    PlayerHit();
                }
                else
                {
                    isOneIgnore = false;
                }
            }
            Destroy(collision.gameObject);
        }
    }
}
