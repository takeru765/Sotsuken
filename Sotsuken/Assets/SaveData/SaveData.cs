using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    //日付
    public int year;
    public int month;
    public int day;

    //各ポイント
    public int alumiPoint;
    public int stealPoint;
    public int petPoint;
    public int plaPoint;
    public int paperPoint;
    public int allPoint;

    //当日のマーク数
    public int todayAlumi;
    public int todaySteal;
    public int todayPet;
    public int todayPla;
    public int todayPaper;

    //建築物、LV
    public int[] place = new int[6];
    public int[] lv = new int[6];

    //ミッション関連
    public int goal;
    public int mark;
    public int markRec1;
    public int markRec2;
    public bool setMission;
    public bool successMission;
    public int setYear;
    public int setMonth;
    public int setDay;

    //イベント関連
    public int eventID;
}
