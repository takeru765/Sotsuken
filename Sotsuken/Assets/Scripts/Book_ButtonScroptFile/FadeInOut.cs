using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeInOut : MonoBehaviour
{
    float alfa = 1f; //画像の不透明度に使用
    Image fade; //フェードイン・アウト用の黒画像
    bool isFadeIn = true; //フェードイン中
    bool isFadeOut = false; //フェードアウト中

    void FadeIn(float speed = 0.75f) //フェードイン処理。フェード速度も指定可能
    {
        alfa -= speed * Time.deltaTime; //時間経過に応じて不透明度を低下
        fade.color = new Color(0, 0, 0, alfa); //不透明度を反映

        if (alfa < 0)
        {
            alfa = 0;
            isFadeIn = false;

            fade.raycastTarget = false; //フェード画像のクリック判定を無効化(下にあるボタンをクリックさせるため)
        }
    }

    void FadeOut(float speed = 0.75f)  //フェードアウト処理。フェード速度も指定可能
    {
        fade.raycastTarget = true; //フェード画像のクリック判定を有効化(各種ボタンをクリックできないようにする)

        alfa += speed * Time.deltaTime; //時間経過に応じて不透明度を増加
        fade.color = new Color(0, 0, 0, alfa); //不透明度を反映

        if (alfa >= 1)
        {
            alfa = 1;
            isFadeOut = false;

            SceneManager.LoadScene("Oka_test");
        }
    }

    public void StartFadeOut() //フェードアウト開始
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
