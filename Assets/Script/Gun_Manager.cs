using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Manager : MonoBehaviour
{

    public GameObject Target;//�Ə�UI�@�@�˒��O
    private GameObject Target2;//�Ə�UI2�@�˒���
    private GameObject Target3;//�Ə�UI3�@�`���[�W����
    public Camera CharacterCamera;








    public GameObject Gun;�@//�e�̃I�u�W�F�N�g
    public GameObject BulletPoint;//�e�e�̐����_
    public GameObject Bullet;//�e�ۂ̃v���n�u

    public float ChageTimer = 0;
    public float Required_ChageTime;


    public int[] Vewing_distance;//�e��Ray�̋����@���ˎ��̗L���˒�����


    Ray Arm_Ray;//�{�Ƃ�GunRay�����A�I�u�W�F�N�g�Ƃ킩��₷�������邽�߂�Arm�ɂ���
    RaycastHit hit;//�˒�������悤��RaycastHit


    public enum BulletCharge
    {
        NoCharge = 0,//�ʏ�e�ɉ����A���C���[��NoCharge�Ƃ��Ă̒l���g��
        FullCharge = 1,

    }
   public BulletCharge bulletCharge = 0;











    //�ȉ����C���[�Ɋւ������
    private bool Within_Range = false;//�˒���
    public bool Wire_Injectioning = false;//�ˏo��
    public bool Wire_Pull = false;//���C���[������
    public bool Wire_farst = false;//���e���x�����쓮�����邽�߂̂���
    public bool Wire_Distance_far = false;//���C���[���e�n�_�Ƃ̋������L�����Ă���

    public LineRenderer Wire_Line;//���C���[��\�����郉�C�������_���[�@
    public GameObject Wire_shot_shell;//���C���[�V���b�g�̃v���n�u
    private GameObject Generated_Wire_shot;//���C���[�̐�[�̃I�u�W�F�N�g �v���n�u��������������
    public float[] Wire_to_PlayerDistance = new float[3];//���C���[�Ƃ̋����̊i�[
    public int Wire_force;//�����̗́@
    private Vector3 Add_Wire_force;//Y�����̌����͒����p�@���ۂ̌������Ɏg�p

    private float Wire_TimeLimit = 5;// ���C���[���؎��ԁ@�^�C�����~�b�g
    public float Wire_Timer;// ���C���[�^�C�}�[�@���ؔ��f�Ɏg��
                        
    Rigidbody Rb;//�@���C���[�������Ɏg���������Z�I�u�W�F�N�g


    void Start()
    {
        Rb = GetComponent<Rigidbody>();//���W�b�g�{�f�B�̎擾

        Target2 = Target.transform.GetChild(0).gameObject;
        Target3 = Target.transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        TargetSift();//�e����o��Ray�̏Փˈʒu�ɏƏ���������
    }
    void LateUpdate()
    {
        
        Wire();
        // Aim();//�Ə�
    }
    public void TargetSift()
    {

        Arm_Ray = new Ray(Gun.transform.position, Gun.transform.right); //�e(�r)����Ray���΂�

        Debug.Log((int)bulletCharge);

        Debug.DrawRay(Arm_Ray.origin, Arm_Ray.direction * Vewing_distance[(int)bulletCharge], Color.blue);


        if (Physics.Raycast(Arm_Ray, out hit, Vewing_distance[(int)bulletCharge]))//Ray���I�u�W�F�N�g�ɓ������Ă���ꍇ�@�v����Ɏ˒���
        {
            Within_Range = true;//�˒���
            Target.GetComponent<RectTransform>().position = CharacterCamera.WorldToScreenPoint(hit.point);//Ray�ƃI�u�W�F�N�g�̌�_�ɏƏ�UI�����킹��
            Target2.SetActive(true);//�Ə����˒����̂��̂ɕω�
        }
        else //�v����Ɏ˒��O
        {
            Within_Range = false;//�˒��O
            Target.GetComponent<RectTransform>().position = CharacterCamera.WorldToScreenPoint(Arm_Ray.GetPoint(Vewing_distance[(int)bulletCharge]));//Ray��(100)�n�_�ɃI�u�W�F�N�g�̌�_�ɏƏ�UI�����킹��
            Target2.SetActive(false);//�Ə����˒��O�̂��̂ɕω�
        }





    }



    public void BulletShot()
    {
       
          
          
        Bullet.GetComponent<Bullet>().bulletMode = (Bullet.BulletMode)bulletCharge;//�e�̃��[�h���`���[�W��ԂƓ�������       

        Instantiate(Bullet, BulletPoint.transform.position, Gun.transform.rotation);//�e�ۂ̃C���X�^���X�𔭎˃|�C���g���甭��

        ChargeReset();//�`���[�W���Z�b�g�@���r���[�ȃ`���[�W�Ō������ꍇ�̃��Z�b�g���܂�

    }

    public void Charge()
    {
        ChageTimer += Time.deltaTime;//�`���[�W���Ԃ��v��
        if (Required_ChageTime <= ChageTimer)//�`���[�W����
        {
            bulletCharge = BulletCharge.FullCharge;//�`���[�W��ԂɈڍs
            Target3.SetActive(true);//�Ə����`���[�W�ς݂̂��̂ɕω�
        }
    }
    public void ChargeReset()
    {
        ChageTimer = 0;//�`���[�W���ԃ��Z�b�g
       
        bulletCharge = BulletCharge.NoCharge;//��`���[�W��ԂɈڍs
        Target3.SetActive(false);//�Ə����`���[�W�ς݂̂��̂ɕω�
    }

    public void Wire_Shot()
    {
        ChargeReset();//�`���[�W���Z�b�g�@���C���[�ˏo�Ɠ����Ƀ`���[�W�����Z�b�g

        if (Wire_Injectioning)//���C���[�ˏo��
        {
             Wire_Cut();//���C���[�̐؂藣���@�@�@�Ƃɂ������ɂ����C���[���Ԃ����؂�@�@�ȑO�̐�[�����������̎��̂Ŏc���Ă������̃P�A�ƁA�@������x�̃N���b�N�ɂ�郏�C���[�̐؂藣�������˂�


        }
        else if(Within_Range)//���C���[�ˏo���o�Ȃ��ꍇ
        {
            Generated_Wire_shot = (GameObject)Instantiate(Wire_shot_shell, BulletPoint.transform.position, Gun.transform.rotation);//���C���[�̐�[���̐����Ɗi�[
            Generated_Wire_shot.GetComponent<Wire_Shot_Shell>().targetPosition = hit.point;//���C���[�̐�[���̖ڕW�n�_��Ray�̒��e�n�_�֐ݒ�

            Generated_Wire_shot.GetComponent<Wire_Shot_Shell>().Player = this.gameObject;

            Wire_Injectioning = true;//���C���[�ˏo��
            Wire_farst = true;//���C���[���e��Ɉ�x�����쓮�����邽�߂̂���
            Wire_Line.enabled = true;//���C�������_���[��L���ɂ���
        }

    }

    
    


// Start is called before the first frame update
private void Wire()//���C���[�Ɋւ������ 
{






    if (Wire_Injectioning)//���C���[�ˏo��
    {
        Wire_Line.SetPosition(0, BulletPoint.transform.position);//���C���[�̎n�_��ݒ�(���C���[�̎ˏo�|�C���g)
        Wire_Line.SetPosition(1, Generated_Wire_shot.transform.position);//���C���[�̏I�_��ݒ�(���C���[�̐�[)

        if (Wire_Pull)//���C���[���e=������
        {

            if (Wire_farst)//������J�n���̏��񏈗�
            {
                Rb.velocity = new Vector3(Rb.velocity.x, Rb.velocity.y / 2, Rb.velocity.z);//����or�㏸���x�̌����@�ǂ̒��x��������ׂ����͈�l�̗]�n����@�@�@�@����2�͉ʂ����Ă킴�킴�ϐ��ɂ���K�v������̂��c�H�@(�}�W�b�N�i���o�[)

                Wire_to_PlayerDistance[0] = Vector3.Distance(transform.position, Generated_Wire_shot.transform.position);//�v���C���[�ƃ��C���[�̐�[�Ƃ̋���
                Wire_to_PlayerDistance[2] = Wire_to_PlayerDistance[0];//���C���[�Ƃ̋������X�V���Ȃ��Ƃ���ɕۊǁ@���ƂŎg��
                Wire_farst = false;//���񏈗��I��


            }

            //�ȉ���������

            Add_Wire_force = (Generated_Wire_shot.transform.position - Gun.transform.position).normalized * Wire_force;
            Rb.AddForce(new Vector3(Add_Wire_force.x, Add_Wire_force.y * 1.3f, Add_Wire_force.z));//�ڕW�n�_�փv���C���[������ �d�͂ɂ��炪�����߂�Y�����֋��߂ɐݒ�@����͂������ɂ��Ƃŕϐ��ɂ��Ƃ�(�}�W�b�N�i���o�[)
            Wire_to_PlayerDistance[1] = Vector3.Distance(transform.position, Generated_Wire_shot.transform.position);//�v���C���[�ƃ��C���[�̐�[�Ƃ̋���


            //�������������܂�




            if (Wire_to_PlayerDistance[0] != Wire_to_PlayerDistance[1])//���C���[�Ƃ̋����ɕω�����������
            {
                if (Wire_to_PlayerDistance[0] <= Wire_to_PlayerDistance[1])//���C���[�Ƃ̋������k�܂��Ă��鎞
                {
                    Wire_Distance_far = true;//���C���[���牓�̂��Ă���
                }
                else
                {
                    Wire_Distance_far = false;//���C���[�ɋ߂Â��Ă���
                }

                Wire_to_PlayerDistance[0] = Vector3.Distance(transform.position, Generated_Wire_shot.transform.position);//���C���[�Ƃ̋������Čv��

            }

            //---------------------------------------�ȉ��I������------------------------------------------



            //�ȉ� ���C���[�n�_�������̌��ɂ���A���������C���[���牓�̂��Ă��āA�Ȃ������̋��������ȏ�̏ꍇ�A���C���[������
            if (Vector3.Dot(Generated_Wire_shot.transform.position - transform.position, transform.forward) <= 0.3)//���ςɂ��O�㔻��@���̏ꍇ�@����͂������ɂ��Ƃŕϐ��ɂ��Ƃ�(�}�W�b�N�i���o�[)
            {

                if (Wire_Distance_far == true)//����������Ă���ꍇ
                {

                    if (Wire_to_PlayerDistance[0] >= 5 * Map(Wire_to_PlayerDistance[2], 0, Vewing_distance[0], 1f, 1.3f))//���C���[�̐�[���Ƃ̋��������𒴂����烏�C���[������   ���ꏈ���@��l�̂��`�A���@����͂������ɂ��Ƃŕϐ��ɂ��Ƃ�(�}�W�b�N�i���o�[)
                    {

                        if (Wire_farst == false)//���C���[���������ǂ����@�Œ���̏������ς�ł���Ȃ�
                        {
                            Wire_Cut();//���C���[����
                        }
                    }
                }


            }


            if (Wire_to_PlayerDistance[0] < 5)//���C���[���e�n�_�Ƃ��΂炭���ȉ��̋�����������
            {
                Wire_Timer += Time.deltaTime;
                if (Wire_Timer >= Wire_TimeLimit * Map(Wire_to_PlayerDistance[2], 0, Vewing_distance[0], 0.7f, 1.1f)) //���C���[�̋��������ȉ��̎������΂炭�������玩�؂邷��@ ���ꏈ���@��l�̂��`�A���@����͂������ɂ��Ƃŕϐ��ɂ��Ƃ�(�}�W�b�N�i���o�[)
                {
                    if (Wire_farst == false)//���C���[���������ǂ����@�Œ���̏������ς�ł���Ȃ�
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

public void Wire_Cut()//���C���[�̐؂藣��
{


    Destroy(Generated_Wire_shot);//���C���[��[���̏���
    Wire_Timer = 0;
    Wire_Injectioning = false;//���C���[�ˏo���ȉ��S�Ẵt���O�̏�����

    Wire_Pull = false;//���C���[���e=������


    Wire_farst = false;//������J�n���̏��񏈗�
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

        return V_min + (V_max - V_min) / ((R_max - R_min) / (value - R_min));//value��V_min����V_Max�͈̔͂���R_min����R_max�͈̔͂ɂ���

    }

}
