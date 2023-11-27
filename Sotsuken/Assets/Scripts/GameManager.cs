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

            //イベントボーナスをリセット
            eventBonus = 1.0f;
        }
    }

    //UI(ポイント表示)関連
    [SerializeField] TextMeshProUGUI alumiViewer;
    [SerializeField] TextMeshProUGUI stealViewer;
    [SerializeField] TextMeshProUGUI petViewer;
    [SerializeField] TextMeshProUGUI plaViewer;
    [SerializeField] TextMeshProUGUI paperViewer;
    [SerializeField] TextMeshProUGUI allViewer;

    //各ポイント表示の変更
    void AlumiViewerChange(int i)
    {
        alumiViewer.text = "アルミ　" + string.Format(" {0:G}", i) + "pt";
    }

    void StealViewerChange(int i)
    {
        stealViewer.text = "スチール　" + string.Format(" {0:G}", i) + "pt";
    }

    void PetViewerChange(int i)
    {
        petViewer.text = "ペットボトル　" + string.Format(" {0:G}", i) + "pt";
    }

    void PlaViewerChange(int i)
    {
        plaViewer.text = "プラスチック　" + string.Format(" {0:G}", i) + "pt";
    }

    void PaperViewerChange(int i)
    {
        paperViewer.text = "かみ　" + string.Format(" {0:G}", i) + "pt";
    }

    void AllViewerChange(int i)
    {
        allViewer.text = "ごうけい　" + string.Format(" {0:G}", i) + "pt";
    }

    //全ポイント表示を一括変更
    void PointViewerChange()
    {
        AlumiViewerChange(alumiPoint);
        StealViewerChange(stealPoint);
        PetViewerChange(petPoint);
        PlaViewerChange(plaPoint);
        PaperViewerChange(paperPoint);
        AllViewerChange(allPoint);
    }

    //UI(ウィンドウ)関連
    [SerializeField] GameObject BuildWindow0; //初回建設用ウィンドウ
    [SerializeField] GameObject BuildWindow1; //建築物レベルアップ用ウィンドウ
    [SerializeField] GameObject MissionWindow; //ミッション用ウィンドウ
    [SerializeField] GameObject InputWindow; //リサイクルマーク入力用ウィンドウ
    [SerializeField] GameObject EventWindow; //イベント用ウィンドウ
    bool open = false; //既に何らかのウィンドウが開いているフラグ

    [SerializeField] Image build1Picture;

    //建設ウィンドウの開閉
    public void OpenBuild(int i)
    {
        if(lv[i] == 0)
        {
            OpenBuild0(i);
        }
        else
        {
            OpenBuild1(i);
        }
    }
    public void OpenBuild0(int i) //OpenBuild内で呼び出し
    {
        if(open == false)
        {
            selectedPlace = i;
            BuildWindow0.SetActive(true);
            open = true;
        }
    }

    public void CloseBuild0()
    {
        selectedPlace = -1;
        selectedBuilding = 0;
        BuildWindow0.SetActive(false);
        open = false;
    }

    public void OpenBuild1(int i) //OpenBuild内で呼び出し
    {
        selectedPlace = i;
        /*
        switch(i)
        {
            case 1:
                if(place_1 == 1)
                {
                    build1Picture.sprite = recycleImage;
                }
                else if(place_1 ==  2)
                {
                    build1Picture.sprite = amusementImage;
                }
                break;
            case 2:
                if (place_2 == 1)
                {
                    build1Picture.sprite = recycleImage;
                }
                else if (place_2 == 2)
                {
                    build1Picture.sprite = amusementImage;
                }
                break;
            case 3:
                if (place_3 == 1)
                {
                    build1Picture.sprite = recycleImage;
                }
                else if (place_3 == 2)
                {
                    build1Picture.sprite = amusementImage;
                }
                break;
            case 4:
                if (place_4 == 1)
                {
                    build1Picture.sprite = recycleImage;
                }
                else if (place_4 == 2)
                {
                    build1Picture.sprite = amusementImage;
                }
                break;

        }
        */
        if (open == false)
        {
            selectedPlace = i;
            BuildWindow1.SetActive(true);
            open = true;
            //建設物に合わせた画像を表示
            if (place[i] == 1)
            {
                build1Picture.sprite = recycleImage;
            }
            else if (place[i] == 2)
            {
                build1Picture.sprite = amusementImage;
            }
        }
    }

    public void CloseBuild1()
    {
        selectedPlace = 0;
        BuildWindow1.SetActive(false);
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

    //イベントウィンドウの開閉
    public void OpenEvent1()
    {
        if(open == false)
        {
            EventWindow.SetActive(true);
            open = true;

            //イベント情報を設定
            Event tmpEvent = ScriptableObject.CreateInstance("Event") as Event;
            tmpEvent = eventDataBase.eventList[0]; //[]内の番号のイベント情報を取得

            //各種数値を設定
            eventBonus = tmpEvent.bonus;
            alumiBonus = tmpEvent.alumi;
            stealBonus = tmpEvent.steal;
            petBonus = tmpEvent.pet;
            plaBonus = tmpEvent.pla;
            paperBonus = tmpEvent.paper;

            //イベント説明テキストを変更
            eventText1.text = tmpEvent.intro;
        }
    }

    public void CloseEvent1()
    {
        EventWindow.SetActive(false);
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

    //選択中の建築場所
    int selectedPlace = -1; //0～5が対応。-1は初期値。
    //選択中の建築物
    int selectedBuilding = 0; //BuildWindow0で使用。1がリサイクル場、2が娯楽施設

    //建築フラグ等
    int[] place = {0, 0, 0, 0, 0, 0}; //建築場所1～6の建物。1がリサイクル場、2が娯楽施設
    int[] lv = {0, 0, 0, 0, 0, 0}; //建築場所1～6のレベル

    //各建築場所の画像
    [SerializeField] Image buildImage0;
    [SerializeField] Image buildImage1;
    [SerializeField] Image buildImage2;
    [SerializeField] Image buildImage3;
    [SerializeField] Image buildImage4;
    [SerializeField] Image buildImage5;
    //リサイクル場、娯楽施設の画像
    [SerializeField] Sprite recycleImage;
    [SerializeField] Sprite amusementImage;

    //建築物選択
    public void ChangeSelectedBuildeing(int i)
    {
        selectedBuilding = i;
    }

    //建設決定ボタン
    public void DecideBuild()
    {
        if(selectedBuilding != 0)
        {
            place[selectedPlace] = selectedBuilding;
            lv[selectedPlace] = 1;
            switch(selectedPlace)
            {
                case 0:
                    if (selectedBuilding == 1)
                    {
                        buildImage0.sprite = recycleImage;
                    }
                    else if (selectedBuilding == 2)
                    {
                        buildImage0.sprite = amusementImage;
                    }
                    break;
                case 1:
                    if (selectedBuilding == 1)
                    {
                        buildImage1.sprite = recycleImage;
                    }
                    else if (selectedBuilding == 2)
                    {
                        buildImage1.sprite = amusementImage;
                    }
                    break;
                case 2:
                    if (selectedBuilding == 1)
                    {
                        buildImage2.sprite = recycleImage;
                    }
                    else if (selectedBuilding == 2)
                    {
                        buildImage2.sprite = amusementImage;
                    }
                    break;
                case 3:
                    if (selectedBuilding == 1)
                    {
                        buildImage3.sprite = recycleImage;
                    }
                    else if (selectedBuilding == 2)
                    {
                        buildImage3.sprite = amusementImage;
                    }
                    break;
                case 4:
                    if (selectedBuilding == 1)
                    {
                        buildImage4.sprite = recycleImage;
                    }
                    else if (selectedBuilding == 2)
                    {
                        buildImage4.sprite = amusementImage;
                    }
                    break;
                case 5:
                    if (selectedBuilding == 1)
                    {
                        buildImage5.sprite = recycleImage;
                    }
                    else if (selectedBuilding == 2)
                    {
                        buildImage5.sprite = amusementImage;
                    }
                    break;
                default:
                    break;
            }
            
            PointViewerChange();
            CloseBuild0();
        }
    }

    //建築物レベルアップボタン
    public void DecideLevelUp()
    {
        switch(lv[selectedPlace])
        {
            case 1:
                if(place[selectedPlace] == 1 && alumiPoint >= 5)
                {
                    lv[selectedPlace] += 1;
                    alumiPoint -= 5;
                }
                else if(place[selectedPlace] == 2 && stealPoint >= 5)
                {
                    lv[selectedPlace] += 1;
                    stealPoint -= 5;
                }
                break;
            case 2:
                if (place[selectedPlace] == 1 && alumiPoint >= 10)
                {
                    lv[selectedPlace] += 1;
                    alumiPoint -= 10;
                }
                else if (place[selectedPlace] == 2 && stealPoint >= 10)
                {
                    lv[selectedPlace] += 1;
                    stealPoint -= 10;
                }
                break;
            case 3:
                if (place[selectedPlace] == 1 && alumiPoint >= 15)
                {
                    lv[selectedPlace] += 1;
                    alumiPoint -= 15;
                }
                else if (place[selectedPlace] == 2 && stealPoint >= 15)
                {
                    lv[selectedPlace] += 1;
                    stealPoint -= 15;
                }
                break;
        }

        PointViewerChange();
        //以下3行は動作確認用です。要らなくなったら消してください。
        Debug.Log(lv[0] + "," + lv[1] + "," + lv[2]);
        CalcBuildBonus();
        Debug.Log(inputRate + "," + (int)(5 * (inputRate + 0.01f)));
    }

    //建築によるボーナス
    float inputRate = 1f; //マーク入力倍率
    float eventRate = 1f; //イベント倍率

    //建築ボーナスの再計算
    void CalcBuildBonus()
    {
        //各種ボーナスの初期化
        inputRate = 1f;
        eventRate = eventBonus;

        //建築フラグを元に再計算
        for(int i = 0; i < 6; i++)
        {
            if(place[i] == 1)
            {
                inputRate += 0.2f * lv[i] + 0.0001f; //そのまま計算すると端数の都合で答えがおかしくなることがあるため、+0.001してます。
            }
            else if(place[i] == 2)
            {
                eventRate += 0.2f * lv[i] + 0.0001f; //イベントの報酬量に応じ、.00...1の部分は変えてください。
            }
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
        //建築ボーナス・イベントボーナスを計算
        CalcBuildBonus();

        //当日の入力数を反映
        todayAlumi += tmpAlumi;
        todaySteal += tmpSteal;
        todayPet += tmpPet;
        todayPla += tmpPla;
        todayPaper += tmpPaper;

        //加算するポイントを計算
        if(alumiBonus == true)//アルミ
        {
            tmpAlumi = (int)(tmpAlumi * 5 * inputRate * eventRate);
        }
        else
        {
            tmpAlumi = (int)(tmpAlumi * 5 * inputRate);
        }

        if (stealBonus == true) //スチール
        {
            tmpSteal = (int)(tmpSteal * 5 * inputRate * eventRate);
        }
        else
        {
            tmpSteal = (int)(tmpSteal * 5 * inputRate);
        }

        if (petBonus == true)//ペットボトル
        {
            tmpPet = (int)(tmpPet * 5 * inputRate * eventRate);
        }
        else
        {
            tmpPet = (int)(tmpPet * 5 * inputRate);
        }

        if (plaBonus == true)//プラスチック
        {
            tmpPla = (int)(tmpPla * 5 * inputRate * eventRate);
        }
        else
        {
            tmpPla = (int)(tmpPla * 5 * inputRate);
        }

        if (paperBonus == true)//紙
        {
            tmpPaper = (int)(tmpPaper * 5 * inputRate * eventRate);
        }
        else
        {
            tmpPaper = (int)(tmpPaper * 5 * inputRate);
        }


        //各マークのポイントを反映
        alumiPoint += tmpAlumi;
        stealPoint += tmpSteal;
        petPoint += tmpPet;
        plaPoint += tmpPla;
        paperPoint += tmpPaper;
        allPoint += tmpAlumi + tmpSteal + tmpPet + tmpPla + tmpPaper;

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

        //ポイント表示UIに反映
        PointViewerChange();
    }

    //イベント関連
    [SerializeField] EventDataBase eventDataBase; //イベントデータベースの管理
    public void AddEventData(Event i)
    {
        eventDataBase.eventList.Add(i);
    }

    float eventBonus = 1.0f; //イベントによるボーナス倍率。

    //対象マーク
    bool alumiBonus = false;
    bool stealBonus = false;
    bool petBonus = false;
    bool plaBonus = false;
    bool paperBonus = false;

    //イベント説明テキスト
    [SerializeField] TextMeshProUGUI eventText1; //ボーナス系イベント用

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

        //ポイント表示UIに反映
        PointViewerChange();
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
