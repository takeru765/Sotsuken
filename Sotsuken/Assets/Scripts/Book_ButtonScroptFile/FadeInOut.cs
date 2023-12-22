using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeInOut : MonoBehaviour
{
    float alfa = 1f; //�摜�̕s�����x�Ɏg�p
    Image fade; //�t�F�[�h�C���E�A�E�g�p�̍��摜
    bool isFadeIn = true; //�t�F�[�h�C����
    bool isFadeOut = false; //�t�F�[�h�A�E�g��

    void FadeIn(float speed = 0.75f) //�t�F�[�h�C�������B�t�F�[�h���x���w��\
    {
        alfa -= speed * Time.deltaTime; //���Ԍo�߂ɉ����ĕs�����x��ቺ
        fade.color = new Color(0, 0, 0, alfa); //�s�����x�𔽉f

        if (alfa < 0)
        {
            alfa = 0;
            isFadeIn = false;

            fade.raycastTarget = false; //�t�F�[�h�摜�̃N���b�N����𖳌���(���ɂ���{�^�����N���b�N�����邽��)
        }
    }

    void FadeOut(float speed = 0.75f)  //�t�F�[�h�A�E�g�����B�t�F�[�h���x���w��\
    {
        fade.raycastTarget = true; //�t�F�[�h�摜�̃N���b�N�����L����(�e��{�^�����N���b�N�ł��Ȃ��悤�ɂ���)

        alfa += speed * Time.deltaTime; //���Ԍo�߂ɉ����ĕs�����x�𑝉�
        fade.color = new Color(0, 0, 0, alfa); //�s�����x�𔽉f

        if (alfa >= 1)
        {
            alfa = 1;
            isFadeOut = false;

            SceneManager.LoadScene("Oka_test");
        }
    }

    public void StartFadeOut() //�t�F�[�h�A�E�g�J�n
    {
        isFadeOut = true;
    }

    private void Awake()
    {
        fade = this.gameObject.GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isFadeIn == true)
        {
            FadeIn();
        }
        else if(isFadeOut == true)
        {
            FadeOut();
        }
    }
}
