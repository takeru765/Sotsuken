using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    //���t
    public int year;
    public int month;
    public int day;

    //�e�|�C���g
    public int alumiPoint;
    public int stealPoint;
    public int petPoint;
    public int plaPoint;
    public int paperPoint;
    public int allPoint;

    //�����̃}�[�N��
    public int todayAlumi;
    public int todaySteal;
    public int todayPet;
    public int todayPla;
    public int todayPaper;

    //���z���ALV
    public int[] place = new int[6];
    public int[] lv = new int[6];

    //�~�b�V�����֘A
    public int goal;
    public int mark;
    public int markRec1;
    public int markRec2;
    public bool setMission;
    public bool successMission;
    public int setYear;
    public int setMonth;
    public int setDay;

    //�C�x���g�֘A
    public int eventID;
}