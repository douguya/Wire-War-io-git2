using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManagerSC : MonoBehaviour
{
    public string Title;        //
    public string lobby;
    public static string Lobysend; //
    public string Game;
    public static string Gamesend;
    public string Result;      //


    float delay=0.4f;

    // Start is called before the first frame update
    void Start()
    {
        // Debug.developerConsoleVisible = false;
        Gamesend = Game;
        Lobysend = lobby;
    }

    // Update is called once per frame
    void Update()
    {




    }

    public void TransitionToTitle()//�^�C�g���ɔ��
    {


        StartCoroutine(Scene_transition(Title));

    }

    public void TransitionTolobby()//���r�[�ɔ��
    {
        StartCoroutine(Scene_transition(lobby));
    }



    public void TransitionToGame()//�Q�[���V�[���ɔ��
    {
        StartCoroutine(Scene_transition(Game));
    }

    public void TransitionToResult()//���U���g�ɔ��
    {

        StartCoroutine(Scene_transition(Result));
    }


    public IEnumerator Scene_transition(string Sene)//String�̃V�[���ɔ�ԁ@
    {

        yield return new WaitForSeconds(delay);//�V�[���J�ډ��̕��ҋ@
        SceneManager.LoadScene(Sene);

        yield break;
    }

    public void Finish()
    {
        Application.Quit();
    }




}
