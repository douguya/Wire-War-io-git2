using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator anim;  //Animatorをanimという変数で定義する


    public void JumpEnd()//ジャンプ終了後処理
    {
       
        anim.SetBool("MidAir", true);
      
    }
    public void AirEnd()//ジャンプ終了後処理
    {
        anim.SetBool("MidAir", false);
    }
    public void PostJump_processing()//ジャンプ直後処理
    {

        anim.SetBool("Jump", false);
    }
}
