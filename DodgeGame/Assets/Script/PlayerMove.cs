using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float Speed;

    void Update()
    {
        if(Input.GetAxisRaw("Horizontal") != 0)
        {
            transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * Speed * Time.deltaTime, 0f, 0f));
        }
        if (Input.GetAxisRaw("Vertical") != 0)
        {
            transform.Translate(new Vector3(0f, Input.GetAxisRaw("Vertical") * Speed * Time.deltaTime, 0f));
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.instance.GameStop();
        }

        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);

        if (pos.x < 0.03f) pos.x = 0.03f;
        if (pos.x > 0.97f) pos.x = 0.97f;
        if (pos.y < 0.03f) pos.y = 0.03f;
        if (pos.y > 0.97f) pos.y = 0.97f;

        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }
}
