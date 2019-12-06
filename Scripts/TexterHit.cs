using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TexterHit : MonoBehaviour
{
    private Animator anim;
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            anim.SetTrigger("hit");
        }
    }
}
