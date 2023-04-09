using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Object_Component_Setter : MonoBehaviour
{
    public Text T1;
    public Text T2;
    public Text T3;
    public Text T4;
    public Text T5;
    public Text T6;
    public Text T7;
    public Text T8;
    public Text T9;

    private CharactreContorol CharactreC_SC;
    private Gun_Manager GunM_SC;

    private GS_UIManager UIM_SC;

    // Start is called before the first frame update
    //プレイヤ―についているスクリプトの変数のうち、
    //わざわざ自分自身のオブジェクトを指定しなければならないもの、または外部のオブジェクトに依存するものをこのスクリプトから設定する
    //そのため、プレハブや子オブジェクトに関してはここで関与しない

    private void Awake()
    {
        CharactreC_SC = GetComponent<CharactreContorol>();
        GunM_SC=GetComponent<Gun_Manager>();



        GunM_SC.Rb= CharactreC_SC.Rb= GetComponent<Rigidbody>();



        UIM_SC = GameObject.FindWithTag("UIManager").GetComponent<GS_UIManager>();
        GunM_SC.uimanager = CharactreC_SC.uimanager = UIM_SC;

        CharactreC_SC.GunScript = GunM_SC;

        GunM_SC.Wire_Line= GetComponent<LineRenderer>();


    }

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
