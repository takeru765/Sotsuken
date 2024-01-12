using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    //����N���t���O
    public bool first = true;

    //���t
    public int year = 10000;
    public int month = 10000;
    public int day = 10000;

    //�v���C����
    public int playLong = 0;

    //�e�|�C���g
    public int alumiPoint;
    public int stealPoint;
    public int petPoint;
    public int plaPoint;
    public int paperPoint;
    public int allPoint;
    public int cleanLV;

    //�����̃}�[�N��
    public int todayAlumi;
    public int todaySteal;
    public int todayPet;
    public int todayPla;
    public int todayPaper;

    //�}�ӗp�t���O
    public int alumiBook;
    public int stealBook;
    public int petBook;
    public int plaBook;
    public int paperBook;

    //�`���[�g���A���֘A
    public int opSequece; //�`���[�g���A���i�s�x
    public bool tutorialMission = false; //�~�b�V�����̃`���[�g���A���σt���O
    public bool tutorialEvent = false; //�~�b�V�����̃`���[�g���A���σt���O

    //���z���ALV
    public int[] place = new int[6];
    public int[] lv = new int[6];

    //�~�b�V�����֘A
    public int goal = 1;
    public int mark = 1;
    public int markRec1;
    public int markRec2;
    public bool setMission;
    public bool successMission;
    public int setYear;
    public int setMonth;
    public int setDay;

    //�C�x���g�֘A
    public int eventID = 1;
    public bool answered;
}
