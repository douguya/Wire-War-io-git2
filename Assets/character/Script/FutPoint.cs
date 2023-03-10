using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FutPoint : MonoBehaviour
{

    public GameObject character;//本体への接続
    public bool Air=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        //接触したオブジェクトのタグが"Player"のとき
        if (other.CompareTag("floor"))
        {
            if (Air )
            {

                Debug.Log("666");
                //オブジェクトの色を赤に変更する
                character.GetComponent<CharactreContorol>().AirToIdle();
                character.GetComponent<CharactreContorol>().CanJump = true;
                Air = false;
            }
           
        }
    }
    void OnTriggerStay(Collider other)
    {
        //接触したオブジェクトのタグが"Player"のとき
        if (other.CompareTag("floor"))
        {
            //オブジェクトの色を赤に変更する
            if (Air==false)
            {
               
               
            }

        }
    }
    void OnTriggerExit(Collider other)
    {
        //接触したオブジェクトのタグが"Player"のとき
        if (other.CompareTag("floor"))
        {
            //オブジェクトの色を赤に変更する
            Air = true;
        }
    }

}
