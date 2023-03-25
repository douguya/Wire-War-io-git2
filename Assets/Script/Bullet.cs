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



    public int[] Bullet_Speed;//�e��
    public int[] Ballet_Range;//�˒�����
    public Vector3[] Bullet_Size;//�e�̃T�C�Y

    public Vector3 StartPosi;//���ˎ��̈ʒu

    public ParticleSystem Bullet_Particle;//���eor���R���Ŏ��ɔ�������p�[�e�B�N��

    public MeshRenderer BulletMeshe;//�p�[�e�B�N���Đ����ɒe�ۂ��\���ɂ��邽�߂̂���
    public SphereCollider Collider; //�p�[�e�B�N���Đ����ɓ����蔻����폜���邽�߂̂���

    private bool Exprode=false;//�������̔���@�e�ۂ��~�߂�̂Ɏg�p�@�e�ۂɃp�[�e�B�N����t���Ă���̂ŁA�p�[�e�B�N���Đ����͋����~�߂Ȃ���΂����Ȃ�����

    // Start is called before the first frame update
    void Start()
    {
        StartPosi = transform.position;//��苗���ŏ������߂ɔ��ˈʒu���L��


        transform.localScale = Bullet_Size[(int)bulletMode];

    
      
    }

    // Update is called once per frame
    void Update()
    {
        if (Exprode==false)//�������Ȃ�����O�i
        {
            this.transform.Translate(Vector3.right * Time.deltaTime * Bullet_Speed[(int)bulletMode]);//�w�肵���e���Ŕ�΂�

        }
     

        if (Vector3.Distance(transform.position, StartPosi)> Ballet_Range[(int)bulletMode])//���˂����Ƃ��납�痣�ꂽ��I��
        {
           
            StartCoroutine("EndBullet");//�e�ۂ̏I�������@���e�p�[�e�B�N���̌�폜
            Debug.Log(111);
        }
      
    }

    void OnTriggerEnter(Collider other)
    {

        StartCoroutine("EndBullet");//�e�ۂ̏I�������@���e�p�[�e�B�N���̌�폜
        Debug.Log(other.gameObject.name);
    }



    IEnumerator EndBullet()//�e�ۂ̏I�������@���e�p�[�e�B�N���̌�폜
    {
      
        Exprode = true; //�����@�e�ۂ̓������~
        Bullet_Particle.Play();//�p�[�e�B�N���Đ�
        BulletMeshe.enabled = false;//�\������
        Collider.enabled = false;//�����蔻�����

        yield return new WaitForSeconds(0.2f);
        Destroy(this.gameObject);//�e�ۂ̏���

    }

}
