using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //各種ポイント
    int alumiPoint = 0; //アルミ缶
    int stealPoint = 0; //スチール缶
    int petPoint = 0; //ペットボトル
    int plaPoint = 0; //プラスチック
    int paperPoint = 0; //紙製容器包装
    int allPoint = 0; //累計ポイント

    //建築フラグ等
    int place_A = 0; //建築場所その1の建物
    int lv_A = 0; //建築場所その1のレベル
    int place_B = 0;
    int lv_B = 0;
    int place_C = 0;
    int lv_C = 0;
    int place_D = 0;
    int lv_D = 0;
    int place_E = 0;
    int lv_E = 0;
    int place_F = 0;
    int lv_F = 0;

    //イベント関連


    //ミッション関連
    int goal = 0; //設定した目標
    int count = 0; //現在のマーク獲得数
    int mark = 0; //設定したマークの種類
    int markRec1 = 0; //前日のマークの種類
    int markRec2 = 0; //前々日のマークの種類

    //ミッションのマークを設定する。
    public void SetMark(int m)
    {
        mark = m;
        Debug.Log("mark = " + mark);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
