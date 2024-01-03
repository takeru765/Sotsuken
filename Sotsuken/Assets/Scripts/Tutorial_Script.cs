using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using System.IO;

public class Tutorial_Script : MonoBehaviour
{

    //セーブデータ関連
    [HideInInspector] public SaveData save; //セーブデータ
    string filePath; //json(セーブデータ)ファイルのパス
    string fileName = "save.json"; //jsonのファイル名

    //セーブ処理
    void Save(SaveData data)
    {
        save.opSequece = opSequence;

        //saveへの情報書き込み
        /*save.year = year;
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

        save.alumiBook = alumiBook;
        save.stealBook = stealBook;
        save.petBook = petBook;
        save.plaBook = plaBook;
        save.paperBook = paperBook;

        save.opSequece = opSequence;

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

        save.eventID = eventID;
        save.answered = answered;*/

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

        opSequence = save.opSequece;//チュートリルをシーンを超えて読み込めるように

        //各種数値の反映
        /* year = save.year;
         month = save.month;
         day = save.day;

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

         alumiBook = save.alumiBook;
         stealBook = save.stealBook;
         petBook = save.petBook;
         plaBook = save.plaBook;
         paperBook = save.paperBook;

         opSequence = save.opSequece;

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

         eventID = save.eventID;
         answered = save.answered;

         //各種UIに反映
         PointViewerChange();
         CheckBuildImage();*/
    }

    //フェードイン・アウト関連
    float alfa = 1f; //画像の不透明度に使用
    [SerializeField] Image fade; //フェードイン・アウト用の黒画像
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
        opSequence = 31;
        Save(save);
        isFadeOut = true;
    }

    //オープニング、チュートリアルの進行管理
    [SerializeField] GameObject opWindow0; //文章のみのOP用ウィンドウ
    [SerializeField] TextMeshProUGUI opText;

    [SerializeField] GameObject Button_Active; //文章のみのOP用ウィンドウ

    int opSequence = 20; //オープニング・チュートリアルの進行度(本スクリプトは図鑑パートのデバッグ用に20からスタート)

    void ClickCheck() //クリック時に呼び出す。オープニングの進捗に応じて、画面クリックで進行するかを管理。制作中
    {
        switch (opSequence)
        {
            /*case 0:
                audioSource.PlayOneShot(openWindow); //効果音再生
                opSequence = 1;
                break;
            case 1:
                audioSource.PlayOneShot(openWindow); //効果音再生
                opSequence = 2;
                break;
            case 2:
                audioSource.PlayOneShot(openWindow); //効果音再生
                Save(save);
                opSequence = 10;
                break;
            case 10:
                break;
            case 11:
                break;
            case 12:
                audioSource.PlayOneShot(openWindow); //効果音再生
                Save(save);
                opSequence = 31; //後で、移動先を図鑑チュートリアルに変更
                break;*/
            case 20:
                opSequence = 21;
                break;
            case 21:
                opSequence = 22;
                break;
            case 22:
                opSequence = 23;
                break;
            case 23:
                opSequence = 24;
                break;
            case 24:
                opSequence = 25;
                break;
            case 25:
                opSequence = 26;
                break;
            /*case 31:
                audioSource.PlayOneShot(openWindow); //効果音再生
                opSequence = 32;
                break;
            case 32:
                opSequence = 33;
                break;
            case 33:
                break;
            case 34:
                audioSource.PlayOneShot(openWindow); //効果音再生
                opSequence = 35;
                break;
            case 35:
                audioSource.PlayOneShot(openWindow); //効果音再生
                Save(save);
                opSequence = 40;
                break;
            case 41:
                break;
            case 42:
                Save(save);
                opSequence = 50;
                break;
            case 50:
                opSequence = 51;
                break;
            case 51:
                opSequence = 52;
                break;
            default:
                break;*/
        }
    }

    void ControlOP() //オープニング・チュートリアルの進行管理。追加しやすさ見やすさのために、パートごとに10の位を変更する形にしてます。
    {
        
        switch (opSequence)
        {
            /*case 0: //オープニング
                canAll(false); //全ボタンの開閉を禁止

                SetOPWindow0("プレイありがとうございます。\n\nあなたはこのリサイクルシティの市長です。\n\n現実で出たゴミを「リサイクル」しながら、\nこの町を発展させていくのが\nこのゲームの目的です。");
                break;
            case 1:
                canAll(false);

                SetOPWindow0("秘書「市長、就任おめでとうございます！」\n秘書「ゴミを資源として再利用する「リサイクル」。リサイクルを通じて、この町をキレイにしていきましょう！」");
                break;
            case 2:
                canAll(false);

                SetOPWindow0("秘書「まずは、あなたの身の回りにある、\n「リサイクルマーク」のついたゴミを探してみてください。」");
                break;
            case 10: //マーク入力チュートリアル
                canAll(false);
                canInput = true; //入力ボタンだけ使用を許可
                opWindow0.SetActive(false);

                SetTutorial(250f, -350f, 0.5f, "リサイクルマークを\n見つけたら、\nここをクリックしよう！！");
                PutArrow(350f, -550f);
                break;
            case 11:
                canAll(false);

                SetTutorial(-100f, 750f, 0.7f, "見つけたマークの個数を入力して、\n「けってい」ボタンを押そう。");
                PutArrow(-100f, 510f);
                break;
            case 12:
                canAll(false);

                SetTutorial(0f, 200f, 1f, "マークを入力すると、\nリサイクルポイントを獲得できるよ。");
                PutArrow(0f, 550f, 135f);
                break;*/

            case 20: //図鑑パートのチュートリアルを想定//ここば図鑑パートで
                SetOPWindow0("ここはずかんパートです\n\nいままであつめたリサイクルマークを\n\nみてべんきょうすることができます");
                //Debug.Log("起動済み");
                break;

            case 21:
                SetOPWindow0("もしリサイクルマークのいみをわすれてしまったら、\n\nいつでもこのずかんをひらいて\n\nがんばっておぼえましょう");
                break;
            case 22:
                SetOPWindow0("ためしに、アルミかんのずかんをひらいてみましょう");
                break;
            case 23:
                opWindow0.SetActive(false);
                SetTutorial(80f, 260f, 1.0f, "アルミかんのマークをクリックすると、ずかんがひらきます\n\n");
                PutArrow(-300f, 400f, -180f);
                break;

            case 24:
                //図鑑の穴埋め作業について説明
                SetTutorial(22f, -198f, 1.0f, "ずかんのうえはマークのなまえと、\n\nマークにつかわれるゴミのしゅるいがかかれています");
                break;
            case 25:
                SetTutorial(16f, 604f, 1.0f, "したにはずかんのないようがバラバラになったパズルがあります\n\nあてはめてずかんをかんせいさせてちしきをつけましょう");
                break;
            case 26:
                SetOPWindow0("いじょうでずかんぱーとのせつめいをおわります");
                break;

            /*case 31: //建築パートの前振り
                canAll(false);
                tutorialWindow.SetActive(false);
                arrow.SetActive(false);

                SetOPWindow0("秘書「おめでとうございます！！」\n秘書「さっそくリサイクルできたようですね！」");
                break;
            case 32:
                canAll(false);

                SetOPWindow0("秘書「実は私もリサイクルできるゴミを\n見つけてきました。」\n秘書「その分のポイントも差し上げますね。」");
                break;
            case 33:
                canAll(false);

                alumiPoint += 15;
                stealPoint += 15;
                petPoint += 15;
                plaPoint += 15;
                paperPoint += 15;
                allPoint += 75;
                PointViewerChange(); //ポイント表示UIに反映
                audioSource.PlayOneShot(inputEnter); //効果音再生
                opSequence = 34;
                break;
            case 34:
                canAll(false);

                SetOPWindow0("各ポイントを15ずつ獲得した！");

                break;
            case 35:
                canAll(false);

                SetOPWindow0("秘書「次は、獲得したポイントを使って、\n町を発展させてみましょう！」");
                break;
            case 40: //建築パートのチュートリアル
                canAll(false);
                canBuild = true;
                opWindow0.SetActive(false);

                if (lv[0] == 0) //(デバッグ用)既に建築済みの場合はチュートリアルを終了する。
                {
                    SetTutorial(-200f, 0f, 0.5f, "土地をタップすると、建築画面に進むよ。");
                    PutArrow(-200f, 180f, 90f);
                }
                else
                {
                    opSequence = 42;
                }
                break;
            case 41:
                canAll(false);

                SetTutorial(-100f, 750f, 0.7f, "「リサイクル場」か「娯楽施設」を\n建てられるよ。\n好きな方を選んで、\n「けってい」ボタンを押そう！");
                PutArrow(-100f, 510f);
                break;
            case 42:
                canAll(false);
                tutorialWindow.SetActive(false);
                arrow.SetActive(false);

                SetOPWindow0("秘書「うまく建物を作れましたね！\n建物を作ると、より多くのポイントを獲得できるようになります。」");
                break;
            case 50: //イベント
                canAll(false);

                SetOPWindow0("秘書「他にも、この町でゴミが出ることもあります。」");
                break;
            case 51:
                canAll(false);
                canEvent = true;
                opWindow0.SetActive(false);

                if (alumiBook > 0) //入力済みのマークに応じて、イベント内容を決定
                {
                    eventID = 1;
                }
                else if (stealBook > 0)
                {
                    eventID = 2;
                }
                else if (petBook > 0)
                {
                    eventID = 3;
                }
                else if (plaBook > 0)
                {
                    eventID = 4;
                }
                else if (paperBook > 0)
                {
                    eventID = 5;
                }
                OpenEvent();

                SetTutorial(0f, 700f, 0.5f, "このように、ゴミにあったリサイクル方法を答える、\nクイズイベントが発生することがあります。");
                PutArrow(0f, 500f, -45f);
                break;
            case 52:
                canAll(false);

                SetTutorial(200f, 700f, 0.5f, "答えが分からないときは、\nリサイクル図鑑を見てみよう！");
                PutArrow(400f, 500f, 0f);
                break;
            case 53:
                canAll(false);
                canBook = true;

                SetTutorial(-150f, -500f, 0.5f, "答えが分からないときは、\nリサイクル図鑑を見てみよう！");
                PutArrow(-300f, -700f, -90f);
                break;
            case 60: //イベント→図鑑のチュートリアルを想定
                break;
            case 70: //イベントパート、回答編
                break;
            default:
                //ウィンドウ等を非表示に
                opWindow0.SetActive(false);
                tutorialWindow.SetActive(false);
                arrow.SetActive(false);

                canAll(true);
                break;*/
        }
    }

    void SetOPWindow0(string text) //オープニングイベント用ウィンドウの文章変更
    {
        opWindow0.SetActive(true);
        opText.text = text;
    }

    //チュートリアル関連
    [SerializeField] GameObject arrow; //強調用の矢印
    bool blinking = false; //点滅中フラグ
    int blinkInterval = 25; //点滅間隔のフレーム数
    int intervalCount = 25; //経過フレームのカウント用
    int blinkTimes = 3; //点滅する回数

    [SerializeField] GameObject tutorialWindow;
    [SerializeField] TextMeshProUGUI tutorialText;

    void SetTutorial(float x, float y, float Scale, string text) //チュートリアルウィンドウの配置
    {
        tutorialWindow.transform.localPosition = new Vector2(x, y); //ウィンドウの位置調整
        tutorialWindow.transform.localScale = new Vector2(Scale, Scale);
        //tutorialWindow.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height); //ウィンドウのサイズ調整
        tutorialText.text = text; //メッセージ変更

        tutorialWindow.SetActive(true);
    }

    void PutArrow(float x, float y, float rotation = 0f) //点滅しない矢印の配置
    {
        arrow.transform.localPosition = new Vector2(x, y);
        arrow.transform.localEulerAngles = new Vector3(0, 0, rotation);
        arrow.SetActive(true);
    }

    void SetArrow(float xPos, float yPos, float rotation = 0f, int interval = 50, int times = 3) //矢印点滅開始。矢印が必要な位置が少ないなら、矢印を複数作った方が楽かも。
    {
        //矢印の位置、点滅間隔・回数を設定
        arrow.transform.localPosition = new Vector2(xPos, yPos);
        arrow.transform.localEulerAngles = new Vector3(0, 0, rotation);
        blinkInterval = interval / 2;
        intervalCount = interval / 2;
        blinkTimes = times;

        arrow.SetActive(true);
        blinking = true; //点滅フラグをON
    }

    private void Awake()
    {
       //パスを取得(Windowsの場合、「C:\Users\(ユーザー名)\AppData\LocalLow\DefaultCompany\Sotsuken」に保存される)
        filePath = Application.persistentDataPath + "/" + fileName;

        //ファイルが無い場合はファイルを作成
        if (!File.Exists(filePath))
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
       
    }

    // Update is called once per frame
    void Update()
    {
        //フェードイン・アウトフラグがonの時、フェード処理を行う
        if (isFadeIn == true)
        {
            FadeIn();
        }
        else if (isFadeOut == true)
        {
            FadeOut();
        }

        if(isFadeIn == false) //フェードイン中はクリックでチュートリアルを進行させない
        {
            if (Input.GetMouseButtonDown(0))
            {
                ClickCheck();

            }
            ControlOP();
        }
    }

}
