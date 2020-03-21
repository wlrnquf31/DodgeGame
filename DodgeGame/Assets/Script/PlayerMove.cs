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
        
        //if(Input.GetKeyDown(KeyCode.Escape))
        //{
        //    GameManager.instance.setScore(GameManager.instance.score + 100);
        //}

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.instance.GameStop();
        }
        

        
    }
}
