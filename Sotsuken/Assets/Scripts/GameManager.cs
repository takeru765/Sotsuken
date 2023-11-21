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

    //日付変更チェック
    int day = 0;
    int month = 0;
    int year = 0;
    void CheckDate()
    {
        if(time.Year > year || time.Month > month || time.Day > day)
        {
            //各種数値・フラグをリセット
            setMission = false;
            successMission = false;
            count = 0;
            todayAlumi = 0;
            todaySteal = 0;
            todayPet = 0;
            todayPla = 0;
            todayPaper = 0;

            //日付を更新
            year = time.Year;
            month = time.Month;
            day = time.Day;
        }
    }

    //UI(ウィンドウ)関連
    [SerializeField] GameObject BuildWindow; //建設用ウィンドウ
    [SerializeField] GameObject MissionWindow; //ミッション用ウィンドウ
    [SerializeField] GameObject InputWindow; //リサイクルマーク入力用ウィンドウ
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

            //ミッションの成功・失敗を判定
            CheckMission();
            CheckDate();
        }
    }

    public void CloseMission()
    {
        MissionWindow.SetActive(false);
        open = false;
    }

    //リサイクルマーク入力ウィンドウの開閉
    public void OpenInput()
    {
        if (open == false)
        {
            InputWindow.SetActive(true);
            open = true;
        }
    }

    public void CloseInput()
    {
        InputWindow.SetActive(false);
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
    int place_1 = 0; //建築場所その1の建物
    int lv_1 = 0; //建築場所その1のレベル
    int place_2 = 0;
    int lv_2 = 0;
    int place_3 = 0;
    int lv_3 = 0;
    int place_4 = 0;
    int lv_4 = 0;
    int place_5 = 0;
    int lv_5 = 0;
    int place_6 = 0;
    int lv_6 = 0;

    //建築によるボーナス
    float inputRate = 1f; //マーク入力倍率
    float eventRate = 1f; //イベント倍率

    //建築ボーナスの再計算
    void calcBuildBonus()
    {
        //各種ボーナスの初期化
        inputRate = 1f;
        eventRate = 1f;

        //建築フラグを元に再計算
        switch(place_1)
        {
            case 1:
                inputRate += 0.2f * lv_1;
                break;
            case 2:
                eventRate += 0.2f * lv_1;
                break;
            default:
                break;
        }
        switch (place_2)
        {
            case 1:
                inputRate += 0.2f * lv_2;
                break;
            case 2:
                eventRate += 0.2f * lv_2;
                break;
            default:
                break;
        }
        switch (place_3)
        {
            case 1:
                inputRate += 0.2f * lv_3;
                break;
            case 2:
                eventRate += 0.2f * lv_3;
                break;
            default:
                break;
        }
        switch (place_4)
        {
            case 1:
                inputRate += 0.2f * lv_4;
                break;
            case 2:
                eventRate += 0.2f * lv_4;
                break;
            default:
                break;
        }
        switch (place_5)
        {
            case 1:
                inputRate += 0.2f * lv_5;
                break;
            case 2:
                eventRate += 0.2f * lv_5;
                break;
            default:
                break;
        }
        switch (place_6)
        {
            case 1:
                inputRate += 0.2f * lv_6;
                break;
            case 2:
                eventRate += 0.2f * lv_6;
                break;
            default:
                break;
        }
    }


    //リサイクルマーク入力関連
    int tmpAlumi = 0; //入力途中の一時保存用
    int tmpSteal = 0;
    int tmpPet = 0;
    int tmpPla = 0;
    int tmpPaper = 0;

    [SerializeField] TextMeshProUGUI alumiText; //入力途中の表示用
    [SerializeField] TextMeshProUGUI stealText;
    [SerializeField] TextMeshProUGUI petText;
    [SerializeField] TextMeshProUGUI plaText;
    [SerializeField] TextMeshProUGUI paperText;

    //マーク増加ボタン
    public void AddInputMark(int mark) //markは1=アルミ缶、2=スチール缶、3=ペットボトル、4=プラスチック、5=紙
    {
        switch(mark)
        {
            case 1:
                tmpAlumi += 1;
                //一応、上限を10にしておく
                if(tmpAlumi > 10)
                {
                    tmpAlumi = 10;
                }

                //テキスト同期
                if (tmpAlumi < 10)
                {
                    alumiText.text = string.Format(" {0:G}", tmpAlumi);
                }
                else
                {
                    alumiText.text = string.Format("{0:G}", tmpAlumi);
                }
                break;

            case 2:
                tmpSteal += 1;
                //一応、上限を10にしておく
                if (tmpSteal > 10)
                {
                    tmpSteal = 10;
                }

                //テキスト同期
                if (tmpSteal < 10)
                {
                    stealText.text = string.Format(" {0:G}", tmpSteal);
                }
                else
                {
                    stealText.text = string.Format("{0:G}", tmpSteal);
                }
                break;

            case 3:
                tmpPet += 1;
                //一応、上限を10にしておく
                if (tmpPet > 10)
                {
                    tmpPet = 10;
                }

                //テキスト同期
                if (tmpPet < 10)
                {
                    petText.text = string.Format(" {0:G}", tmpPet);
                }
                else
                {
                    petText.text = string.Format("{0:G}", tmpPet);
                }
                break;

            case 4:
                tmpPla += 1;
                //一応、上限を10にしておく
                if (tmpPla > 10)
                {
                    tmpPla = 10;
                }

                //テキスト同期
                if (tmpPla < 10)
                {
                    plaText.text = string.Format(" {0:G}", tmpPla);
                }
                else
                {
                    plaText.text = string.Format("{0:G}", tmpPla);
                }
                break;

            case 5:
                tmpPaper += 1;
                //一応、上限を10にしておく
                if (tmpPaper > 10)
                {
                    tmpPaper = 10;
                }

                //テキスト同期
                if (tmpPaper < 10)
                {
                    paperText.text = string.Format(" {0:G}", tmpPaper);
                }
                else
                {
                    paperText.text = string.Format("{0:G}", tmpPaper);
                }
                break;

            default:
                break;
        }
    }

    //マーク減少ボタン
    public void MinusInputMark(int mark) //markは1=アルミ缶、2=スチール缶、3=ペットボトル、4=プラスチック、5=紙
    {
        switch (mark)
        {
            case 1:
                tmpAlumi -= 1;
                //一応、下限を0にしておく
                if (tmpAlumi < 0)
                {
                    tmpAlumi = 0;
                }

                //テキスト同期
                if (tmpAlumi < 10)
                {
                    alumiText.text = string.Format(" {0:G}", tmpAlumi);
                }
                else
                {
                    alumiText.text = string.Format("{0:G}", tmpAlumi);
                }
                break;

            case 2:
                tmpSteal -= 1;
                
                if (tmpSteal < 0)
                {
                    tmpSteal = 0;
                }

                //テキスト同期
                if (tmpSteal < 10)
                {
                    stealText.text = string.Format(" {0:G}", tmpSteal);
                }
                else
                {
                    stealText.text = string.Format("{0:G}", tmpSteal);
                }
                break;

            case 3:
                tmpPet -= 1;
                
                if (tmpPet < 0)
                {
                    tmpPet = 0;
                }

                //テキスト同期
                if (tmpPet < 10)
                {
                    petText.text = string.Format(" {0:G}", tmpPet);
                }
                else
                {
                    petText.text = string.Format("{0:G}", tmpPet);
                }
                break;

            case 4:
                tmpPla -= 1;
                
                if (tmpPla < 0)
                {
                    tmpPla = 0;
                }

                //テキスト同期
                if (tmpPla < 10)
                {
                    plaText.text = string.Format(" {0:G}", tmpPla);
                }
                else
                {
                    plaText.text = string.Format("{0:G}", tmpPla);
                }
                break;

            case 5:
                tmpPaper -= 1;
                
                if (tmpPaper < 0)
                {
                    tmpPaper = 0;
                }

                //テキスト同期
                if (tmpPaper < 10)
                {
                    paperText.text = string.Format(" {0:G}", tmpPaper);
                }
                else
                {
                    paperText.text = string.Format("{0:G}", tmpPaper);
                }
                break;

            default:
                break;
        }
    }

    public void InputEnter() //リサイクルマーク入力を確定
    {
        //各マークのポイントと当日の入力数を反映
        alumiPoint += (int) (tmpAlumi * 5 * inputRate);
        stealPoint += (int) (tmpSteal * 5 * inputRate);
        petPoint += (int) (tmpPet * 5 * inputRate);
        plaPoint += (int) (tmpPla * 5 * inputRate);
        paperPoint += (int) (tmpPaper * 5 * inputRate);
        todayAlumi += tmpAlumi;
        todaySteal += tmpSteal;
        todayPet += tmpPet;
        todayPla += tmpPla;
        todayPaper += tmpPaper;
        allPoint += (int)((tmpAlumi + tmpSteal + tmpPet + tmpPla + tmpPaper) * 5 * inputRate);

        //tmp○○を初期化
        tmpAlumi = 0;
        tmpSteal = 0;
        tmpPet = 0;
        tmpPla = 0;
        tmpPaper = 0;
        //表示UIに反映
        alumiText.text = string.Format(" {0:G}", tmpAlumi);
        stealText.text = string.Format(" {0:G}", tmpSteal);
        petText.text = string.Format(" {0:G}", tmpPet);
        plaText.text = string.Format(" {0:G}", tmpPla) ;
        paperText.text = string.Format(" {0:G}", tmpPaper);

        //デバッグ表示
        Debug.Log("アルミ=" + alumiPoint + "、スチール=" + stealPoint + "、ペットボトル=" + petPoint + "、プラスチック=" + plaPoint + "、紙=" + paperPoint);
    }

    //イベント関連


    //ミッション関連
    int goal = 1; //設定した目標
    int count = 0; //現在のマーク獲得数
    int mark = 1; //設定したマークの種類 1～5=アルミ、スチール、ペット、プラ、紙
    int markRec1 = 0; //前日のマークの種類
    int markRec2 = 0; //前々日のマークの種類
    [SerializeField] TextMeshProUGUI goalText; //目標の数値テキスト
    bool setMission = false; //ミッション確定済みフラグ
    bool successMission = false; //ミッション達成済みフラグ
    int setYear = 10000; //ミッション確定年
    int setMonth = 10000; //ミッション確定月
    int setDay = 10000; //ミッション確定日

    [SerializeField] GameObject MissionSuccess; //ミッション成功メッセージ
    [SerializeField] GameObject MissionFailed; //ミッション失敗メッセージ

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
            default:
                break;
        }

        if(setMission == true && count >= goal && successMission == false)
        {
            //ここにミッション成功時の処理を入れる
            SuccessMission();
        }

        if (time.Year > setYear || time.Month > setMonth || time.Day > setDay) //翌日になってるか判定
        {
            if(setMission ==true && successMission ==false)
            {
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
            reward += i * 5;
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
        allPoint += reward;

        //前日、前々日のマークを更新
        markRec2 = markRec1;
        markRec1 = mark;

        //ミッション達成フラグをON
        successMission = true;

        //ミッション成功ウィンドウを表示
        MissionSuccess.SetActive(true);
    }

    //ミッション失敗処理
    void FailedMission()
    {
        int reward = 0;

        //獲得ポイントの計算
        for(int i = 1; i < count; i++)
        {
            reward += i * 5;
        }

        reward += count * 5 / 2;

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

        //ミッション失敗ウィンドウを表示
        MissionFailed.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        time = DateTime.Now;

        day = time.Day;
        month = time.Month;
        year = time.Year;

        Debug.Log(year + "/" + month + "/" + day);
    }

    // Update is called once per frame
    void Update()
    {
        //現在日時を取得。処理が重い場合、別の場所(画面切り替え時等)に移すかも
        time = DateTime.Now;
    }
}
