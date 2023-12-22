using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleSceneMove : MonoBehaviour
{
    //�����֘A
    AudioSource audioSource;
    [SerializeField] AudioClip titleEnter;

    //�t�F�[�h�A�E�g�֘A
    [SerializeField] GameObject fadeObject;
    [SerializeField] Image fadeImage;
    //bool isFadeIn = true; //�t�F�[�h�C�����t���O
    bool isFadeOut = false; //�t�F�[�h�A�E�g���t���O
    float alfa = 0f; //�t�F�[�h�摜�̕s�����x

    void FadeOut(float speed = 0.75f)
    {
        fadeImage.raycastTarget = true; //�t�F�[�h�摜�̃N���b�N�����L����(�e��{�^�����N���b�N�ł��Ȃ��悤�ɂ���)

        alfa += speed * Time.deltaTime; //���Ԍo�߂ɉ����ĕs�����x�𑝉�
        fadeImage.color = new Color(0, 0, 0, alfa); //�s�����x�𔽉f

        if (alfa >= 1)
        {
            alfa = 1;
            isFadeOut = false;

            SceneManager.LoadScene("Oka_test");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isFadeOut == false)
        {
            isFadeOut = true;
            audioSource.PlayOneShot(titleEnter);
        }

        if (isFadeOut == true)
        {
            FadeOut();
        }
    }
}
