using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorFollow : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        Cursor.visible = false;
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = cursorPos;
        if (Input.GetMouseButton(0))
        {
            anim.SetTrigger("attack");
        }
        else 
        {
            anim.SetTrigger("hold");
        }
    }
}
