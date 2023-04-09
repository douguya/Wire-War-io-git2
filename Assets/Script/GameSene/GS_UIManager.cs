using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GS_UIManager : MonoBehaviour//�Q�[���V�[���ł�UI
{

    public GameObject Energy_NoFilde;
    public GameObject Energy_Forecast_Lines;

    public GameObject HP_FullFilde;


    public GameObject[] Target;//�Ə�UI�@�@�˒��O ;//�Ə�UI2�@�˒���  //�Ə�UI3�@�`���[�W����


    [Header("�G�l���M�[UI �����ԗp�̈�̈ʒu--------")]
    public int EP_Max;//UI����Position��Max
    public int EP_Mini;//UI����Position��Min
    [Header("�G�l���M�[����\�����C��UI �̈ʒu--------")]
    public int EFP_Max;//UI����Position��Max
    public int EFP_Mini;//UI����Position��Min

    [Header("HPUI �̗͗̈�̈ʒu--------")]
    public int HPP_Max;//UI����Position��Max
    public int HPP_Mini;//UI����Position��Min


    public float Test=100;





    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        






    }



    public  void Energy_UI_Conversion(float Energy, int Empty, int Full)//�ő�lFull�@�ŏ��lEmpty�@�̃G�l���M�[�ʂ��@Map�֐����g����UI�ŕ\������@�G�l���M�[�Ȃ���Ԃ̉摜���ォ��~��Ă��銴��
    {
        if (Energy < Empty)//�͈͂𒴂��Ă����狭���I�ɍ��킹��
        {
            Energy = Empty;
        }
        else if (Energy > Full)
        {
            Energy = Full;
        }


        var EnerugyPoint = Energy_NoFilde.GetComponent<RectTransform>().anchoredPosition;//�G�l���M�[���Ȃ���Ԃ̐}�`�̈ʒu

        EnerugyPoint.y = Map(Energy, Empty, Full, EP_Mini, EP_Max);

        Energy_NoFilde.GetComponent<RectTransform>().anchoredPosition = EnerugyPoint;//�G�l���M�[���Ȃ���Ԃ̐}�`�̈ʒu��ω�
    }
    public void Energy__Forecast_Lines_UI_Conversion(float EnergyForecast, int Empty, int Full)//�ő�lFull�@�ŏ��lEmpty�@�̃G�l���M�[�ʂ��@Map�֐����g����UI�ŕ\������@�G�l���M�[�Ȃ���Ԃ̉摜���ォ��~��Ă��銴��
    {
        if (EnergyForecast < Empty)//�͈͂𒴂��Ă����狭���I�ɍ��킹��
        {
            EnergyForecast = Empty;
        }
        else if (EnergyForecast > Full)
        {
            EnergyForecast = Full;
        }


        var EnerugyPoint = Energy_Forecast_Lines.GetComponent<RectTransform>().anchoredPosition;//�G�l���M�[���Ȃ���Ԃ̐}�`�̈ʒu

        EnerugyPoint.y = Map(EnergyForecast, Empty, Full, EFP_Mini, EFP_Max);

        Energy_Forecast_Lines.GetComponent<RectTransform>().anchoredPosition = EnerugyPoint;//�G�l���M�[���Ȃ���Ԃ̐}�`�̈ʒu��ω�
    }


    public void HP_UI_Conversion(float HP, int Empty, int Full)//�ő�lFull�@�ŏ��lEmpty�@��HP�ʂ��@Map�֐����g����UI�ŕ\������@HP�̉摜���g�ɂ���銴��
    {
        if (HP < Empty)//�͈͂𒴂��Ă����狭���I�ɍ��킹��
        {
            HP = Empty;
        }
        else if (HP > Full)
        {
            HP = Full;
        }

        var HP_Point = HP_FullFilde.GetComponent<RectTransform>().anchoredPosition;//HP�̐}�`�̈ʒu

        HP_Point.x = Map(HP, Empty, Full, HPP_Mini, HPP_Max);

        HP_FullFilde.GetComponent<RectTransform>().anchoredPosition = HP_Point;//HP�̐}�`�̈ʒu��ω�
    }


   
    public float Map(float value, float R_min, float R_max, float V_min, float V_max)
    {
        /*
          float Rrenge = (R_max-R_min);�@�@�@R�͈̔͂��Z�o
          float convartR = (value-R_min);�@�@value�� R_min �ɑ΂��Ăǂ̒��x�̍������邩���Z�o
          float Rratio = Rrenge/ convartR;�@ R_min ~ R_max �͈͓̔��ɂ����� value �̔䗦 �� Rratio����1�̌`�� �Z�o

          float Vrenge = (V_max-V_min);�@�@  V�͈̔͂��Z�o
          float VDelta = (Vrenge/Rratio);    // R�̔䗦�Ɋ�Â���V�͈̔͂Œl�̍����Z�o����

          //    V_min + (V_max - V_min)      �ŏ��l�𑫂����ƂŁA�l�Ƃ��Ă̏����ʒu�𐮂���
          */






        return V_min + (V_max - V_min) / ((R_max - R_min) / (value - R_min));//R_min����R_max�͈̔͂ŗ^����ꂽ�Ƃ��ɁA�����V_min����V_max�͈̔͂Ƀ}�b�s���O

    }
    public void TargetSift_Position(Vector2 TargetPosition ,bool IN)//�Ə��̑���@�˒������ǂ����@�@�ڕW���ɏƏ�UI�����낦��
    {


        Target[0].GetComponent<RectTransform>().position = TargetPosition;//Ray�ƃI�u�W�F�N�g�̌�_�ɏƏ�UI�����킹��
        Target[1].SetActive(IN);//�Ə����˒����̂��̂ɕω�


    }



    public void TargetSift_Charge( bool Charge)//�Ə��̑���@�`���[�W��Ԃ��ǂ����@
    {

        Target[2].SetActive(Charge);

    }



}
