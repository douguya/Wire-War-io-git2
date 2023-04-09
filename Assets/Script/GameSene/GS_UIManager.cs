using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GS_UIManager : MonoBehaviour//ゲームシーンでのUI
{

    public GameObject Energy_NoFilde;
    public GameObject Energy_Forecast_Lines;

    public GameObject HP_FullFilde;


    public GameObject[] Target;//照準UI　　射程外 ;//照準UI2　射程内  //照準UI3　チャージ完了


    [Header("エネルギーUI 消費状態用領域の位置--------")]
    public int EP_Max;//UI側のPositionのMax
    public int EP_Mini;//UI側のPositionのMin
    [Header("エネルギー消費予測ラインUI の位置--------")]
    public int EFP_Max;//UI側のPositionのMax
    public int EFP_Mini;//UI側のPositionのMin

    [Header("HPUI 体力領域の位置--------")]
    public int HPP_Max;//UI側のPositionのMax
    public int HPP_Mini;//UI側のPositionのMin


    public float Test=100;





    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        






    }



    public  void Energy_UI_Conversion(float Energy, int Empty, int Full)//最大値Full　最小値Empty　のエネルギー量を　Map関数を使ってUIで表現する　エネルギーない状態の画像が上から降りてくる感じ
    {
        if (Energy < Empty)//範囲を超えていたら強制的に合わせる
        {
            Energy = Empty;
        }
        else if (Energy > Full)
        {
            Energy = Full;
        }


        var EnerugyPoint = Energy_NoFilde.GetComponent<RectTransform>().anchoredPosition;//エネルギーがない状態の図形の位置

        EnerugyPoint.y = Map(Energy, Empty, Full, EP_Mini, EP_Max);

        Energy_NoFilde.GetComponent<RectTransform>().anchoredPosition = EnerugyPoint;//エネルギーがない状態の図形の位置を変化
    }
    public void Energy__Forecast_Lines_UI_Conversion(float EnergyForecast, int Empty, int Full)//最大値Full　最小値Empty　のエネルギー量を　Map関数を使ってUIで表現する　エネルギーない状態の画像が上から降りてくる感じ
    {
        if (EnergyForecast < Empty)//範囲を超えていたら強制的に合わせる
        {
            EnergyForecast = Empty;
        }
        else if (EnergyForecast > Full)
        {
            EnergyForecast = Full;
        }


        var EnerugyPoint = Energy_Forecast_Lines.GetComponent<RectTransform>().anchoredPosition;//エネルギーがない状態の図形の位置

        EnerugyPoint.y = Map(EnergyForecast, Empty, Full, EFP_Mini, EFP_Max);

        Energy_Forecast_Lines.GetComponent<RectTransform>().anchoredPosition = EnerugyPoint;//エネルギーがない状態の図形の位置を変化
    }


    public void HP_UI_Conversion(float HP, int Empty, int Full)//最大値Full　最小値Empty　のHP量を　Map関数を使ってUIで表現する　HPの画像が鉾にずれる感じ
    {
        if (HP < Empty)//範囲を超えていたら強制的に合わせる
        {
            HP = Empty;
        }
        else if (HP > Full)
        {
            HP = Full;
        }

        var HP_Point = HP_FullFilde.GetComponent<RectTransform>().anchoredPosition;//HPの図形の位置

        HP_Point.x = Map(HP, Empty, Full, HPP_Mini, HPP_Max);

        HP_FullFilde.GetComponent<RectTransform>().anchoredPosition = HP_Point;//HPの図形の位置を変化
    }


   
    public float Map(float value, float R_min, float R_max, float V_min, float V_max)
    {
        /*
          float Rrenge = (R_max-R_min);　　　Rの範囲を算出
          float convartR = (value-R_min);　　valueが R_min に対してどの程度の差があるかを算出
          float Rratio = Rrenge/ convartR;　 R_min ~ R_max の範囲内における value の比率 を Rratio分の1の形で 算出

          float Vrenge = (V_max-V_min);　　  Vの範囲を算出
          float VDelta = (Vrenge/Rratio);    // Rの比率に基づいてVの範囲で値の差を算出する

          //    V_min + (V_max - V_min)      最小値を足すことで、値としての初期位置を整える
          */






        return V_min + (V_max - V_min) / ((R_max - R_min) / (value - R_min));//R_minからR_maxの範囲で与えられたときに、それをV_minからV_maxの範囲にマッピング

    }
    public void TargetSift_Position(Vector2 TargetPosition ,bool IN)//照準の操作　射程内かどうか　　目標物に照準UIをそろえる
    {


        Target[0].GetComponent<RectTransform>().position = TargetPosition;//Rayとオブジェクトの交点に照準UIを合わせる
        Target[1].SetActive(IN);//照準を射程内のものに変化


    }



    public void TargetSift_Charge( bool Charge)//照準の操作　チャージ状態かどうか　
    {

        Target[2].SetActive(Charge);

    }



}
