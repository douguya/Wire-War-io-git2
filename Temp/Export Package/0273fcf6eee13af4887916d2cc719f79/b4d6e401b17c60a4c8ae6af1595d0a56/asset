using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class CharactreContorol : MonoBehaviour
{
    //以下デバッグ用変数
    public Text T1;
    public Text T2;
    public Text T3;
    public Text T4;
    public Text T5;
    public Text T6;
    public Text T7;
    public Text T8;
    public Text T9;

    public GameObject Target;
    //デバッグ用変数ここまで



    public Transform waist;//腰のオブジェクト
    public Transform Legs; //足の総体オブジェクト
    public GameObject Gun;　//銃のオブジェクト


    //以下歩行用bool値
    bool Right = false;
    bool Left = false;
    bool Forward = false;
    bool Back = false;
    //歩行用bool値ここまで




    public int walk;//歩行速度
    public Vector3 JumpForce;//ジャンプ力
    public bool CanJump = true;//ジャンプ許可
    Rigidbody Rb;//　歩行/跳躍　時に使用する物理演算コンポーネント



    public int Vewing_distance;//銃のRayの距離　発射時の有効射程距離



    public GameObject Body;//プレイヤーの体
    public GameObject FutPoint;//接地判定用のオブジェクト
    public GameObject CameraPoint_X;//カメラの軸
    public GameObject Camera;//カメラ
    private Camera CharacterCamera;
    private Vector3[] Mouse = new Vector3[2];  //マウスの位置(変化前と現在)
    public Animator anim;  //Animatorをanimという変数で定義する

    public GameObject Bullet;
  

    Ray Arm_Ray;//本とはGunRayだが、オブジェクトとわかりやすく分けるためにArmにする







    // Start is called before the first frame update
    void Start()
    {

        Mouse[0] = Input.mousePosition;//mouseの座標の取得
        Rb = this.GetComponent<Rigidbody>();
        CharacterCamera = Camera.GetComponent<Camera>();
      
    }

    // Update is called once per frame
    void Update()
    {
        TargetSift();//銃から出たRayの衝突位置に照準を合せる
        Move_();//WASD.spaceキーの入力とそれによる移動

    }

    void LateUpdate()
    {
        Turn_around();//マウスの動きによる体の向きの変化
        Shot();
      // Aim();//照準
    }

   
    public void TargetSift()
    {

        Arm_Ray = new Ray(Gun.transform.position, Gun.transform.right * Vewing_distance); //銃(腕)からRayを飛ばす



        RaycastHit hit;
        if (Physics.Raycast(Arm_Ray, out hit))//Rayがオブジェクトに当たっている場合
        {

            T1.text = "Hit  :  " + hit.collider.gameObject.name;
            Target.GetComponent<RectTransform>().position = CharacterCamera.WorldToScreenPoint(hit.point);

        }
        else
        {

            T1.text = "Not";
            Target.GetComponent<RectTransform>().position = CharacterCamera.WorldToScreenPoint(Arm_Ray.GetPoint(Vewing_distance));

        }
    }


    public void Move_()
    {
        //キー入力
        // Wキー（前方移動）
        if (Input.GetKey(KeyCode.W))
        {
            Forward = true;
            Back = false;
        }

        // Sキー（後方移動）
        if (Input.GetKey(KeyCode.S))
        {
            Forward = false;
            Back = true;
        }

        // Dキー（右移動）
        if (Input.GetKey(KeyCode.D))
        {
            Right = true;
            Left = false;
        }

        // Aキー（左移動）
        if (Input.GetKey(KeyCode.A))
        {
            Right = false;
            Left = true;
        }

        // Spaceキー（ジャンプ）
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (CanJump == true)
            {
                CanJump = false;
                anim.SetTrigger("Jump");
                Rb.AddForce(JumpForce, ForceMode.Impulse);
            }
        }


        //入力終了時
        //左シフト(ダッシュ)

        // Wキー（前方移動）
        if (Input.GetKeyUp(KeyCode.W))
        {
            Forward = false;
        }

        // Sキー（後方移動）
        if (Input.GetKeyUp(KeyCode.S))
        {
            Back = false;
        }

        // Dキー（右移動）
        if (Input.GetKeyUp(KeyCode.D))
        {
            Right = false;
        }

        // Aキー（左移動）
        if (Input.GetKeyUp(KeyCode.A))
        {
            Left = false;
        }



        //以上キー入力


        //以下キー入力によるアニメーション/移動/足の向き調整


        
        if (Forward  && Right==false && Left==false)//↑
        {
            anim.SetBool("Forward_Anim", true);
            Legs.transform.localEulerAngles = new Vector3(0,0, 0);//足の向きをローカルで変更して入力に合わせる
          　transform.position+=transform.forward*walk*Time.deltaTime;//歩行速度に正面ベクトルをかけて軌道修正
        }

        if (Right && Forward==false && Back==false)//→
        {
            anim.SetBool("Forward_Anim", true);
            Legs.transform.localEulerAngles = new Vector3(-90, 0,0);//足の向きをローカルで変更して入力に合わせる
            transform.position += transform.right * walk * Time.deltaTime;

        }
        if (Left && Forward==false && Back==false)//←
        {
            anim.SetBool("Forward_Anim", true);
            Legs.transform.localEulerAngles = new Vector3(90, 0,0);//足の向きをローカルで変更して入力に合わせる
            transform.position += -transform.right * walk * Time.deltaTime;
        }


       

        if (Forward && Right && Left==false)//↑→
        {
            anim.SetBool("Forward_Anim", true);
            Legs.transform.localEulerAngles = new Vector3(-45, 0, 0);//足の向きをローカルで変更して入力に合わせる
            transform.position += (transform.forward+transform.right) * walk * Time.deltaTime;//歩行速度に正面ベクトルをかけて軌道修正
        }
        if (Forward && Right==false && Left)//←↑
        {
            anim.SetBool("Forward_Anim", true);
            Legs.transform.localEulerAngles = new Vector3(45, 0, 0);//足の向きをローカルで変更して入力に合わせる
            transform.position += (transform.forward -transform.right) * walk * Time.deltaTime;//歩行速度に正面ベクトルをかけて軌道修正
        }


        if (Back && Right == false && Left == false)//↓
        {
            anim.SetBool("Back_Anim", true);
            Legs.transform.localEulerAngles = new Vector3(0, 0, 0);//足の向きをローカルで変更して入力に合わせる
            transform.position += -transform.forward * walk * Time.deltaTime;//歩行速度に正面ベクトルをかけて軌道修正
        }



        if (Back && Right && Left==false)//↓→
        {
            anim.SetBool("Back_Anim", true);
            Legs.transform.localEulerAngles = new Vector3(45, 0, 0);
            transform.position += (-transform.forward + transform.right) * walk * Time.deltaTime;//歩行速度に正面ベクトルをかけて軌道修正
        }
        if (Back &&  Left && Right ==false)//←↓
        { anim.SetBool("Back_Anim", true);
            Legs.transform.localEulerAngles = new Vector3(-45, 0, 0);
            transform.position += (-transform.forward - transform.right) * walk * Time.deltaTime;//歩行速度に正面ベクトルをかけて軌道修正
        }

        if (Forward == false && Right == false && Left == false)//アニメーション終了
        {
            anim.SetBool("Forward_Anim", false);
        }
        if (Back==false)//アニメーション終了
        {
            anim.SetBool("Back_Anim",false);
        }
        if (Forward == false && Right == false && Left == false && Back == false)
        { 
            Legs.transform.localEulerAngles = new Vector3(0, 0, 0);//足の向きをローカルで変更して入力に合わせる
        }

    }

    public void Turn_around()
    {

        Mouse[1] = Input.mousePosition;//mouseの座標の取得
        if (Mouse[0] != Mouse[1])//マウスが移動しているかどうか
        {
            //マウスの移動でカメラを回転させると、XとYがちょっとややこしいことになる　

            var Came_Y = Mouse[0].x - Mouse[1].x;
            Came_Y = (this.transform.rotation.y + Came_Y) * 0.05f;//　 0.05f　マウス感度　後で変数にする

            var Came_X = Mouse[0].y - Mouse[1].y;
            Came_X = (CameraPoint_X.transform.rotation.x + Came_X) * 0.05f;



            transform.Rotate(new Vector3(0, -Came_Y, 0));//キャラクターの体ごと向きを変える


            CameraPoint_X.transform.Rotate(new Vector3(Came_X, 0, 0));//カメラをポイントを中心に縦回転


            var Wrote = waist.localEulerAngles;

            waist.transform.localEulerAngles = new Vector3(Wrote.x, Wrote.y + Came_X, Wrote.z);



            Mouse[0] = Mouse[1];//マウス座標のリセット
        }

    }


    public void Shot()
    {
        if (Input.GetMouseButtonUp(0))
        {

            Instantiate(Bullet, Gun.transform.position, Gun.transform.rotation);//弾丸のインスタンスを発射ポイントから発射
        }
    }













    public void AirToIdle()//ジャンプ終了後処理
    {
        anim.SetTrigger("Idle");
      
    }
  


    public void PostJump_processing()//ジャンプ直後処理
    {
        
        anim.SetBool("Jump", false);
        
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
