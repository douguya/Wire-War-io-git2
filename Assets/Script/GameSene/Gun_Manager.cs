using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Manager : MonoBehaviour
{


    public Camera CharacterCamera;//カメラのオブジェクト

    [System.NonSerialized]
    public GS_UIManager uimanager;//UIのマネージャ



    Ray Arm_Ray;//本とはGunRayだが、オブジェクトとわかりやすく分けるためにArmにする
    RaycastHit hit;//射程内判定ようのRaycastHit




    public GameObject Gun;　//銃のオブジェクト
    public GameObject BulletPoint;//銃弾の生成点
    public GameObject Bullet;//弾丸のプレハブ





    [Header("エネルギー　射撃に関する部分を除く--------")]



    public float Energy;//銃を撃つためのエネルギー　とリマ１００


    public int EnergyMax;//エネルギー最大値
    public int EnergyMini;//エネルギー最小値


    public float CooldownTime;//射撃からエネルギー充填開始にかかる時間
    public float RechargeTime;//エネルギーのフル充填にかかる時間





    [Header("射撃に関する値--------")]
    public int ShotCost;   //通常射撃にかかるコスト                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         
    public int ChargeCost; //強化射撃にかかるコスト    



    public float ChageTimer = 0;//銃のチャージ状態のタイマー
    public float Required_ChageTime;//銃のチャージにかかる時間


    public int[] Vewing_distance;//銃のRayの距離　発射時の有効射程距離

    public float ShotTimer=0;//射撃後の時間経過のタイマー



    public enum BulletCharge
    {
        NoCharge = 0,//通常弾に加え、ワイヤーもNoChargeとしての値を使う
        FullCharge = 1,

    }
    public BulletCharge bulletCharge = 0;









    [Header("--------------以下ワイヤーに関するもの---------------")]



    public bool Wire_Injectioning = false;//射出中
    private bool Within_Range = false;//射程内
    public bool Wire_Pull = false;//ワイヤー牽引中
    public bool Wire_farst = false;//着弾後一度だけ作動させるためのもの
    public bool Wire_Distance_far = false;//ワイヤー着弾地点との距離が広がっている

    [System.NonSerialized]
    public LineRenderer Wire_Line;//ワイヤーを表現するラインレンダラー　
    public GameObject Wire_shot_shell;//ワイヤーショットのプレハブ
    private GameObject Generated_Wire_shot;//ワイヤーの先端のオブジェクト プレハブを召喚したもの
    public float[] Wire_to_PlayerDistance = new float[3];//ワイヤーとの距離の格納

    [System.NonSerialized]
    public Rigidbody Rb;//　ワイヤー牽引時に使う物理演算オブジェクト

    [Header("以下ワイヤーのステータス")]
    public int Wire_force;//牽引の力　
    private Vector3 Add_Wire_force;//Y方向の牽引力調整用　実際の牽引時に使用

    private float Wire_TimeLimit = 5;// ワイヤー自切時間　タイムリミット
    public float Wire_Timer;// ワイヤータイマー　自切判断に使う


    void Start()
    {
      
        Energy = 0;//エネルギー完全充填
    }

    // Update is called once per frame
    void Update()
    {
        TargetSift();//銃から出たRayの衝突位置に照準を合せる


        Energy_ReCharge();//エネルギーのチャージ
    }


    public void Energy_ReCharge()
    {

        ShotTimer += Time.deltaTime;//ショット後のタイマーの計測
        if (ShotTimer> CooldownTime&&ChageTimer==0)//ショット後の時間が閾値を超えたら  また　強化ショット非チャージ中なら
        {
            Energy += (Time.deltaTime / RechargeTime) * EnergyMax;//経過時間をフルチャージにかかる時間で割ってエネルギー最大値にかけることでエネルギーをチャージ
        }
      



        EnergyAdjust();////エネルギーを上下限内にそろえる

        uimanager.Energy_UI_Conversion(Energy, EnergyMini, EnergyMax);//UIにエネルギーを反映


        //エネルギーの予測消費ラインをUIに反映
        if (bulletCharge == BulletCharge.NoCharge)  //チャージ状態でない
        {
            uimanager.Energy__Forecast_Lines_UI_Conversion(Energy-ShotCost, EnergyMini, EnergyMax);//エネルギ-の予想消費ラインをUIに反映

        }
        if (bulletCharge == BulletCharge.FullCharge)//　チャージ状態　ほんとはElseでいいんだけど 可読性のために分ける
        {

            uimanager.Energy__Forecast_Lines_UI_Conversion(Energy-ChargeCost, EnergyMini, EnergyMax);//エネルギ-の予想消費ラインをUIに反映
        }


    }


    public void EnergyAdjust()//エネルギーを上下限内にそろえる
    {

        if (Energy < EnergyMini)//範囲を超えていたら強制的に合わせる
        {
            Energy = EnergyMini;
        }
        else if (Energy > EnergyMax)
        {
            Energy = EnergyMax;
        }



    }

    void LateUpdate()
    {
        
        Wire();
        // Aim();//照準
    }
    public void TargetSift()
    {

        Arm_Ray = new Ray(Gun.transform.position, Gun.transform.right); //銃(腕)からRayを飛ばす

        Debug.Log((int)bulletCharge);

        Debug.DrawRay(Arm_Ray.origin, Arm_Ray.direction * Vewing_distance[(int)bulletCharge], Color.blue);


        if (Physics.Raycast(Arm_Ray, out hit, Vewing_distance[(int)bulletCharge]))//Rayがオブジェクトに当たっている場合　要するに射程内
        {
            Within_Range = true;//射程内

            uimanager.TargetSift_Position(CharacterCamera.WorldToScreenPoint(hit.point),true);//Rayとオブジェクトの交点に照準UIを合わせる

          
        }
        else //要するに射程外
        {
            Within_Range = false;//射程外
            uimanager.TargetSift_Position(CharacterCamera.WorldToScreenPoint(Arm_Ray.GetPoint(Vewing_distance[(int)bulletCharge])), false);//Rayの(bulletCharge)地点にオブジェクトの交点に照準UIを合わせる
        }





    }



    public void BulletShot()
    {
        if (bulletCharge == BulletCharge.NoCharge)
        {
            if (Energy >= ShotCost)//エネルギーが必要量を満たしていたら
            {
                ShotTimer = 0;
                Energy -= ShotCost;

                Bullet.GetComponent<Bullet>().bulletMode = (Bullet.BulletMode)bulletCharge;//弾のモードをチャージ状態と同期する       

                Instantiate(Bullet, BulletPoint.transform.position, Gun.transform.rotation);//弾丸のインスタンスを発射ポイントから発射
                ChargeReset();//チャージリセット　中途半端なチャージで撃った場合のリセットも含む
            }
        }

        if (bulletCharge == BulletCharge.FullCharge)//ほんとはElseでいいんだけど 可読性のために分ける
        {
            if (Energy >= ChargeCost)//エネルギーが必要量を満たしていたら
            {
                ShotTimer = 0;
                Energy -= ChargeCost;

                Bullet.GetComponent<Bullet>().bulletMode = (Bullet.BulletMode)bulletCharge;//弾のモードをチャージ状態と同期する       

                Instantiate(Bullet, BulletPoint.transform.position, Gun.transform.rotation);//弾丸のインスタンスを発射ポイントから発射
                ChargeReset();//チャージリセット　中途半端なチャージで撃った場合のリセットも含む
            }
        }
    }





    public void Charge()
    {
        if (Energy>=ChargeCost)
        {
            ChageTimer += Time.deltaTime;//チャージ時間を計測
            if (Required_ChageTime <= ChageTimer)//チャージ完了
            {
                bulletCharge = BulletCharge.FullCharge;//チャージ状態に移行
                uimanager.TargetSift_Charge(true);//照準をチャージ済みのものに変化
            }
           
        }
       
    }
    public void ChargeReset()
    {
        
        ChageTimer = 0;//チャージ時間リセット
       
        bulletCharge = BulletCharge.NoCharge;//非チャージ状態に移行
        uimanager.TargetSift_Charge(false);//照準をチャージ済みのものに変化
    }

    public void Wire_Shot()
    {
        ChargeReset();//チャージリセット　ワイヤー射出と同時にチャージをリセット

        if (Wire_Injectioning)//ワイヤー射出中
        {
             Wire_Cut();//ワイヤーの切り離し　　　とにもかくにもワイヤーをぶった切る　　以前の先端部分が何かの事故で残っていた時のケアと、　もう一度のクリックによるワイヤーの切り離しを兼ねる


        }
        else if(Within_Range)//ワイヤー射出中出ない場合
        {
            Generated_Wire_shot = (GameObject)Instantiate(Wire_shot_shell, BulletPoint.transform.position, Gun.transform.rotation);//ワイヤーの先端部の生成と格納
            Generated_Wire_shot.GetComponent<Wire_Shot_Shell>().targetPosition = hit.point;//ワイヤーの先端部の目標地点をRayの着弾地点へ設定

            Generated_Wire_shot.GetComponent<Wire_Shot_Shell>().Player = this.gameObject;

            Wire_Injectioning = true;//ワイヤー射出中
            Wire_farst = true;//ワイヤー着弾後に一度だけ作動させるためのもの
            Wire_Line.enabled = true;//ラインレンダラーを有効にする
        }

    }

    
    


// Start is called before the first frame update
private void Wire()//ワイヤーに関するもの 
{






    if (Wire_Injectioning)//ワイヤー射出中
    {
        Wire_Line.SetPosition(0, BulletPoint.transform.position);//ワイヤーの始点を設定(ワイヤーの射出ポイント)
        Wire_Line.SetPosition(1, Generated_Wire_shot.transform.position);//ワイヤーの終点を設定(ワイヤーの先端)

        if (Wire_Pull)//ワイヤー着弾=牽引中
        {

            if (Wire_farst)//けん引開始時の初回処理
            {
                Rb.velocity = new Vector3(Rb.velocity.x, Rb.velocity.y / 2, Rb.velocity.z);//落下or上昇速度の減衰　どの程度減衰するべきかは一考の余地あり　　　　この2は果たしてわざわざ変数にする必要があるのか…？　(マジックナンバー)

                Wire_to_PlayerDistance[0] = Vector3.Distance(transform.position, Generated_Wire_shot.transform.position);//プレイヤーとワイヤーの先端との距離
                Wire_to_PlayerDistance[2] = Wire_to_PlayerDistance[0];//ワイヤーとの距離を更新しないところに保管　あとで使う
                Wire_farst = false;//初回処理終了


            }

            //以下牽引処理

            Add_Wire_force = (Generated_Wire_shot.transform.position - Gun.transform.position).normalized * Wire_force;
            Rb.AddForce(new Vector3(Add_Wire_force.x, Add_Wire_force.y * 1.3f, Add_Wire_force.z));//目標地点へプレイヤーを牽引 重力にあらがうためにY方向へ強めに設定　これはさすがにあとで変数にしとく(マジックナンバー)
            Wire_to_PlayerDistance[1] = Vector3.Distance(transform.position, Generated_Wire_shot.transform.position);//プレイヤーとワイヤーの先端との距離


            //牽引処理ここまで




            if (Wire_to_PlayerDistance[0] != Wire_to_PlayerDistance[1])//ワイヤーとの距離に変化があった時
            {
                if (Wire_to_PlayerDistance[0] <= Wire_to_PlayerDistance[1])//ワイヤーとの距離が縮まっている時
                {
                    Wire_Distance_far = true;//ワイヤーから遠のいている
                }
                else
                {
                    Wire_Distance_far = false;//ワイヤーに近づいている
                }

                Wire_to_PlayerDistance[0] = Vector3.Distance(transform.position, Generated_Wire_shot.transform.position);//ワイヤーとの距離を再計測

            }

            //---------------------------------------以下終了条件------------------------------------------



            //以下 ワイヤー地点が自分の後ろにあり、自分がワイヤーから遠のいていて、なおかつその距離が一定以上の場合、ワイヤーを自切
            if (Vector3.Dot(Generated_Wire_shot.transform.position - transform.position, transform.forward) <= 0.3)//内積による前後判定　後ろの場合　これはさすがにあとで変数にしとく(マジックナンバー)
            {

                if (Wire_Distance_far == true)//距離が離れている場合
                {

                    if (Wire_to_PlayerDistance[0] >= 5 * Map(Wire_to_PlayerDistance[2], 0, Vewing_distance[0], 1f, 1.3f))//ワイヤーの先端部との距離が一定を超えたらワイヤーを自切   特殊処理　一考のよりチアリ　これはさすがにあとで変数にしとく(マジックナンバー)
                    {

                        if (Wire_farst == false)//ワイヤー牽引中かどうか　最低限の処理が済んでいるなら
                        {
                            Wire_Cut();//ワイヤー時節
                        }
                    }
                }


            }


            if (Wire_to_PlayerDistance[0] < 5)//ワイヤー着弾地点としばらく一定以下の距離だったら
            {
                Wire_Timer += Time.deltaTime;
                if (Wire_Timer >= Wire_TimeLimit * Map(Wire_to_PlayerDistance[2], 0, Vewing_distance[0], 0.7f, 1.1f)) //ワイヤーの距離が一定以下の時がしばらく続いたら自切るする　 特殊処理　一考のよりチアリ　これはさすがにあとで変数にしとく(マジックナンバー)
                {
                    if (Wire_farst == false)//ワイヤー牽引中かどうか　最低限の処理が済んでいるなら
                    {
                        Wire_Cut();
                    }
                }
            }
            else
            {
                Wire_Timer = 0;
            }



        }



    }


}

public void Wire_Cut()//ワイヤーの切り離し
{


    Destroy(Generated_Wire_shot);//ワイヤー先端部の消去
    Wire_Timer = 0;
    Wire_Injectioning = false;//ワイヤー射出中以下全てのフラグの初期化

    Wire_Pull = false;//ワイヤー着弾=牽引中


    Wire_farst = false;//けん引開始時の初回処理
    Wire_Distance_far = false;

    Wire_Line.enabled = false;


}

    public float Map(float value, float R_min, float R_max, float V_min, float V_max)
    {
        /*
        float Rrenge = (R_max-R_min);
        float convartR = (value-R_min);
        float Rratio = Rrenge/ convartR;

        float Vrenge = (V_max-V_min);
        float VDelta = (Vrenge/Rratio);


        */

        return V_min + (V_max - V_min) / ((R_max - R_min) / (value - R_min));//valueをV_minからV_Maxの範囲からR_minからR_maxの範囲にする

    }

}
