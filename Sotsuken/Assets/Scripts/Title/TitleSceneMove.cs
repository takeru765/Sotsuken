using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleSceneMove : MonoBehaviour
{
    //音声関連
    AudioSource audioSource;
    [SerializeField] AudioClip titleEnter;

    //フェードアウト関連
    [SerializeField] GameObject fadeObject;
    [SerializeField] Image fadeImage;
    //bool isFadeIn = true; //フェードイン中フラグ
    bool isFadeOut = false; //フェードアウト中フラグ
    float alfa = 0f; //フェード画像の不透明度

    void FadeOut(float speed = 0.75f)
    {
        fadeImage.raycastTarget = true; //フェード画像のクリック判定を有効化(各種ボタンをクリックできないようにする)

        alfa += speed * Time.deltaTime; //時間経過に応じて不透明度を増加
        fadeImage.color = new Color(0, 0, 0, alfa); //不透明度を反映

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
