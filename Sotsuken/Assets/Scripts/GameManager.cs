using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using System.IO;

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

    //セーブデータ関連
    [HideInInspector] public SaveData save; //セーブデータ
    string filePath; //json(セーブデータ)ファイルのパス
    string fileName = "save.json"; //jsonのファイル名

    //セーブ処理
    void Save(SaveData data)
    {
        //saveへの情報書き込み
        save.year = year;
        save.month = month;
        save.day = day;

        save.alumiPoint = alumiPoint;
        save.stealPoint = stealPoint;
        save.petPoint = petPoint;
        save.plaPoint = plaPoint;
        save.paperPoint = paperPoint;
        save.allPoint = allPoint;

        save.todayAlumi = todayAlumi;
        save.todaySteal = todaySteal;
        save.todayPet = todayPet;
        save.todayPla = todayPla;
        save.todayPaper = todayPaper;

        save.place = place;
        save.lv = lv;

        save.goal = goal;
        save.mark = mark;
        save.markRec1 = markRec1;
        save.markRec2 = markRec2;
        save.setMission = setMission;
        save.successMission = successMission;
        save.setYear = setYear;
        save.setMonth = setMonth;
        save.setDay = setDay;

        save.answered = answered;

        //データ自体の保存処理
        string json = JsonUtility.ToJson(data); //SaveDataをjson形式に変換
        StreamWriter wr = new StreamWriter(filePath, false); //jsonをfilePathの位置に書き込み
        wr.WriteLine(json);
        wr.Close();
    }

    //ロード処理1。jsonファイルをSaveData形式に変換する
    SaveData Load1(string path)
    {
        StreamReader rd = new StreamReader(path); //ファイルパスを指定
        string json = rd.ReadToEnd(); //ファイルパスにあるjsonファイルを読み込む
        rd.Close();

        return JsonUtility.FromJson<SaveData>(json); //SaveDataに変換して返す
    }

    //ロード処理2。SavaDataの内容をゲームに反映する
    void Load2()
    {
        //各種数値の反映
        year = save.year;
        month = save.month;
        day = save.month;

        alumiPoint = save.alumiPoint;
        stealPoint = save.stealPoint;
        petPoint = save.petPoint;
        plaPoint = save.plaPoint;
        paperPoint = save.paperPoint;
        allPoint = save.allPoint;

        todayAlumi = save.todayAlumi;
        todaySteal = save.todaySteal;
        todayPet = save.todayPet;
        todayPla = save.todayPla;
        todayPaper = save.todayPaper;

        place = save.place;
        lv = save.lv;

        goal = save.goal;
        mark = save.mark;
        markRec1 = save.markRec1;
        markRec2 = save.markRec2;
        setMission = save.setMission;
        successMission = save.successMission;
        setYear = save.setYear;
        setMonth = save.setMonth;
        setDay = save.setDay;

        answered = save.answered;

        //各種UIに反映
        PointViewerChange();
        CheckBuildImage();
    }

    //デバッグ用セーブデータ消去
    public void ResetSave()
    {
        //saveの初期化
        save = new SaveData();
        string json = JsonUtility.ToJson(save); //SaveDataをjson形式に変換
        StreamWriter wr = new StreamWriter(filePath, false); //jsonをfilePathの位置に書き込み
        wr.WriteLine(json);
        wr.Close();

        //初期化したセーブデータを読み込み
        Load1(filePath);
        Load2();
    }

    //フェードイン・アウト処理
    [SerializeField] Image fadeImage;
    bool isFadeIn = true; //フェードイン中フラグ
    bool isFadeOut = false; //フェードアウト中フラグ
    float alfa = 1f; //フェード画像の不透明度

    void FadeIn(float speed = 0.75f) //フェードイン処理。フェード速度も指定可能
    {
        alfa -= speed * Time.deltaTime; //時間経過に応じて不透明度を低下
        fadeImage.color = new Color(0, 0, 0, alfa); //不透明度を反映

        if(alfa < 0)
        {
            alfa = 0;
            isFadeIn = false;

            fadeImage.raycastTarget = false; //フェード画像のクリック判定を無効化(下にあるボタンをクリックさせるため)
        }
    }

    void FadeOut(float speed = 0.75f)
    {
        fadeImage.raycastTarget = true; //フェード画像のクリック判定を有効化(各種ボタンをクリックできないようにする)

        alfa += speed * Time.deltaTime; //時間経過に応じて不透明度を増加
        fadeImage.color = new Color(0, 0, 0, alfa); //不透明度を反映

        if (alfa >= 1)
        {
            alfa = 1;
            isFadeOut = false;

            SceneManager.LoadScene("Odajima_bookpart");
        }
    }

    public void StartFadeOut()
    {
        isFadeOut = true;
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
    [SerializeField] GameObject EventWindow1; //ボーナス系イベント用ウィンドウ
    [SerializeField] GameObject EventWindow2; //クイズ系イベントウィンドウ
    bool open = false; //既に何らかのウィンドウが開いているフラグ

    //建築ウィンドウの画像、文章
    [SerializeField] Image build1Picture;
    [SerializeField] TextMeshProUGUI buildIntro;

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
        Save(save); //オートセーブ

        selectedPlace = -1;
        selectedBuilding = 0;
        BuildWindow0.SetActive(false);
        open = false;
    }

    public void OpenBuild1(int i) //OpenBuild内で呼び出し
    {
        selectedPlace = i;
        
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
            //建設LVに合わせた説明文を表示
            switch(lv[i])
            {
                case 1:
                    if(place[i] == 1)
                    {
                        buildIntro.text = "LV:1→2\nコスト:アルミ5pt\nボーナス:+20%→+40%";
                    }
                    else if(place[i] == 2)
                    {
                        buildIntro.text = "LV:1→2\nコスト:スチール5pt\nボーナス:+20%→+40%";
                    }
                    break;
                case 2:
                    if (place[i] == 1)
                    {
                        buildIntro.text = "LV:2→3\nコスト:アルミ10pt\nボーナス:+40%→+60%";
                    }
                    else if (place[i] == 2)
                    {
                        buildIntro.text = "LV:2→3\nコスト:スチール10pt\nボーナス:+40%→+60%";
                    }
                    break;
                case 3:
                    if (place[i] == 1)
                    {
                        buildIntro.text = "LV:3→4\nコスト:アルミ15pt\nボーナス:+60%→+80%";
                    }
                    else if (place[i] == 2)
                    {
                        buildIntro.text = "LV:3→4\nコスト:スチール15pt\nボーナス:+60%→+80%";
                    }
                    break;
                case 4:
                    buildIntro.text = "LV:4(Max)\nさいだいレベルです。";
                    break;
                default:
                    break;
            }
        }
    }

    public void CloseBuild1()
    {
        Save(save); //オートセーブ

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

        //UI上の目標の表示を変更する
        if (goal < 10)
        {
            goalText.text = string.Format(" {0:G}", goal);
        }
        else
        {
            goalText.text = string.Format("{0:G}", goal);
        }
    }

    public void CloseMission()
    {
        Save(save); //オートセーブ

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
        Save(save); //オートセーブ

        InputWindow.SetActive(false);
        open = false;
    }

    //イベントウィンドウの開閉
    public void OpenEvent()
    {
        if(open == false)
        {
            //イベント情報を取得
            Event tmpEvent = ScriptableObject.CreateInstance("Event") as Event;
            tmpEvent = eventDataBase.eventList[1]; //[]内の番号のイベント情報を取得

            if(tmpEvent.isQuiz == false) //ボーナス系イベントの場合
            {
                EventWindow1.SetActive(true);

                //各種数値を設定
                eventBonus = tmpEvent.bonus;
                alumiBonus = tmpEvent.alumi;
                stealBonus = tmpEvent.steal;
                petBonus = tmpEvent.pet;
                plaBonus = tmpEvent.pla;
                paperBonus = tmpEvent.paper;

                //イベント説明テキストを変更
                eventText1.text = tmpEvent.intro;

                //イベント画像を変更
                eventImage1.sprite = tmpEvent.eventImage;
            }
            else //クイズ系イベントの場合
            {
                EventWindow2.SetActive(true);

                //イベント説明テキストを変更
                eventText2.text = tmpEvent.intro;
                answerText1.text = tmpEvent.answer1;
                answerText2.text = tmpEvent.answer2;
                answerText3.text = tmpEvent.answer3;

                //イベント画像を変更
                eventImage2.sprite = tmpEvent.eventImage;

                //クイズ処理に必要な情報を保存
                correctAnswer = tmpEvent.ansewr;
                rewardPoint = (int) tmpEvent.bonus;
                if(tmpEvent.alumi == true)
                {
                    rewardMark = 1;
                }
                else if (tmpEvent.steal == true)
                {
                    rewardMark = 2;
                }
                else if (tmpEvent.pet == true)
                {
                    rewardMark = 3;
                }
                else if (tmpEvent.pla == true)
                {
                    rewardMark = 4;
                }
                else if (tmpEvent.paper == true)
                {
                    rewardMark = 5;
                }
            }

            open = true;
        }
    }

    public void CloseEvent2()
    {
        Save(save); //オートセーブ

        EventWindow2.SetActive(false);
        open = false;
    }

    public void CloseEvent1()
    {
        Save(save); //オートセーブ

        EventWindow1.SetActive(false);
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

    //建築ボタンへの画像の反映(起動時用を想定)
    void CheckBuildImage()
    {
        for(int i = 0; i < 6; i++)
        {
            if(place[i] == 0)
            {
                switch (i)
                {
                    case 0:
                        buildImage0.sprite = null;
                        break;
                    case 1:
                        buildImage1.sprite = null;
                        break;
                    case 2:
                        buildImage2.sprite = null;
                        break;
                    case 3:
                        buildImage3.sprite = null;
                        break;
                    case 4:
                        buildImage4.sprite = null;
                        break;
                    case 5:
                        buildImage5.sprite = null;
                        break;
                    default:
                        break;
                }
            }
            else if (place[i] == 1)
            {
                switch(i)
                {
                    case 0:
                        buildImage0.sprite = recycleImage;
                        break;
                    case 1:
                        buildImage1.sprite = recycleImage;
                        break;
                    case 2:
                        buildImage2.sprite = recycleImage;
                        break;
                    case 3:
                        buildImage3.sprite = recycleImage;
                        break;
                    case 4:
                        buildImage4.sprite = recycleImage;
                        break;
                    case 5:
                        buildImage5.sprite = recycleImage;
                        break;
                    default:
                        break;
                } 
            }
            else if (place[i] == 2)
            {
                switch (i)
                {
                    case 0:
                        buildImage0.sprite = amusementImage;
                        break;
                    case 1:
                        buildImage1.sprite = amusementImage;
                        break;
                    case 2:
                        buildImage2.sprite = amusementImage;
                        break;
                    case 3:
                        buildImage3.sprite = amusementImage;
                        break;
                    case 4:
                        buildImage4.sprite = amusementImage;
                        break;
                    case 5:
                        buildImage5.sprite = amusementImage;
                        break;
                    default:
                        break;
                }
            }
        }
    }

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
                CloseBuild1();
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
                CloseBuild1();
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
                CloseBuild1();
                break;
            default:
                CloseBuild1();
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

    //リサイクルマーク入力を確定
    public void InputEnter()
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

    //クイズの正解を管理
    int correctAnswer = 0;

    //クイズの報酬管理
    int rewardPoint = 0; //報酬ポイント
    int rewardMark = 0; //報酬のマーク種類、1～5で管理
    bool answered = false; //正解済みフラグ

    //イベント説明テキスト
    [SerializeField] TextMeshProUGUI eventText1; //ボーナス系イベント用
    [SerializeField] TextMeshProUGUI eventText2; //クイズ系イベント用
    [SerializeField] TextMeshProUGUI answerText1; //解答ボタン用1
    [SerializeField] TextMeshProUGUI answerText2; //解答ボタン用2
    [SerializeField] TextMeshProUGUI answerText3; //解答ボタン用3

    //イベント画像表示
    [SerializeField] Image eventImage1; //ボーナス系イベント用
    [SerializeField] Image eventImage2; //クイズ系イベント用

    //クイズイベント解答処理
    public void CheckAnswer(int i)
    {
        if (i == correctAnswer && answered == false)
        {
            //建築ボーナスを計算
            for (int j = 0; j < 6; j++)
            {
                if (place[j] == 2)
                {
                    eventRate = 1 +0.2f * lv[j] + 0.0001f; //イベントの報酬量に応じ、.00...1の部分は変えてください。
                }
            }
            switch (rewardMark)
            {
                case 1:
                    alumiPoint += (int)(rewardPoint * eventRate);
                    break;
                case 2:
                    stealPoint += (int)(rewardPoint * eventRate);
                    break;
                case 3:
                    petPoint += (int)(rewardPoint * eventRate);
                    break;
                case 4:
                    plaPoint += (int)(rewardPoint * eventRate);
                    break;
                case 5:
                    paperPoint += (int)(rewardPoint * eventRate);
                    break;
                default:
                    break;
            }

            //ポイント表示に反映
            PointViewerChange();

            //正解済みフラグをON
            answered = true;
        }
    }

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

    //チュートリアル関連
    [SerializeField] GameObject arrow; //強調用の矢印
    bool blinking = false; //点滅中フラグ
    int blinkInterval = 25; //点滅間隔のフレーム数
    int intervalCount = 25; //経過フレームのカウント用
    int blinkTimes = 3; //点滅する回数

    [SerializeField] GameObject tutorialWindow;
    [SerializeField] TextMeshProUGUI tutorialText;

    void SetTutorial(float x, float y, float width, float height, string text) //チュートリアルウィンドウの配置
    {
        tutorialWindow.transform.localPosition = new Vector2(x, y); //ウィンドウの位置調整
        tutorialWindow.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height); //ウィンドウのサイズ調整
        tutorialText.text = text; //メッセージ変更

        tutorialWindow.SetActive(true);
    }

    void PutArrow(float x, float y) //点滅しない矢印の配置
    {
        arrow.transform.localPosition = new Vector2(x, y);
        arrow.SetActive(true);
    }

    void SetArrow(float xPos, float yPos, int interval = 50, int times = 3) //矢印点滅開始。矢印が必要な位置が少ないなら、矢印を複数作った方が楽かも。
    {
        //矢印の位置、点滅間隔・回数を設定
        arrow.transform.localPosition = new Vector2(xPos, yPos);
        blinkInterval = interval / 2;
        intervalCount = interval / 2;
        blinkTimes = times;

        arrow.SetActive(true);
        blinking = true; //点滅フラグをON
    }

    public void Debug_CallArrow() //点滅のデバッグボタン用。本実装では消していいはず。
    {
        SetArrow(-50f, -650f);
    }

    //現状では、起動時のセーブデータ読み込みにのみ使用
    private void Awake()
    {
        //パスを取得(Windowsの場合、「C:\Users\(ユーザー名)\AppData\LocalLow\DefaultCompany\Sotsuken」に保存される)
        filePath = Application.persistentDataPath + "/" + fileName;

        //ファイルが無い場合はファイルを作成
        if(!File.Exists(filePath))
        {
            Save(save);
        }

        //ファイルを読み込んでsaveに格納
        save = Load1(filePath);
        //saveの内容を各変数に反映
        Load2();
    }
    // Start is called before the first frame update
    void Start()
    {
        time = DateTime.Now;

        day = time.Day;
        month = time.Month;
        year = time.Year;

        Debug.Log(year + "/" + month + "/" + day);

        SetTutorial(250f, -450f, 500f, 250f, "リサイクルマークを\n見つけたら、\nここをタップ！！");
        PutArrow(350f, -650f);
    }

    // Update is called once per frame
    void Update()
    {
        //現在日時を取得。処理が重い場合、別の場所(画面切り替え時等)に移すかも
        time = DateTime.Now;

        //フェードイン・アウト処理
        if(isFadeIn == true)
        {
            FadeIn();
        }
        else if(isFadeOut == true)
        {
            FadeOut();
        }
    }

    void FixedUpdate()
    {
        if(blinking == true)
        {
            //矢印の点滅処理
            intervalCount -= 1;
            if (intervalCount <= 0)
            {
                arrow.SetActive(!arrow.activeSelf);
                if (arrow.activeSelf == false)
                {
                    blinkTimes -= 1;
                }
                if (blinkTimes <= 0)
                {
                    blinking = false;
                }

                intervalCount = blinkInterval;
            }
        }
    }
}
