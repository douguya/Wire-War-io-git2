using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class CharactreContorol : MonoBehaviour
{
    //�ȉ��f�o�b�O�p�ϐ�
    


    //�f�o�b�O�p�ϐ������܂�

    public Transform waist;//���̃I�u�W�F�N�g
    public Transform Legs; //���̑��̃I�u�W�F�N�g


    public float HP;
    public int HPMax;
    public int HPMini;


    //�ȉ����s�pbool�l
    bool Right = false;
    bool Left = false;
    bool Forward = false;
    bool Back = false;
    //���s�pbool�l�����܂�



    public int walk;//���s���x
    public int MaxSpeed;//���s���x

    public Vector3 JumpForce;//�W�����v��
    public bool CanJump = true;//�W�����v����
    [System.NonSerialized]
    public Rigidbody Rb;//�@���s/����@���Ɏg�p���镨�����Z�R���|�[�l���g



    Vector3 Direction;

    public GameObject Body;//�v���C���[�̑�
    public GameObject FutPoint;//�ڒn����p�̃I�u�W�F�N�g
    public GameObject CameraPoint_X;//�J�����̎�
    public GameObject Camera;//�J����

    private Vector3[] Mouse = new Vector3[4];  //�}�E�X�̈ʒu(�ω��O�ƌ���)
    public Animator anim;  //Animator��anim�Ƃ����ϐ��Œ�`����

    [System.NonSerialized]
    public Gun_Manager GunScript;


    [System.NonSerialized]
    public GS_UIManager uimanager;//UI�̃}�l�[�W��


    // Start is called before the first frame update
    void Start()
    {
       
        Mouse[0] = Input.mousePosition;//mouse�̍��W�̎擾
      

    }

    // Update is called once per frame
    void Update()
    {
        Move_();//WASD.space�L�[�̓��͂Ƃ���ɂ��ړ�

    }

    void LateUpdate()
    {

        Turn_around();//�}�E�X�̓����ɂ��̂̌����̕ω�
        Shot();
        uimanager.HP_UI_Conversion(HP, HPMini, HPMax);
    }





    public void Move_()
    {
        //�L�[����
        // W�L�[�i�O���ړ��j
        if (Input.GetKey(KeyCode.W))
        {
            Forward = true;
            Back = false;
        }

        // S�L�[�i����ړ��j
        if (Input.GetKey(KeyCode.S))
        {
            Forward = false;
            Back = true;
        }

        // D�L�[�i�E�ړ��j
        if (Input.GetKey(KeyCode.D))
        {
            Right = true;
            Left = false;
        }

        // A�L�[�i���ړ��j
        if (Input.GetKey(KeyCode.A))
        {
            Right = false;
            Left = true;
        }

        // Space�L�[�i�W�����v�j
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (CanJump == true)
            {
                CanJump = false;
                anim.SetTrigger("Jump");
                Rb.AddForce(JumpForce, ForceMode.Impulse);
            }
        }


        //���͏I����
        //���V�t�g(�_�b�V��)

        // W�L�[�i�O���ړ��j
        if (Input.GetKeyUp(KeyCode.W))
        {
            Forward = false;



        }

        // S�L�[�i����ړ��j
        if (Input.GetKeyUp(KeyCode.S))
        {
            Back = false;

        }

        // D�L�[�i�E�ړ��j
        if (Input.GetKeyUp(KeyCode.D))
        {
            Right = false;
        }

        // A�L�[�i���ړ��j
        if (Input.GetKeyUp(KeyCode.A))
        {
            Left = false;


        }



        //�ȏ�L�[����


        //�ȉ��L�[���͂ɂ��A�j���[�V����/�ړ�/���̌�������

        if (Forward && Right == false && Left == false)//��
        {
            anim.SetBool("Forward_Anim", true);
            Legs.transform.localEulerAngles = new Vector3(0, 0, 0);//���̌��������[�J���ŕύX���ē��͂ɍ��킹��
            transform.position += transform.forward * walk * Time.deltaTime;//���s���x�ɐ��ʃx�N�g���������ċO���C��
        }

        if (Right && Forward == false && Back == false)//��
        {
            anim.SetBool("Forward_Anim", true);
            Legs.transform.localEulerAngles = new Vector3(-90, 0, 0);//���̌��������[�J���ŕύX���ē��͂ɍ��킹��
            transform.position += transform.right * walk * Time.deltaTime;

        }
        if (Left && Forward == false && Back == false)//��
        {
            anim.SetBool("Forward_Anim", true);
            Legs.transform.localEulerAngles = new Vector3(90, 0, 0);//���̌��������[�J���ŕύX���ē��͂ɍ��킹��
            transform.position += -transform.right * walk * Time.deltaTime;
        }




        if (Forward && Right && Left == false)//����
        {
            anim.SetBool("Forward_Anim", true);
            Legs.transform.localEulerAngles = new Vector3(-45, 0, 0);//���̌��������[�J���ŕύX���ē��͂ɍ��킹��
            transform.position += (transform.forward + transform.right) * walk * Time.deltaTime;//���s���x�ɐ��ʃx�N�g���������ċO���C��
        }
        if (Forward && Right == false && Left)//����
        {
            anim.SetBool("Forward_Anim", true);
            Legs.transform.localEulerAngles = new Vector3(45, 0, 0);//���̌��������[�J���ŕύX���ē��͂ɍ��킹��
            transform.position += (transform.forward - transform.right) * walk * Time.deltaTime;//���s���x�ɐ��ʃx�N�g���������ċO���C��
        }


        if (Back && Right == false && Left == false)//��
        {
            anim.SetBool("Back_Anim", true);
            Legs.transform.localEulerAngles = new Vector3(0, 0, 0);//���̌��������[�J���ŕύX���ē��͂ɍ��킹��
            transform.position += -transform.forward * walk * Time.deltaTime;//���s���x�ɐ��ʃx�N�g���������ċO���C��
        }



        if (Back && Right && Left == false)//����
        {
            anim.SetBool("Back_Anim", true);
            Legs.transform.localEulerAngles = new Vector3(45, 0, 0);
            transform.position += (-transform.forward + transform.right) * walk * Time.deltaTime;//���s���x�ɐ��ʃx�N�g���������ċO���C��
        }
        if (Back && Left && Right == false)//����
        {
            anim.SetBool("Back_Anim", true);
            Legs.transform.localEulerAngles = new Vector3(-45, 0, 0);
            transform.position += (-transform.forward - transform.right) * walk * Time.deltaTime;//���s���x�ɐ��ʃx�N�g���������ċO���C��
        }

        if (Forward == false && Right == false && Left == false)//�A�j���[�V�����I��
        {
            anim.SetBool("Forward_Anim", false);
        }
        if (Back == false)//�A�j���[�V�����I��
        {
            anim.SetBool("Back_Anim", false);
        }
        if (Forward == false && Right == false && Left == false && Back == false)
        {
            Legs.transform.localEulerAngles = new Vector3(0, 0, 0);//���̌��������[�J���ŕύX���ē��͂ɍ��킹��
        }


    }

    public void Turn_around()
    {




        // Cursor.lockState = CursorLockMode.Locked;




        //�}�E�X�̈ړ��ŃJ��������]������ƁAX��Y��������Ƃ�₱�������ƂɂȂ�@


        var Came_Y = Input.GetAxis("Mouse X") * 2;//�}�W�b�N�i���o�[�͌�ŕϐ��ɂ���(�ŏI�I�ȉ�ʐݒ�ɔ����Č��߂�)


        var Came_X = Input.GetAxis("Mouse Y") * -2;


        if (Input.GetAxis("Mouse X") != 0)
        {
            transform.Rotate(new Vector3(0, Came_Y, 0));//�L�����N�^�[�̑̂��ƌ�����ς���

        }

        if (Input.GetAxis("Mouse Y") != 0)
        {

            CameraPoint_X.transform.Rotate(new Vector3(Came_X, 0, 0));//�J�������|�C���g�𒆐S�ɏc��]
            var Wrote = waist.localEulerAngles;//�����擾
            waist.transform.localEulerAngles = new Vector3(Wrote.x, Wrote.y + Came_X, Wrote.z);//�J�����ɍ��킹�ċ����ɂȂ�

        }

    }


    public void Shot()
    {
        if (Input.GetMouseButton(0))
        {
            GunScript.Charge();
        }


        if (Input.GetMouseButtonUp(0))
        {
            GunScript.BulletShot();

        }

        if (Input.GetMouseButtonUp(1))
        {
            GunScript.Wire_Shot();

        }






    }







    public void AirToIdle()//�W�����v�I���㏈��
    {
        anim.SetTrigger("Idle");

    }



    public void PostJump_processing()//�W�����v���㏈��
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

        return V_min + (V_max - V_min) / ((R_max - R_min) / (value - R_min));//value��V_min����V_Max�͈̔͂���R_min����R_max�͈̔͂ɂ���

    }


    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag=="Bullet")
        {

            var BulletSC = other.gameObject.GetComponent<Bullet>();
            HP -= BulletSC.Bullet_Attack[(int)BulletSC.bulletMode];
        }
    }



}
