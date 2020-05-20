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

    private GameData data = new GameData();

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
        data = DataManager.instance.Load();
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

        if (pos.x < 0.03f) pos.x = 0.03f;
        if (pos.x > 0.97f) pos.x = 0.97f;
        if (pos.y < 0.03f) pos.y = 0.03f;
        if (pos.y > 0.97f) pos.y = 0.97f;

        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }
}
