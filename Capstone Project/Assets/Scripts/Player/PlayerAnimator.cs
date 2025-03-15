using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    Animator am;
    PManager pm;
    SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        am = GetComponent<Animator>();
        pm = GetComponent<PManager>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //checks if player is moving in PManager
        if(pm.moveDir.x != 0 || pm.moveDir.y != 0){
            //Sets moving to true in animator
            am.SetBool("Move", true);
            SpriteDirectionCheck();
        } else{
            am.SetBool("Move", false);
        }
    }

    void SpriteDirectionCheck(){
        if(pm.lastHorizontalVector < 0){
            sr.flipX = false;
        }
        else{
            sr.flipX = true;
        }
    }
}
