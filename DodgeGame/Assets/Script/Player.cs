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

    [SerializeField]
    private Joystick joystick = null;

    [System.NonSerialized]
    public float halfSizeX;
    [System.NonSerialized]
    public float halfSizeY;

    void Update()
    {
#if UNITY_STANDLONE || UNITY_EDITOR || UNITY_ANDROID
        if (Input.GetKeyDown(KeyCode.Escape) && hp > 0)
        {
            //위치 바꿔야함
            GameManager.instance.GamePlayToggle();
        }
#endif
        playerMove();
    }

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        Hp = 2;
        GetComponent<SpriteRenderer>().sprite = GameManager.instance.skin;

#if UNITY_ANDROID || UNITY_IOS
        joystick.gameObject.SetActive(true);
        if(DataManager.instance.Load().joystickIsLeft)
        {
            Vector3 reversePos = new Vector3(-(joystick.transform.position.x), joystick.transform.position.y, 0);
            joystick.transform.position = reversePos;
        }
#endif

        halfSizeX = (GetComponent<SpriteRenderer>().sprite.rect.size.x * gameObject.transform.localScale.x) * 0.01f * 0.5f;
        halfSizeY = (GetComponent<SpriteRenderer>().sprite.rect.size.y * gameObject.transform.localScale.y) * 0.01f * 0.5f;

        data = DataManager.instance.Load();
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
#if UNITY_ANDROID || UNITY_IOS
        if (joystick.Direction.x != 0)
        {
            transform.Translate(new Vector3(joystick.Direction.x * data.speed * Time.deltaTime, 0f, 0f));
        }
        if (joystick.Direction.y != 0)
        {
            transform.Translate(new Vector3(0f, joystick.Direction.y * data.speed * Time.deltaTime, 0f));
        }
#endif

#if UNITY_STANDALONE || UNITY_EDITOR
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * data.speed * Time.deltaTime, 0f, 0f));
        }
        if (Input.GetAxisRaw("Vertical") != 0)
        {
            transform.Translate(new Vector3(0f, Input.GetAxisRaw("Vertical") * data.speed * Time.deltaTime, 0f));
        }
#endif

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
