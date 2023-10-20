using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    //���ݓ�����ۑ�
    DateTime time;

    //UI(�E�B���h�E)�֘A
    [SerializeField] GameObject BuildWindow; //���ݗp�E�B���h�E
    [SerializeField] GameObject MissionWindow; //�~�b�V�����p�E�B���h�E
    bool open = false; //���ɉ��炩�̃E�B���h�E���J���Ă���t���O

    //���݃E�B���h�E�̊J��
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

    //�~�b�V�����E�B���h�E�̊J��
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

    //�e��|�C���g
    int alumiPoint = 0; //�A���~��
    int stealPoint = 0; //�X�`�[����
    int petPoint = 0; //�y�b�g�{�g��
    int plaPoint = 0; //�v���X�`�b�N
    int paperPoint = 0; //�����e��
    int allPoint = 0; //�݌v�|�C���g

    //�����l�������|�C���g(�~�b�V�����p)
    int todayAlumi = 0; //�A���~��
    int todaySteal = 0; //�X�`�[����
    int todayPet = 0; //�y�b�g�{�g��
    int todayPla = 0; //�v���X�`�b�N
    int todayPaper = 0; //��

    //���z�t���O��
    int place_A = 0; //���z�ꏊ����1�̌���
    int lv_A = 0; //���z�ꏊ����1�̃��x��
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

    //�C�x���g�֘A


    //�~�b�V�����֘A
    int goal = 1; //�ݒ肵���ڕW
    int count = 0; //���݂̃}�[�N�l����
    int mark = 0; //�ݒ肵���}�[�N�̎��
    int markRec1 = 0; //�O���̃}�[�N�̎��
    int markRec2 = 0; //�O�X���̃}�[�N�̎��
    [SerializeField] TextMeshProUGUI goalText; //�ڕW�̐��l�e�L�X�g
    bool setMission = false; //�~�b�V�����m��ς݃t���O
    int setYear = 0; //�~�b�V�����m��N
    int setMonth = 0; //�~�b�V�����m�茎
    int setDay = 0; //�~�b�V�����m���

    //�~�b�V�����̃}�[�N��ݒ肷��B
    public void SetMark(int m)
    {
        if(!setMission)
        {
            mark = m;
            Debug.Log("mark = " + mark);
        }
    }

    //�~�b�V�����̖ڕW���𑝌�����
    public void AddReason(int i)
    {
        if(!setMission)
        {
            goal += i;
        }

        //�ڕW�̏�������𒴂��Ȃ��悤�ɂ���
        if(goal < 1)
        {
            goal = 1;
        }

        if(goal > 10)
        {
            goal = 10;
        }

        //UI��̖ڕW�̕\����ύX����
        if(goal < 10)
        {
            goalText.text = string.Format(" {0:G}", goal);
        }
        else
        {
            goalText.text = string.Format("{0:G}", goal);
        }
    }

    //�~�b�V�����m��
    public void SetMission()
    {
        setMission = true;

        //�����̓��t��ۑ�
        setYear = time.Year;
        setMonth = time.Month;
        setDay = time.Day;
    }

    //�~�b�V�����B������
    void CheckMission()
    {
        if(count >= goal)
        {
            //�����Ƀ~�b�V�����������̏���������

        }
        else
        {
            //�����ɂȂ��Ă邩����
            if()
            {
                //�����Ɏ��s���̏���������

            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //���ݓ������擾�B�������d���ꍇ�A�ʂ̏ꏊ(��ʐ؂�ւ�����)�Ɉڂ�����
        time = DateTime.Now;
    }
}
