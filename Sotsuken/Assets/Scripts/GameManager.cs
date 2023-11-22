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

    //���t�ύX�`�F�b�N
    int day = 0;
    int month = 0;
    int year = 0;
    void CheckDate()
    {
        if(time.Year > year || time.Month > month || time.Day > day)
        {
            //�e�퐔�l�E�t���O�����Z�b�g
            setMission = false;
            successMission = false;
            count = 0;
            todayAlumi = 0;
            todaySteal = 0;
            todayPet = 0;
            todayPla = 0;
            todayPaper = 0;

            //���t���X�V
            year = time.Year;
            month = time.Month;
            day = time.Day;
        }
    }

    //UI(�E�B���h�E)�֘A
    [SerializeField] GameObject BuildWindow; //���ݗp�E�B���h�E
    [SerializeField] GameObject MissionWindow; //�~�b�V�����p�E�B���h�E
    [SerializeField] GameObject InputWindow; //���T�C�N���}�[�N���͗p�E�B���h�E
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

            //�~�b�V�����̐����E���s�𔻒�
            CheckMission();
            CheckDate();
        }
    }

    public void CloseMission()
    {
        MissionWindow.SetActive(false);
        open = false;
    }

    //���T�C�N���}�[�N���̓E�B���h�E�̊J��
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
    int place_1 = 0; //���z�ꏊ����1�̌���
    int lv_1 = 0; //���z�ꏊ����1�̃��x��
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

    //���z�ɂ��{�[�i�X
    float inputRate = 1f; //�}�[�N���͔{��
    float eventRate = 1f; //�C�x���g�{��

    //���z�{�[�i�X�̍Čv�Z
    void calcBuildBonus()
    {
        //�e��{�[�i�X�̏�����
        inputRate = 1f;
        eventRate = 1f;

        //���z�t���O�����ɍČv�Z
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


    //���T�C�N���}�[�N���͊֘A
    int tmpAlumi = 0; //���͓r���̈ꎞ�ۑ��p
    int tmpSteal = 0;
    int tmpPet = 0;
    int tmpPla = 0;
    int tmpPaper = 0;

    [SerializeField] TextMeshProUGUI alumiText; //���͓r���̕\���p
    [SerializeField] TextMeshProUGUI stealText;
    [SerializeField] TextMeshProUGUI petText;
    [SerializeField] TextMeshProUGUI plaText;
    [SerializeField] TextMeshProUGUI paperText;

    //�}�[�N�����{�^��
    public void AddInputMark(int mark) //mark��1=�A���~�ʁA2=�X�`�[���ʁA3=�y�b�g�{�g���A4=�v���X�`�b�N�A5=��
    {
        switch(mark)
        {
            case 1:
                tmpAlumi += 1;
                //�ꉞ�A�����10�ɂ��Ă���
                if(tmpAlumi > 10)
                {
                    tmpAlumi = 10;
                }

                //�e�L�X�g����
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
                //�ꉞ�A�����10�ɂ��Ă���
                if (tmpSteal > 10)
                {
                    tmpSteal = 10;
                }

                //�e�L�X�g����
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
                //�ꉞ�A�����10�ɂ��Ă���
                if (tmpPet > 10)
                {
                    tmpPet = 10;
                }

                //�e�L�X�g����
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
                //�ꉞ�A�����10�ɂ��Ă���
                if (tmpPla > 10)
                {
                    tmpPla = 10;
                }

                //�e�L�X�g����
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
                //�ꉞ�A�����10�ɂ��Ă���
                if (tmpPaper > 10)
                {
                    tmpPaper = 10;
                }

                //�e�L�X�g����
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

    //�}�[�N�����{�^��
    public void MinusInputMark(int mark) //mark��1=�A���~�ʁA2=�X�`�[���ʁA3=�y�b�g�{�g���A4=�v���X�`�b�N�A5=��
    {
        switch (mark)
        {
            case 1:
                tmpAlumi -= 1;
                //�ꉞ�A������0�ɂ��Ă���
                if (tmpAlumi < 0)
                {
                    tmpAlumi = 0;
                }

                //�e�L�X�g����
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

                //�e�L�X�g����
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

                //�e�L�X�g����
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

                //�e�L�X�g����
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

                //�e�L�X�g����
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

    public void InputEnter() //���T�C�N���}�[�N���͂��m��
    {
        //�e�}�[�N�̃|�C���g�Ɠ����̓��͐��𔽉f
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

        //tmp������������
        tmpAlumi = 0;
        tmpSteal = 0;
        tmpPet = 0;
        tmpPla = 0;
        tmpPaper = 0;
        //�\��UI�ɔ��f
        alumiText.text = string.Format(" {0:G}", tmpAlumi);
        stealText.text = string.Format(" {0:G}", tmpSteal);
        petText.text = string.Format(" {0:G}", tmpPet);
        plaText.text = string.Format(" {0:G}", tmpPla) ;
        paperText.text = string.Format(" {0:G}", tmpPaper);

        //�f�o�b�O�\��
        Debug.Log("�A���~=" + alumiPoint + "�A�X�`�[��=" + stealPoint + "�A�y�b�g�{�g��=" + petPoint + "�A�v���X�`�b�N=" + plaPoint + "�A��=" + paperPoint);
    }

    //�C�x���g�֘A


    //�~�b�V�����֘A
    int goal = 1; //�ݒ肵���ڕW
    int count = 0; //���݂̃}�[�N�l����
    int mark = 1; //�ݒ肵���}�[�N�̎�� 1�`5=�A���~�A�X�`�[���A�y�b�g�A�v���A��
    int markRec1 = 0; //�O���̃}�[�N�̎��
    int markRec2 = 0; //�O�X���̃}�[�N�̎��
    [SerializeField] TextMeshProUGUI goalText; //�ڕW�̐��l�e�L�X�g
    bool setMission = false; //�~�b�V�����m��ς݃t���O
    bool successMission = false; //�~�b�V�����B���ς݃t���O
    int setYear = 10000; //�~�b�V�����m��N
    int setMonth = 10000; //�~�b�V�����m�茎
    int setDay = 10000; //�~�b�V�����m���

    [SerializeField] GameObject MissionSuccess; //�~�b�V�����������b�Z�[�W
    [SerializeField] GameObject MissionFailed; //�~�b�V�������s���b�Z�[�W

    //�~�b�V�����̃}�[�N��ݒ肷��B
    public void SetMark(int m)
    {
        if(!setMission)
        {
            mark = m;
        }

        Debug.Log("mark = " + mark);
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
            //�����Ƀ~�b�V�����������̏���������
            SuccessMission();
        }

        if (time.Year > setYear || time.Month > setMonth || time.Day > setDay) //�����ɂȂ��Ă邩����
        {
            if(setMission ==true && successMission ==false)
            {
                FailedMission();
            }
        }

        Debug.Log("�|�C���g��" + alumiPoint + "," + stealPoint + "," + plaPoint + "," + petPoint + "," + paperPoint);
    }

    //�~�b�V������������
    void SuccessMission()
    {
        int reward = 0;

        //�l���|�C���g���v�Z
        for(int i = 1; i <= goal; i++)
        {
            reward += i * 5;
        }

        //�ݒ肵���}�[�N�Ƀ|�C���g���Z
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

        //�O���A�O�X���̃}�[�N���X�V
        markRec2 = markRec1;
        markRec1 = mark;

        //�~�b�V�����B���t���O��ON
        successMission = true;

        //�~�b�V���������E�B���h�E��\��
        MissionSuccess.SetActive(true);
    }

    //�~�b�V�������s����
    void FailedMission()
    {
        int reward = 0;

        //�l���|�C���g�̌v�Z
        for(int i = 1; i < count; i++)
        {
            reward += i * 5;
        }

        reward += count * 5 / 2;

        //�ݒ肵���}�[�N�Ƀ|�C���g�����Z
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

        //�O���A�O�X���̃}�[�N���X�V
        markRec2 = markRec1;
        markRec1 = mark;

        //�~�b�V��������t���O������
        setMission = false;

        //�~�b�V�������s�E�B���h�E��\��
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
        //���ݓ������擾�B�������d���ꍇ�A�ʂ̏ꏊ(��ʐ؂�ւ�����)�Ɉڂ�����
        time = DateTime.Now;
    }
}
