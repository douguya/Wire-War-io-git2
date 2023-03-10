using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class CharactreContorol : MonoBehaviour
{
    //�ȉ��f�o�b�O�p�ϐ�
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
    //�f�o�b�O�p�ϐ������܂�



    public Transform waist;//���̃I�u�W�F�N�g
    public Transform Legs; //���̑��̃I�u�W�F�N�g
    public GameObject Gun;�@//�e�̃I�u�W�F�N�g


    //�ȉ����s�pbool�l
    bool Right = false;
    bool Left = false;
    bool Forward = false;
    bool Back = false;
    //���s�pbool�l�����܂�




    public int walk;//���s���x
    public Vector3 JumpForce;//�W�����v��
    public bool CanJump = true;//�W�����v����
    Rigidbody Rb;//�@���s/����@���Ɏg�p���镨�����Z�R���|�[�l���g



    public int Vewing_distance;//�e��Ray�̋����@���ˎ��̗L���˒�����



    public GameObject Body;//�v���C���[�̑�
    public GameObject FutPoint;//�ڒn����p�̃I�u�W�F�N�g
    public GameObject CameraPoint_X;//�J�����̎�
    public GameObject Camera;//�J����
    private Camera CharacterCamera;
    private Vector3[] Mouse = new Vector3[2];  //�}�E�X�̈ʒu(�ω��O�ƌ���)
    public Animator anim;  //Animator��anim�Ƃ����ϐ��Œ�`����

    public GameObject Bullet;
  

    Ray Arm_Ray;//�{�Ƃ�GunRay�����A�I�u�W�F�N�g�Ƃ킩��₷�������邽�߂�Arm�ɂ���







    // Start is called before the first frame update
    void Start()
    {

        Mouse[0] = Input.mousePosition;//mouse�̍��W�̎擾
        Rb = this.GetComponent<Rigidbody>();
        CharacterCamera = Camera.GetComponent<Camera>();
      
    }

    // Update is called once per frame
    void Update()
    {
        TargetSift();//�e����o��Ray�̏Փˈʒu�ɏƏ���������
        Move_();//WASD.space�L�[�̓��͂Ƃ���ɂ��ړ�

    }

    void LateUpdate()
    {
        Turn_around();//�}�E�X�̓����ɂ��̂̌����̕ω�
        Shot();
      // Aim();//�Ə�
    }

   
    public void TargetSift()
    {

        Arm_Ray = new Ray(Gun.transform.position, Gun.transform.right * Vewing_distance); //�e(�r)����Ray���΂�



        RaycastHit hit;
        if (Physics.Raycast(Arm_Ray, out hit))//Ray���I�u�W�F�N�g�ɓ������Ă���ꍇ
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


        
        if (Forward  && Right==false && Left==false)//��
        {
            anim.SetBool("Forward_Anim", true);
            Legs.transform.localEulerAngles = new Vector3(0,0, 0);//���̌��������[�J���ŕύX���ē��͂ɍ��킹��
          �@transform.position+=transform.forward*walk*Time.deltaTime;//���s���x�ɐ��ʃx�N�g���������ċO���C��
        }

        if (Right && Forward==false && Back==false)//��
        {
            anim.SetBool("Forward_Anim", true);
            Legs.transform.localEulerAngles = new Vector3(-90, 0,0);//���̌��������[�J���ŕύX���ē��͂ɍ��킹��
            transform.position += transform.right * walk * Time.deltaTime;

        }
        if (Left && Forward==false && Back==false)//��
        {
            anim.SetBool("Forward_Anim", true);
            Legs.transform.localEulerAngles = new Vector3(90, 0,0);//���̌��������[�J���ŕύX���ē��͂ɍ��킹��
            transform.position += -transform.right * walk * Time.deltaTime;
        }


       

        if (Forward && Right && Left==false)//����
        {
            anim.SetBool("Forward_Anim", true);
            Legs.transform.localEulerAngles = new Vector3(-45, 0, 0);//���̌��������[�J���ŕύX���ē��͂ɍ��킹��
            transform.position += (transform.forward+transform.right) * walk * Time.deltaTime;//���s���x�ɐ��ʃx�N�g���������ċO���C��
        }
        if (Forward && Right==false && Left)//����
        {
            anim.SetBool("Forward_Anim", true);
            Legs.transform.localEulerAngles = new Vector3(45, 0, 0);//���̌��������[�J���ŕύX���ē��͂ɍ��킹��
            transform.position += (transform.forward -transform.right) * walk * Time.deltaTime;//���s���x�ɐ��ʃx�N�g���������ċO���C��
        }


        if (Back && Right == false && Left == false)//��
        {
            anim.SetBool("Back_Anim", true);
            Legs.transform.localEulerAngles = new Vector3(0, 0, 0);//���̌��������[�J���ŕύX���ē��͂ɍ��킹��
            transform.position += -transform.forward * walk * Time.deltaTime;//���s���x�ɐ��ʃx�N�g���������ċO���C��
        }



        if (Back && Right && Left==false)//����
        {
            anim.SetBool("Back_Anim", true);
            Legs.transform.localEulerAngles = new Vector3(45, 0, 0);
            transform.position += (-transform.forward + transform.right) * walk * Time.deltaTime;//���s���x�ɐ��ʃx�N�g���������ċO���C��
        }
        if (Back &&  Left && Right ==false)//����
        { anim.SetBool("Back_Anim", true);
            Legs.transform.localEulerAngles = new Vector3(-45, 0, 0);
            transform.position += (-transform.forward - transform.right) * walk * Time.deltaTime;//���s���x�ɐ��ʃx�N�g���������ċO���C��
        }

        if (Forward == false && Right == false && Left == false)//�A�j���[�V�����I��
        {
            anim.SetBool("Forward_Anim", false);
        }
        if (Back==false)//�A�j���[�V�����I��
        {
            anim.SetBool("Back_Anim",false);
        }
        if (Forward == false && Right == false && Left == false && Back == false)
        { 
            Legs.transform.localEulerAngles = new Vector3(0, 0, 0);//���̌��������[�J���ŕύX���ē��͂ɍ��킹��
        }

    }

    public void Turn_around()
    {

        Mouse[1] = Input.mousePosition;//mouse�̍��W�̎擾
        if (Mouse[0] != Mouse[1])//�}�E�X���ړ����Ă��邩�ǂ���
        {
            //�}�E�X�̈ړ��ŃJ��������]������ƁAX��Y��������Ƃ�₱�������ƂɂȂ�@

            var Came_Y = Mouse[0].x - Mouse[1].x;
            Came_Y = (this.transform.rotation.y + Came_Y) * 0.05f;//�@ 0.05f�@�}�E�X���x�@��ŕϐ��ɂ���

            var Came_X = Mouse[0].y - Mouse[1].y;
            Came_X = (CameraPoint_X.transform.rotation.x + Came_X) * 0.05f;



            transform.Rotate(new Vector3(0, -Came_Y, 0));//�L�����N�^�[�̑̂��ƌ�����ς���


            CameraPoint_X.transform.Rotate(new Vector3(Came_X, 0, 0));//�J�������|�C���g�𒆐S�ɏc��]


            var Wrote = waist.localEulerAngles;

            waist.transform.localEulerAngles = new Vector3(Wrote.x, Wrote.y + Came_X, Wrote.z);



            Mouse[0] = Mouse[1];//�}�E�X���W�̃��Z�b�g
        }

    }


    public void Shot()
    {
        if (Input.GetMouseButtonUp(0))
        {

            Instantiate(Bullet, Gun.transform.position, Gun.transform.rotation);//�e�ۂ̃C���X�^���X�𔭎˃|�C���g���甭��
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

}
