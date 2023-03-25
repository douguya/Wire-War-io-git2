using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{



    public enum BulletMode 
    {
        NormalShot=0,
        ChargeShot=1,

    }
    public  BulletMode bulletMode=0;



    public int[] Bullet_Speed;//弾速
    public int[] Ballet_Range;//射程距離
    public Vector3[] Bullet_Size;//弾のサイズ

    public Vector3 StartPosi;//発射時の位置

    public ParticleSystem Bullet_Particle;//着弾or自然消滅時に発動するパーティクル

    public MeshRenderer BulletMeshe;//パーティクル再生時に弾丸を非表示にするためのもの
    public SphereCollider Collider; //パーティクル再生時に当たり判定を削除するためのもの

    private bool Exprode=false;//爆発時の判定　弾丸を止めるのに使用　弾丸にパーティクルを付けているので、パーティクル再生時は球を止めなければいけないため

    // Start is called before the first frame update
    void Start()
    {
        StartPosi = transform.position;//一定距離で消すために発射位置を記憶


        transform.localScale = Bullet_Size[(int)bulletMode];

    
      
    }

    // Update is called once per frame
    void Update()
    {
        if (Exprode==false)//爆発しない限り前進
        {
            this.transform.Translate(Vector3.right * Time.deltaTime * Bullet_Speed[(int)bulletMode]);//指定した弾速で飛ばす

        }
     

        if (Vector3.Distance(transform.position, StartPosi)> Ballet_Range[(int)bulletMode])//発射したところから離れたら終了
        {
           
            StartCoroutine("EndBullet");//弾丸の終了処分　着弾パーティクルの後削除
            Debug.Log(111);
        }
      
    }

    void OnTriggerEnter(Collider other)
    {

        StartCoroutine("EndBullet");//弾丸の終了処分　着弾パーティクルの後削除
        Debug.Log(other.gameObject.name);
    }



    IEnumerator EndBullet()//弾丸の終了処分　着弾パーティクルの後削除
    {
      
        Exprode = true; //爆発　弾丸の動きを停止
        Bullet_Particle.Play();//パーティクル再生
        BulletMeshe.enabled = false;//表示消去
        Collider.enabled = false;//当たり判定消去

        yield return new WaitForSeconds(0.2f);
        Destroy(this.gameObject);//弾丸の消去

    }

}
