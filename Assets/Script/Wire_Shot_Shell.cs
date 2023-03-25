using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire_Shot_Shell : MonoBehaviour
{
    public float Shell_Speed = 30;//弾速
    public Vector3 targetPosition;//目標地点
                                  // Start is called before the first frame update
    public GameObject Player;

    private bool Shell_Landing=false;



    public ParticleSystem Shell_Particle;//着弾時に発動するパーティクル


    private bool Exprode = false;//爆発時の判定　弾丸を止めるのに使用　弾丸にパーティクルを付けているので、パーティクル再生時は球を止めなければいけないため
    private void Awake()
    {
      
    }
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {



        //  this.transform.Translate(Vector3.right * Time.deltaTime * Shell_Speed);//指定した弾速で飛ばす

        if (Shell_Landing==false)//着弾するまで
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Shell_Speed * Time.deltaTime);//Bulletとは違う処理なので分化  発射したPlayerの子オブジェクトとして保存するので、Playerの向きや位置に干渉されない飛ばし方をする　
        }
      

    


        //オブジェクトに当たった時にした方がいいのでは？
        //弾丸とプレイヤーには当たらないようにする？

    }
  
    void OnTriggerEnter(Collider other)//ワイヤー着弾
    {
        if (Shell_Landing == false)
        {
            Shell_Landing = true;//Shellスクリプト内着弾判定
            Player.GetComponent<Gun_Manager>().Wire_Pull = true;//ワイヤー牽引
            Shell_Particle.Play();//パーティクル再生
        }
    
    }
}
