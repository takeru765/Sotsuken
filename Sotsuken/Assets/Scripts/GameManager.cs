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

            //�C�x���g�{�[�i�X�����Z�b�g
            eventBonus = 1.0f;
        }
    }

    //UI(�|�C���g�\��)�֘A
    [SerializeField] TextMeshProUGUI alumiViewer;
    [SerializeField] TextMeshProUGUI stealViewer;
    [SerializeField] TextMeshProUGUI petViewer;
    [SerializeField] TextMeshProUGUI plaViewer;
    [SerializeField] TextMeshProUGUI paperViewer;
    [SerializeField] TextMeshProUGUI allViewer;

    //�e�|�C���g�\���̕ύX
    void AlumiViewerChange(int i)
    {
        alumiViewer.text = "�A���~�@" + string.Format(" {0:G}", i) + "pt";
    }

    void StealViewerChange(int i)
    {
        stealViewer.text = "�X�`�[���@" + string.Format(" {0:G}", i) + "pt";
    }

    void PetViewerChange(int i)
    {
        petViewer.text = "�y�b�g�{�g���@" + string.Format(" {0:G}", i) + "pt";
    }

    void PlaViewerChange(int i)
    {
        plaViewer.text = "�v���X�`�b�N�@" + string.Format(" {0:G}", i) + "pt";
    }

    void PaperViewerChange(int i)
    {
        paperViewer.text = "���݁@" + string.Format(" {0:G}", i) + "pt";
    }

    void AllViewerChange(int i)
    {
        allViewer.text = "���������@" + string.Format(" {0:G}", i) + "pt";
    }

    //�S�|�C���g�\�����ꊇ�ύX
    void PointViewerChange()
    {
        AlumiViewerChange(alumiPoint);
        StealViewerChange(stealPoint);
        PetViewerChange(petPoint);
        PlaViewerChange(plaPoint);
        PaperViewerChange(paperPoint);
        AllViewerChange(allPoint);
    }

    //UI(�E�B���h�E)�֘A
    [SerializeField] GameObject BuildWindow0; //���񌚐ݗp�E�B���h�E
    [SerializeField] GameObject BuildWindow1; //���z�����x���A�b�v�p�E�B���h�E
    [SerializeField] GameObject MissionWindow; //�~�b�V�����p�E�B���h�E
    [SerializeField] GameObject InputWindow; //���T�C�N���}�[�N���͗p�E�B���h�E
    [SerializeField] GameObject EventWindow; //�C�x���g�p�E�B���h�E
    bool open = false; //���ɉ��炩�̃E�B���h�E���J���Ă���t���O

    [SerializeField] Image build1Picture;

    //���݃E�B���h�E�̊J��
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
    public void OpenBuild0(int i) //OpenBuild���ŌĂяo��
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

    public void OpenBuild1(int i) //OpenBuild���ŌĂяo��
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
            //���ݕ��ɍ��킹���摜��\��
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

    //�C�x���g�E�B���h�E�̊J��
    public void OpenEvent1()
    {
        if(open == false)
        {
            EventWindow.SetActive(true);
            open = true;

            //�C�x���g����ݒ�
            Event tmpEvent = ScriptableObject.CreateInstance("Event") as Event;
            tmpEvent = eventDataBase.eventList[0]; //[]���̔ԍ��̃C�x���g�����擾

            //�e�퐔�l��ݒ�
            eventBonus = tmpEvent.bonus;
            alumiBonus = tmpEvent.alumi;
            stealBonus = tmpEvent.steal;
            petBonus = tmpEvent.pet;
            plaBonus = tmpEvent.pla;
            paperBonus = tmpEvent.paper;

            //�C�x���g�����e�L�X�g��ύX
            eventText1.text = tmpEvent.intro;
        }
    }

    public void CloseEvent1()
    {
        EventWindow.SetActive(false);
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

    //�I�𒆂̌��z�ꏊ
    int selectedPlace = -1; //0�`5���Ή��B-1�͏����l�B
    //�I�𒆂̌��z��
    int selectedBuilding = 0; //BuildWindow0�Ŏg�p�B1�����T�C�N����A2����y�{��

    //���z�t���O��
    int[] place = {0, 0, 0, 0, 0, 0}; //���z�ꏊ1�`6�̌����B1�����T�C�N����A2����y�{��
    int[] lv = {0, 0, 0, 0, 0, 0}; //���z�ꏊ1�`6�̃��x��

    //�e���z�ꏊ�̉摜
    [SerializeField] Image buildImage0;
    [SerializeField] Image buildImage1;
    [SerializeField] Image buildImage2;
    [SerializeField] Image buildImage3;
    [SerializeField] Image buildImage4;
    [SerializeField] Image buildImage5;
    //���T�C�N����A��y�{�݂̉摜
    [SerializeField] Sprite recycleImage;
    [SerializeField] Sprite amusementImage;

    //���z���I��
    public void ChangeSelectedBuildeing(int i)
    {
        selectedBuilding = i;
    }

    //���݌���{�^��
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

    //���z�����x���A�b�v�{�^��
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
        //�ȉ�3�s�͓���m�F�p�ł��B�v��Ȃ��Ȃ���������Ă��������B
        Debug.Log(lv[0] + "," + lv[1] + "," + lv[2]);
        CalcBuildBonus();
        Debug.Log(inputRate + "," + (int)(5 * (inputRate + 0.01f)));
    }

    //���z�ɂ��{�[�i�X
    float inputRate = 1f; //�}�[�N���͔{��
    float eventRate = 1f; //�C�x���g�{��

    //���z�{�[�i�X�̍Čv�Z
    void CalcBuildBonus()
    {
        //�e��{�[�i�X�̏�����
        inputRate = 1f;
        eventRate = eventBonus;

        //���z�t���O�����ɍČv�Z
        for(int i = 0; i < 6; i++)
        {
            if(place[i] == 1)
            {
                inputRate += 0.2f * lv[i] + 0.0001f; //���̂܂܌v�Z����ƒ[���̓s���œ��������������Ȃ邱�Ƃ����邽�߁A+0.001���Ă܂��B
            }
            else if(place[i] == 2)
            {
                eventRate += 0.2f * lv[i] + 0.0001f; //�C�x���g�̕�V�ʂɉ����A.00...1�̕����͕ς��Ă��������B
            }
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
        //���z�{�[�i�X�E�C�x���g�{�[�i�X���v�Z
        CalcBuildBonus();

        //�����̓��͐��𔽉f
        todayAlumi += tmpAlumi;
        todaySteal += tmpSteal;
        todayPet += tmpPet;
        todayPla += tmpPla;
        todayPaper += tmpPaper;

        //���Z����|�C���g���v�Z
        if(alumiBonus == true)//�A���~
        {
            tmpAlumi = (int)(tmpAlumi * 5 * inputRate * eventRate);
        }
        else
        {
            tmpAlumi = (int)(tmpAlumi * 5 * inputRate);
        }

        if (stealBonus == true) //�X�`�[��
        {
            tmpSteal = (int)(tmpSteal * 5 * inputRate * eventRate);
        }
        else
        {
            tmpSteal = (int)(tmpSteal * 5 * inputRate);
        }

        if (petBonus == true)//�y�b�g�{�g��
        {
            tmpPet = (int)(tmpPet * 5 * inputRate * eventRate);
        }
        else
        {
            tmpPet = (int)(tmpPet * 5 * inputRate);
        }

        if (plaBonus == true)//�v���X�`�b�N
        {
            tmpPla = (int)(tmpPla * 5 * inputRate * eventRate);
        }
        else
        {
            tmpPla = (int)(tmpPla * 5 * inputRate);
        }

        if (paperBonus == true)//��
        {
            tmpPaper = (int)(tmpPaper * 5 * inputRate * eventRate);
        }
        else
        {
            tmpPaper = (int)(tmpPaper * 5 * inputRate);
        }


        //�e�}�[�N�̃|�C���g�𔽉f
        alumiPoint += tmpAlumi;
        stealPoint += tmpSteal;
        petPoint += tmpPet;
        plaPoint += tmpPla;
        paperPoint += tmpPaper;
        allPoint += tmpAlumi + tmpSteal + tmpPet + tmpPla + tmpPaper;

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

        //�|�C���g�\��UI�ɔ��f
        PointViewerChange();
    }

    //�C�x���g�֘A
    [SerializeField] EventDataBase eventDataBase; //�C�x���g�f�[�^�x�[�X�̊Ǘ�
    public void AddEventData(Event i)
    {
        eventDataBase.eventList.Add(i);
    }

    float eventBonus = 1.0f; //�C�x���g�ɂ��{�[�i�X�{���B

    //�Ώۃ}�[�N
    bool alumiBonus = false;
    bool stealBonus = false;
    bool petBonus = false;
    bool plaBonus = false;
    bool paperBonus = false;

    //�C�x���g�����e�L�X�g
    [SerializeField] TextMeshProUGUI eventText1; //�{�[�i�X�n�C�x���g�p

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

        //�|�C���g�\��UI�ɔ��f
        PointViewerChange();
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
