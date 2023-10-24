using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    //現在日時を保存
    DateTime time;

    //UI(ウィンドウ)関連
    [SerializeField] GameObject BuildWindow; //建設用ウィンドウ
    [SerializeField] GameObject MissionWindow; //ミッション用ウィンドウ
    bool open = false; //既に何らかのウィンドウが開いているフラグ

    //建設ウィンドウの開閉
    public void OpenBuild()
    {
        if(open == false)
        {
            BuildWindow.SetActive(true);
            open = true;
        }
    }

    public void CloseBuild()
    {
        BuildWindow.SetActive(false);
        open = false;
    }

    //ミッションウィンドウの開閉
    public void OpenMission()
    {
        if(open == false)
        {
            MissionWindow.SetActive(true);
            open = true;
        }
    }

    public void CloseMission()
    {
        MissionWindow.SetActive(false);
        open = false;
    }

    //各種ポイント
    int alumiPoint = 0; //アルミ缶
    int stealPoint = 0; //スチール缶
    int petPoint = 0; //ペットボトル
    int plaPoint = 0; //プラスチック
    int paperPoint = 0; //紙製容器包装
    int allPoint = 0; //累計ポイント

    //当日獲得したポイント(ミッション用)
    int todayAlumi = 0; //アルミ缶
    int todaySteal = 0; //スチール缶
    int todayPet = 0; //ペットボトル
    int todayPla = 0; //プラスチック
    int todayPaper = 0; //紙

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
    int goal = 1; //設定した目標
    int count = 0; //現在のマーク獲得数
    int mark = 0; //設定したマークの種類 1～5=アルミ、スチール、ペット、プラ、紙
    int markRec1 = 0; //前日のマークの種類
    int markRec2 = 0; //前々日のマークの種類
    [SerializeField] TextMeshProUGUI goalText; //目標の数値テキスト
    bool setMission = false; //ミッション確定済みフラグ
    int setYear = 0; //ミッション確定年
    int setMonth = 0; //ミッション確定月
    int setDay = 0; //ミッション確定日

    //ミッションのマークを設定する。
    public void SetMark(int m)
    {
        if(!setMission)
        {
            mark = m;
        }

        Debug.Log("mark = " + mark);
    }

    //ミッションの目標数を増減する
    public void AddReason(int i)
    {
        if(!setMission)
        {
            goal += i;
        }

        //目標の上限下限を超えないようにする
        if(goal < 1)
        {
            goal = 1;
        }

        if(goal > 10)
        {
            goal = 10;
        }

        //UI上の目標の表示を変更する
        if(goal < 10)
        {
            goalText.text = string.Format(" {0:G}", goal);
        }
        else
        {
            goalText.text = string.Format("{0:G}", goal);
        }
    }

    //ミッション確定
    public void SetMission()
    {
        setMission = true;

        //今日の日付を保存
        setYear = time.Year;
        setMonth = time.Month;
        setDay = time.Day;
    }

    //ミッション達成判定
    void CheckMission()
    {
        switch(mark)
        {
            case 1:
                count = todayAlumi;
                break;
            case 2:
                count = todaySteal;
                break;
            case 3:
                count = todayPet;
                break;
            case 4:
                count = todayPla;
                break;
            case 5:
                count = todayPaper;
                break;
        }

        if(count >= goal)
        {
            //ここにミッション成功時の処理を入れる
            SuccessMission();
        }
        else
        {
            //翌日になってるか判定
            if(time.Year > setYear || time.Month > setMonth || time.Day > setDay)
            {
                //ここに失敗時の処理を入れる
                FailedMission();
            }
        }

        Debug.Log("ポイントは" + alumiPoint + "," + stealPoint + "," + plaPoint + "," + petPoint + "," + paperPoint);
    }

    //ミッション成功処理
    void SuccessMission()
    {
        int reward = 0;

        //獲得ポイントを計算
        for(int i = 1; i <= goal; i++)
        {
            reward += i;
        }

        //設定したマークにポイント加算
        switch(mark)
        {
            case 1:
                alumiPoint += reward;
                break;
            case 2:
                stealPoint += reward;
                break;
            case 3:
                petPoint += reward;
                break;
            case 4:
                plaPoint += reward;
                break;
            case 5:
                paperPoint += reward;
                break;
        }

        //前日、前々日のマークを更新
        markRec2 = markRec1;
        markRec1 = mark;

        //ミッション決定フラグを解除
        setMission = false;
    }

    //ミッション失敗処理
    void FailedMission()
    {
        int reward = 0;

        //獲得ポイントの計算
        for(int i = 1; i < count; i++)
        {
            reward += i;
        }

        reward += count / 2;

        //設定したマークにポイントを加算
        switch (mark)
        {
            case 1:
                alumiPoint += reward;
                break;
            case 2:
                stealPoint += reward;
                break;
            case 3:
                petPoint += reward;
                break;
            case 4:
                plaPoint += reward;
                break;
            case 5:
                paperPoint += reward;
                break;
        }

        //前日、前々日のマークを更新
        markRec2 = markRec1;
        markRec1 = mark;

        //ミッション決定フラグを解除
        setMission = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //現在日時を取得。処理が重い場合、別の場所(画面切り替え時等)に移すかも
        time = DateTime.Now;
    }
}
