using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarImage : MonoBehaviour
{
    public GameManagerScript gm;
    public string tColor;

    public Animator anim;

    void Awake()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        
    }
    // Update is called once per frame
    void Update()
    {
        tColor = gm.curPlayer;
        SetAvatar();
    }

    public void SetAvatar()
    {
        if (tColor == "blue")
        {
            DisableAvatars();
            anim.SetBool("isBlue", true);
        }
        else if (tColor == "yellow")
        {
            DisableAvatars();
            anim.SetBool("isYellow", true);
        }
        else if (tColor == "green")
        {
            DisableAvatars();
            anim.SetBool("isGreen", true);
        }
        else if (tColor == "red")
        {
            DisableAvatars();
            anim.SetBool("isRed", true);
        }
    }

    public void DisableAvatars()
    {
        anim.SetBool("isBlue", false);
        anim.SetBool("isYellow", false);
        anim.SetBool("isRed", false);
        anim.SetBool("isGreen", false);
    }
}
