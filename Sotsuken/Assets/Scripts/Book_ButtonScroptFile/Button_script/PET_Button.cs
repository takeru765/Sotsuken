using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PET_Button : MonoBehaviour
{
    public Counter_test TrashPoint;//現在は仮のスクリプト。Gamemanegerから個別で登録した個数を引用する
    //public Sprite trashSprite;
    //private Image image;

    int Btrash_pet;//引用した数値を入れる変数

    void Start()
    {
        Button btn = GetComponent<Button>();
        btn.interactable = false;//ボタン機能を無効にする

       // image = GetComponent<Image>();

    }



    // Update is called once per frame
    void Update()
    {
        //Btrash = TrashPoint.trash;
        Btrash_pet = TrashPoint.pet;//引用した数値を入れる変数

        if (Btrash_pet >= 1)//仮の数値。登録したごみが１個以上になった場合に、図鑑の項目が出現するようになる。
        {
            Button btn = GetComponent<Button>();//インスペクターのボタンオブジェクトで指定したボタンの解放、このままではほかのオブジェクトも同時に処理してしまうので、もっと細かい指定が必要。
            btn.interactable = true;

            //image.sprite = trashSprite;//画像を変える
        }

    }
}