using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using System.IO;

public class Counter_test : MonoBehaviour
{
    //セーブデータ関連
    [HideInInspector] public SaveData save; //セーブデータ
    string filePath; //json(セーブデータ)ファイルのパス
    string fileName = "save.json"; //jsonのファイル名

    //現状では空のセーブを作ってる記録してるだけ。どこかで使う場合は一声かけてください。
    //スクリプト側から数値を記録したい場合は、Load2の逆に処理を追加して下さい。
    void Save() 
    {
        //データ自体の保存処理
        string json = JsonUtility.ToJson(save); //SaveDataをjson形式に変換
        StreamWriter wr = new StreamWriter(filePath, false); //jsonをfilePathの位置に書き込み
        wr.WriteLine(json);
        wr.Close();
    }

    SaveData Load1(string path) //ロード処理1。セーブデータ自体の読み込む
    {
        StreamReader rd = new StreamReader(path); //ファイルパスを指定
        string json = rd.ReadToEnd(); //ファイルパスにあるjsonファイルを読み込む
        rd.Close();

        return JsonUtility.FromJson<SaveData>(json); //SaveDataに変換して返す
    }

    void Load2() //ロード処理2。読み込んだセーブデータから、スクリプト内の変数に記録する。必要に応じて追加してください。
    {
        //発展パートで記録した「今までマークを入力した数」を読み込む
        alumi = save.alumiBook;
        steal = save.stealBook;
        pet = save.petBook;
        pla = save.plaBook;
        paper = save.paperBook;
    }

    public int alumi;//それぞれごみを入力した回数。セーブデータから読み取り
    public int steal;
    public int pet;
    public int pla;
    public int paper;


    public Trophy_Flag flag_script;

    //シーン起動時に、Startより前に処理されるらしい。
    private void Awake()
    {
        //パスを取得(Windowsの場合、「C:\Users\(ユーザー名)\AppData\LocalLow\DefaultCompany\Sotsuken」に保存される)
        filePath = Application.persistentDataPath + "/" + fileName;

        //ファイルが無い場合はファイルを作成
        if (!File.Exists(filePath))
        {
            Save();
        }

        //ファイルを読み込んでsaveに格納
        save = Load1(filePath);
        //saveの内容を各変数に反映
        Load2();

        Debug.Log("アルミ：" + alumi + "、スチール：" + steal + "、ペットボトル：" + pet + "、プラスチック：" + pla + "紙：" + paper);
    }

    // Start is called before the first frame update
    void Start()
    {
       

    }

    public void Onclick()
    {
        alumi += 1;
        steal += 1;
        pet += 1;
        pla += 1;
        paper += 1;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (paper == 0)
        {
            Debug.Log("ゼロだよ");
        }
        else if (paper == 3)
        {
            Debug.Log("3カウント！");
        }*/


    }

}
