using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire_Shot_Shell : MonoBehaviour
{
    public float Shell_Speed = 30;//�e��
    public Vector3 targetPosition;//�ڕW�n�_
                                  // Start is called before the first frame update
    public GameObject Player;

    private bool Shell_Landing=false;



    public ParticleSystem Shell_Particle;//���e���ɔ�������p�[�e�B�N��


    private bool Exprode = false;//�������̔���@�e�ۂ��~�߂�̂Ɏg�p�@�e�ۂɃp�[�e�B�N����t���Ă���̂ŁA�p�[�e�B�N���Đ����͋����~�߂Ȃ���΂����Ȃ�����
    private void Awake()
    {
      
    }
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {



        //  this.transform.Translate(Vector3.right * Time.deltaTime * Shell_Speed);//�w�肵���e���Ŕ�΂�

        if (Shell_Landing==false)//���e����܂�
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Shell_Speed * Time.deltaTime);//Bullet�Ƃ͈Ⴄ�����Ȃ̂ŕ���  ���˂���Player�̎q�I�u�W�F�N�g�Ƃ��ĕۑ�����̂ŁAPlayer�̌�����ʒu�Ɋ�����Ȃ���΂���������@
        }
      

    


        //�I�u�W�F�N�g�ɓ����������ɂ������������̂ł́H
        //�e�ۂƃv���C���[�ɂ͓�����Ȃ��悤�ɂ���H

    }
  
    void OnTriggerEnter(Collider other)//���C���[���e
    {
        if (Shell_Landing == false)
        {
            Shell_Landing = true;//Shell�X�N���v�g�����e����
            Player.GetComponent<Gun_Manager>().Wire_Pull = true;//���C���[����
            Shell_Particle.Play();//�p�[�e�B�N���Đ�
        }
    
    }
}
