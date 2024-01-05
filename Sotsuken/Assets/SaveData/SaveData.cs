using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    //初回起動フラグ
    public bool first = true;

    //日付
    public int year = 10000;
    public int month = 10000;
    public int day = 10000;

    //プレイ日数
    public int playLong = 0;

    //各ポイント
    public int alumiPoint;
    public int stealPoint;
    public int petPoint;
    public int plaPoint;
    public int paperPoint;
    public int allPoint;
    public int cleanLV;

    //当日のマーク数
    public int todayAlumi;
    public int todaySteal;
    public int todayPet;
    public int todayPla;
    public int todayPaper;

    //図鑑用フラグ
    public int alumiBook;
    public int stealBook;
    public int petBook;
    public int plaBook;
    public int paperBook;

    //チュートリアル関連
    public int opSequece; //チュートリアル進行度
    public bool tutorialMission = false; //ミッションのチュートリアル済フラグ
    public bool tutorialEvent = false; //ミッションのチュートリアル済フラグ

    //建築物、LV
    public int[] place = new int[6];
    public int[] lv = new int[6];

    //ミッション関連
    public int goal = 1;
    public int mark = 1;
    public int markRec1;
    public int markRec2;
    public bool setMission;
    public bool successMission;
    public int setYear;
    public int setMonth;
    public int setDay;

    //イベント関連
    public int eventID = 1;
    public bool answered;
}
