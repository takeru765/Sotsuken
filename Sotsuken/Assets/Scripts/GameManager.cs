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

            //�C�x���g���e�������_���Ō���
            eventID = UnityEngine.Random.Range(0, 6); //Random�́A����̒l�͊܂܂Ȃ����Ƃɒ���

            OpenEvent();
        }
    }

    //�Z�[�u�f�[�^�֘A
    [HideInInspector] public SaveData save; //�Z�[�u�f�[�^
    string filePath; //json(�Z�[�u�f�[�^)�t�@�C���̃p�X
    string fileName = "save.json"; //json�̃t�@�C����

    //�Z�[�u����
    void Save(SaveData data)
    {
        //save�ւ̏�񏑂�����
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
        save.answered = answered;

        //�f�[�^���̂̕ۑ�����
        string json = JsonUtility.ToJson(data); //SaveData��json�`���ɕϊ�
        StreamWriter wr = new StreamWriter(filePath, false); //json��filePath�̈ʒu�ɏ�������
        wr.WriteLine(json);
        wr.Close();
    }

    //���[�h����1�Bjson�t�@�C����SaveData�`���ɕϊ�����
    SaveData Load1(string path)
    {
        StreamReader rd = new StreamReader(path); //�t�@�C���p�X���w��
        string json = rd.ReadToEnd(); //�t�@�C���p�X�ɂ���json�t�@�C����ǂݍ���
        rd.Close();

        return JsonUtility.FromJson<SaveData>(json); //SaveData�ɕϊ����ĕԂ�
    }

    //���[�h����2�BSavaData�̓��e���Q�[���ɔ��f����
    void Load2()
    {
        //�e�퐔�l�̔��f
        year = save.year;
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

        //�e��UI�ɔ��f
        PointViewerChange();
        CheckBuildImage();
    }

    //�f�o�b�O�p�Z�[�u�f�[�^����
    public void ResetSave()
    {
        //save�̏�����
        save = new SaveData();
        save.day = time.Day;
        save.month = time.Month;
        save.year = time.Year;
        string json = JsonUtility.ToJson(save); //SaveData��json�`���ɕϊ�
        StreamWriter wr = new StreamWriter(filePath, false); //json��filePath�̈ʒu�ɏ�������
        wr.WriteLine(json);
        wr.Close();

        //�����������Z�[�u�f�[�^��ǂݍ���
        save = Load1(filePath);
        Load2();
    }

    //�t�F�[�h�C���E�A�E�g����
    [SerializeField] GameObject fadeObject;
    [SerializeField] Image fadeImage;
    bool isFadeIn = true; //�t�F�[�h�C�����t���O
    bool isFadeOut = false; //�t�F�[�h�A�E�g���t���O
    float alfa = 1f; //�t�F�[�h�摜�̕s�����x

    void FadeIn(float speed = 0.75f) //�t�F�[�h�C�������B�t�F�[�h���x���w��\
    {
        fadeObject.SetActive(true);

        alfa -= speed * Time.deltaTime; //���Ԍo�߂ɉ����ĕs�����x��ቺ
        fadeImage.color = new Color(0, 0, 0, alfa); //�s�����x�𔽉f

        if(alfa < 0)
        {
            alfa = 0;
            isFadeIn = false;

            if(opSequence == 0)
            {
                audioSource.PlayOneShot(openWindow); //�`���[�g���A���J�n���̌��ʉ�
            }
            fadeImage.raycastTarget = false; //�t�F�[�h�摜�̃N���b�N����𖳌���(���ɂ���{�^�����N���b�N�����邽��)
        }
    }

    void FadeOut(float speed = 0.75f)
    {
        fadeImage.raycastTarget = true; //�t�F�[�h�摜�̃N���b�N�����L����(�e��{�^�����N���b�N�ł��Ȃ��悤�ɂ���)

        alfa += speed * Time.deltaTime; //���Ԍo�߂ɉ����ĕs�����x�𑝉�
        fadeImage.color = new Color(0, 0, 0, alfa); //�s�����x�𔽉f

        if (alfa >= 1)
        {
            alfa = 1;
            isFadeOut = false;

            SceneManager.LoadScene("Odajima_bookpart");
        }
    }

    public void StartFadeOut()
    {
        if(canBook == true && open == false)
        {
            if(opSequence == 53)
            {
                opSequence = 60;
            }
            Save(save);
            isFadeOut = true;
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
    [SerializeField] GameObject EventWindow1; //�{�[�i�X�n�C�x���g�p�E�B���h�E
    [SerializeField] GameObject EventWindow2; //�N�C�Y�n�C�x���g�E�B���h�E
    bool open = false; //���ɉ��炩�̃E�B���h�E���J���Ă���t���O

    //���z�E�B���h�E�̉摜�A����
    [SerializeField] Image build1Picture;
    [SerializeField] TextMeshProUGUI buildIntro;

    //���݃E�B���h�E�̊J��
    public void OpenBuild(int i)
    {
        if(canBuild == true)
        {
            if (lv[i] == 0)
            {
                if(opSequence == 40) //�`���[�g���A���p�̏����B����̓y�n������I���ł���悤�ɂ���B
                {
                    if(i == 0)
                    {
                        OpenBuild0(i);
                        opSequence = 41;
                    }
                }
                else //�`���[�g���A���O
                {
                    OpenBuild0(i);
                }
            }
            else
            {
                OpenBuild1(i);
            }
        }
    }
    public void OpenBuild0(int i) //OpenBuild���ŌĂяo��
    {
        if(open == false)
        {
            selectedPlace = i;
            BuildWindow0.SetActive(true);
            open = true;

            audioSource.PlayOneShot(openWindow); //���ʉ��Đ�
        }
    }

    public void CloseBuild0(bool i)
    {
        if(canBuild == true)
        {
            selectedPlace = -1;
            selectedBuilding = 0;
            BuildWindow0.SetActive(false);
            open = false;

            if(i == true) //���̌��ʉ�������ꍇ�͍Đ����Ȃ��悤��
            {
                audioSource.PlayOneShot(closeWindow); //���ʉ��Đ�
            }
        }

        //�~�b�V�����̐����E���s�𔻒�
        CheckMission();
        CheckDate();
        Save(save); //�I�[�g�Z�[�u
    }

    public void OpenBuild1(int i) //OpenBuild���ŌĂяo��
    {
        selectedPlace = i;
        
        if (open == false)
        {
            selectedPlace = i;
            BuildWindow1.SetActive(true);
            open = true;
            audioSource.PlayOneShot(openWindow); //���ʉ��Đ�

            //���ݕ��ɍ��킹���摜��\��
            if (place[i] == 1)
            {
                build1Picture.sprite = recycleImage;
            }
            else if (place[i] == 2)
            {
                build1Picture.sprite = amusementImage;
            }
            //����LV�ɍ��킹����������\��
            switch(lv[i])
            {
                case 1:
                    if(place[i] == 1)
                    {
                        buildIntro.text = "LV:1��2\n�R�X�g:�e30pt\n�{�[�i�X:+20%��+40%";
                    }
                    else if(place[i] == 2)
                    {
                        buildIntro.text = "LV:1��2\n�R�X�g:�e30pt\n�{�[�i�X:+20%��+40%";
                    }
                    break;
                case 2:
                    if (place[i] == 1)
                    {
                        buildIntro.text = "LV:2��3\n�R�X�g:�e45pt\n�{�[�i�X:+40%��+60%";
                    }
                    else if (place[i] == 2)
                    {
                        buildIntro.text = "LV:2��3\n�R�X�g:�e45pt\n�{�[�i�X:+40%��+60%";
                    }
                    break;
                case 3:
                    if (place[i] == 1)
                    {
                        buildIntro.text = "LV:3��4\n�R�X�g:�e60pt\n�{�[�i�X:+60%��+80%";
                    }
                    else if (place[i] == 2)
                    {
                        buildIntro.text = "LV:3��4\n�R�X�g:�e60pt\n�{�[�i�X:+60%��+80%";
                    }
                    break;
                case 4:
                    buildIntro.text = "LV:4(Max)\n�����������x���ł��B";
                    break;
                default:
                    break;
            }
        }
    }

    public void CloseBuild1()
    {
        if(canBuild == true)
        {
            selectedPlace = 0;
            BuildWindow1.SetActive(false);
            open = false;

            audioSource.PlayOneShot(closeWindow); //���ʉ��Đ�
        }

        //�~�b�V�����̐����E���s�𔻒�
        CheckMission();
        CheckDate();
        Save(save); //�I�[�g�Z�[�u
    }

    //�~�b�V�����E�B���h�E�̊J��
    public void OpenMission()
    {

        if(open == false && canMission == true)
        {
            MissionWindow.SetActive(true);
            open = true;
            audioSource.PlayOneShot(openWindow); //���ʉ��Đ�
        }

        //UI��̖ڕW�̕\����ύX����
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
        if (canMission == true)
        {
            MissionWindow.SetActive(false);
            open = false;

            audioSource.PlayOneShot(closeWindow); //���ʉ��Đ�
        }

        //�~�b�V�����̐����E���s�𔻒�
        CheckMission();
        CheckDate();
        Save(save); //�I�[�g�Z�[�u
    }

    //���T�C�N���}�[�N���̓E�B���h�E�̊J��
    public void OpenInput()
    {
        if (open == false && canInput ==true)
        {
            InputWindow.SetActive(true);
            open = true;
            audioSource.PlayOneShot(openWindow); //���ʉ��Đ�
        }

        if(opSequence == 10) //�`���[�g���A���i�s�Ǘ�
        {
            opSequence = 11;
        }
    }

    public void CloseInput(bool i)
    {
        if(canInput == true)
        {
            InputWindow.SetActive(false);
            open = false;

            if(i == true)
            {
                audioSource.PlayOneShot(closeWindow); //���ʉ��Đ�
            }
        }

        //�~�b�V�����̐����E���s�𔻒�
        CheckMission();
        CheckDate();
        Save(save); //�I�[�g�Z�[�u
    }

    //�C�x���g�E�B���h�E�̊J��
    public void OpenEvent()
    {
        if(open == false && canEvent == true)
        {
            //�C�x���g�����擾
            Event tmpEvent = ScriptableObject.CreateInstance("Event") as Event;
            tmpEvent = eventDataBase.eventList[eventID]; //[]���̔ԍ��̃C�x���g�����擾

            if(tmpEvent.isQuiz == false) //�{�[�i�X�n�C�x���g�̏ꍇ
            {
                EventWindow1.SetActive(true);

                //�e�퐔�l��ݒ�
                eventBonus = tmpEvent.bonus;
                alumiBonus = tmpEvent.alumi;
                stealBonus = tmpEvent.steal;
                petBonus = tmpEvent.pet;
                plaBonus = tmpEvent.pla;
                paperBonus = tmpEvent.paper;

                //�C�x���g�����e�L�X�g��ύX
                eventText1.text = tmpEvent.intro;

                //�C�x���g�摜��ύX
                eventImage1.sprite = tmpEvent.eventImage;
            }
            else //�N�C�Y�n�C�x���g�̏ꍇ
            {
                EventWindow2.SetActive(true);

                //�C�x���g�����e�L�X�g��ύX
                eventText2.text = tmpEvent.intro;
                answerText1.text = tmpEvent.answer1;
                answerText2.text = tmpEvent.answer2;
                answerText3.text = tmpEvent.answer3;

                //�C�x���g�摜��ύX
                eventImage2.sprite = tmpEvent.eventImage;

                //�N�C�Y�����ɕK�v�ȏ���ۑ�
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
            audioSource.PlayOneShot(openWindow); //���ʉ��Đ�
        }
    }

    public void CloseEvent1()
    {
        if (canEvent == true)
        {
            EventWindow1.SetActive(false);
            open = false;

            audioSource.PlayOneShot(closeWindow); //���ʉ��Đ�
        }

        //�~�b�V�����̐����E���s�𔻒�
        CheckMission();
        CheckDate();
        Save(save); //�I�[�g�Z�[�u
    }

    public void CloseEvent2()
    {
        EventWindow2.SetActive(false);
        open = false;

        if(opSequence == 52)
        {
            opSequence = 53;
        }

        audioSource.PlayOneShot(closeWindow); //���ʉ��Đ�

        //�~�b�V�����̐����E���s�𔻒�
        CheckMission();
        CheckDate();
        Save(save); //�I�[�g�Z�[�u
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

    //����܂Ŋl�������݌v�̃}�[�N��(�}�ӁE�̍��p)
    int alumiBook = 0;
    int stealBook = 0;
    int petBook = 0;
    int plaBook = 0;
    int paperBook = 0;

    //SE�f��
    AudioSource audioSource;
    [SerializeField] AudioClip openWindow;
    [SerializeField] AudioClip closeWindow;
    [SerializeField] AudioClip valueChange;
    [SerializeField] AudioClip inputEnter;
    [SerializeField] AudioClip enter;

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
    //�󂫒n�A���T�C�N����A��y�{�݂̉摜
    [SerializeField] Sprite blankImage;
    [SerializeField] Sprite recycleImage;
    [SerializeField] Sprite amusementImage;

    //���z�{�^���ւ̉摜�̔��f(�N�����p��z��)
    void CheckBuildImage()
    {
        for(int i = 0; i < 6; i++)
        {
            if(place[i] == 0)
            {
                switch (i)
                {
                    case 0:
                        buildImage0.sprite = blankImage;
                        break;
                    case 1:
                        buildImage1.sprite = blankImage;
                        break;
                    case 2:
                        buildImage2.sprite = blankImage;
                        break;
                    case 3:
                        buildImage3.sprite = blankImage;
                        break;
                    case 4:
                        buildImage4.sprite = blankImage;
                        break;
                    case 5:
                        buildImage5.sprite = blankImage;
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

    //���z���I��
    public void ChangeSelectedBuildeing(int i)
    {
        selectedBuilding = i;
    }

    //���݌���{�^��
    public void DecideBuild()
    {
        if(selectedBuilding != 0) //���Ă���̂�I�����Ă���
        {
            bool enoughBuildPoint = false;

            switch(selectedBuilding) //���z�ɕK�v�ȃ|�C���g�����邩�m�F
            {
                case 1:
                    if(alumiPoint >= 15 && stealPoint >= 15 && petPoint >= 15 && plaPoint >= 15 && paperPoint >= 15)
                    {
                        enoughBuildPoint = true;
                    }
                    break;
                case 2:
                    if (alumiPoint >= 15 && stealPoint >= 15 && petPoint >= 15 && plaPoint >= 15 && paperPoint >= 15)
                    {
                        enoughBuildPoint = true;
                    }
                    break;
                default:
                    break;
            }

            //�|�C���g������Ă�Ό��z���������s
            if(enoughBuildPoint == true)
            {
                place[selectedPlace] = selectedBuilding;
                lv[selectedPlace] = 1;
                switch (selectedPlace)
                {
                    case 0:
                        if (selectedBuilding == 1)
                        {
                            buildImage0.sprite = recycleImage;
                            alumiPoint -= 15;
                            stealPoint -= 15;
                            petPoint -= 15;
                            plaPoint -= 15;
                            paperPoint -= 15;
                        }
                        else if (selectedBuilding == 2)
                        {
                            buildImage0.sprite = amusementImage;
                            alumiPoint -= 15;
                            stealPoint -= 15;
                            petPoint -= 15;
                            plaPoint -= 15;
                            paperPoint -= 15;
                        }
                        break;
                    case 1:
                        if (selectedBuilding == 1)
                        {
                            buildImage1.sprite = recycleImage;
                            alumiPoint -= 15;
                            stealPoint -= 15;
                            petPoint -= 15;
                            plaPoint -= 15;
                            paperPoint -= 15;
                        }
                        else if (selectedBuilding == 2)
                        {
                            buildImage1.sprite = amusementImage;
                            alumiPoint -= 15;
                            stealPoint -= 15;
                            petPoint -= 15;
                            plaPoint -= 15;
                            paperPoint -= 15;
                        }
                        break;
                    case 2:
                        if (selectedBuilding == 1)
                        {
                            buildImage2.sprite = recycleImage;
                            alumiPoint -= 15;
                            stealPoint -= 15;
                            petPoint -= 15;
                            plaPoint -= 15;
                            paperPoint -= 15;
                        }
                        else if (selectedBuilding == 2)
                        {
                            buildImage2.sprite = amusementImage;
                            alumiPoint -= 15;
                            stealPoint -= 15;
                            petPoint -= 15;
                            plaPoint -= 15;
                            paperPoint -= 15;
                        }
                        break;
                    case 3:
                        if (selectedBuilding == 1)
                        {
                            buildImage3.sprite = recycleImage;
                            alumiPoint -= 15;
                            stealPoint -= 15;
                            petPoint -= 15;
                            plaPoint -= 15;
                            paperPoint -= 15;
                        }
                        else if (selectedBuilding == 2)
                        {
                            buildImage3.sprite = amusementImage;
                            alumiPoint -= 15;
                            stealPoint -= 15;
                            petPoint -= 15;
                            plaPoint -= 15;
                            paperPoint -= 15;
                        }
                        break;
                    case 4:
                        if (selectedBuilding == 1)
                        {
                            buildImage4.sprite = recycleImage;
                            alumiPoint -= 15;
                            stealPoint -= 15;
                            petPoint -= 15;
                            plaPoint -= 15;
                            paperPoint -= 15;
                        }
                        else if (selectedBuilding == 2)
                        {
                            buildImage4.sprite = amusementImage;
                            alumiPoint -= 15;
                            stealPoint -= 15;
                            petPoint -= 15;
                            plaPoint -= 15;
                            paperPoint -= 15;
                        }
                        break;
                    case 5:
                        if (selectedBuilding == 1)
                        {
                            buildImage5.sprite = recycleImage;
                            alumiPoint -= 15;
                            stealPoint -= 15;
                            petPoint -= 15;
                            plaPoint -= 15;
                            paperPoint -= 15;
                        }
                        else if (selectedBuilding == 2)
                        {
                            buildImage5.sprite = amusementImage;
                            alumiPoint -= 15;
                            stealPoint -= 15;
                            petPoint -= 15;
                            plaPoint -= 15;
                            paperPoint -= 15;
                        }
                        break;
                    default:
                        break;
                }

                if (opSequence == 41) //�`���[�g���A���Ǘ��p
                {
                    opSequence = 42;
                    canBuild = true;
                }


                audioSource.PlayOneShot(enter); //���ʉ��Đ�
                PointViewerChange();
                Save(save);
                CloseBuild0(false);
            }
            
        }
    }

    //���z�����x���A�b�v�{�^��
    public void DecideLevelUp()
    {
        switch(lv[selectedPlace])
        {
            case 1:
                if (place[selectedPlace] == 1 && alumiPoint >= 30 && stealPoint >= 30 && petPoint >= 30 && plaPoint >= 30 && paperPoint >= 30)
                {
                    lv[selectedPlace] += 1;
                    alumiPoint -= 30;
                    stealPoint -= 30;
                    petPoint -= 30;
                    plaPoint -= 30;
                    paperPoint -= 30;
                }
                else if (place[selectedPlace] == 2 && alumiPoint >= 30 && stealPoint >= 30 && petPoint >= 30 && plaPoint >= 30 && paperPoint >= 30)
                {
                    lv[selectedPlace] += 1;
                    alumiPoint -= 30;
                    stealPoint -= 30;
                    petPoint -= 30;
                    plaPoint -= 30;
                    paperPoint -= 30;
                }
                CloseBuild1();
                break;
            case 2:
                if (place[selectedPlace] == 1 && alumiPoint >= 45 && stealPoint >= 45 && petPoint >= 45 && plaPoint >= 45 && paperPoint >= 45)
                {
                    lv[selectedPlace] += 1;
                    alumiPoint -= 45;
                    stealPoint -= 45;
                    petPoint -= 45;
                    plaPoint -= 45;
                    paperPoint -= 45;
                }
                else if (place[selectedPlace] == 2 && alumiPoint >= 45 && stealPoint >= 45 && petPoint >= 45 && plaPoint >= 45 && paperPoint >= 45)
                {
                    lv[selectedPlace] += 1;
                    alumiPoint -= 45;
                    stealPoint -= 45;
                    petPoint -= 45;
                    plaPoint -= 45;
                    paperPoint -= 45;
                }
                CloseBuild1();
                break;
            case 3:
                if (place[selectedPlace] == 1 && alumiPoint >= 60 && stealPoint >= 60 && petPoint >= 60 && plaPoint >= 60 && paperPoint >= 60)
                {
                    lv[selectedPlace] += 1;
                    alumiPoint -= 60;
                    stealPoint -= 60;
                    petPoint -= 60;
                    plaPoint -= 60;
                    paperPoint -= 60;
                }
                else if (place[selectedPlace] == 2 && alumiPoint >= 60 && stealPoint >= 60 && petPoint >= 60 && plaPoint >= 60 && paperPoint >= 60)
                {
                    lv[selectedPlace] += 1;
                    alumiPoint -= 60;
                    stealPoint -= 60;
                    petPoint -= 60;
                    plaPoint -= 60;
                    paperPoint -= 60;
                }
                CloseBuild1();
                break;
            default:
                CloseBuild1();
                break;
        }

        PointViewerChange();
        Save(save);
        //�ȉ�3�s�͓���m�F�p�ł��B�v��Ȃ��Ȃ���������Ă��������B
        /*
        Debug.Log(lv[0] + "," + lv[1] + "," + lv[2]);
        CalcBuildBonus();
        Debug.Log(inputRate + "," + (int)(5 * (inputRate + 0.01f)));
        */
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

        audioSource.PlayOneShot(valueChange); //���ʉ��Đ�
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

        audioSource.PlayOneShot(valueChange); //���ʉ��Đ�
    }

    //���T�C�N���}�[�N���͂��m��
    public void InputEnter()
    {
        //���z�{�[�i�X�E�C�x���g�{�[�i�X���v�Z
        CalcBuildBonus();

        //�����̓��͐��𔽉f
        todayAlumi += tmpAlumi;
        todaySteal += tmpSteal;
        todayPet += tmpPet;
        todayPla += tmpPla;
        todayPaper += tmpPaper;

        //�݌v�}�[�N�l�����ɔ��f
        alumiBook += tmpAlumi;
        stealBook += tmpSteal;
        petBook += tmpPet;
        plaBook += tmpPla;
        paperBook += tmpPaper;

        //���Z����|�C���g���v�Z
        if(alumiBonus == true)//�A���~
        {
            tmpAlumi = (int)(tmpAlumi * 15 * inputRate * eventRate);
        }
        else
        {
            tmpAlumi = (int)(tmpAlumi * 15 * inputRate);
        }

        if (stealBonus == true) //�X�`�[��
        {
            tmpSteal = (int)(tmpSteal * 15 * inputRate * eventRate);
        }
        else
        {
            tmpSteal = (int)(tmpSteal * 15 * inputRate);
        }

        if (petBonus == true)//�y�b�g�{�g��
        {
            tmpPet = (int)(tmpPet * 15 * inputRate * eventRate);
        }
        else
        {
            tmpPet = (int)(tmpPet * 15 * inputRate);
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
            tmpPaper = (int)(tmpPaper * 15 * inputRate * eventRate);
        }
        else
        {
            tmpPaper = (int)(tmpPaper * 15 * inputRate);
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

        audioSource.PlayOneShot(inputEnter); //���ʉ��Đ�

        //�`���[�g���A���i�s
        if (opSequence == 11 && allPoint > 0)
        {
            canInput = true;
            opSequence = 12;
            CloseInput(false);
        }

        Save(save);
    }

    //�C�x���g�֘A
    [SerializeField] EventDataBase eventDataBase; //�C�x���g�f�[�^�x�[�X�̊Ǘ�
    public void AddEventData(Event i)
    {
        eventDataBase.eventList.Add(i);
    }

    float eventBonus = 1.0f; //�C�x���g�ɂ��{�[�i�X�{���B
    int eventID = 0; //�����̃C�x���gID

    //�Ώۃ}�[�N
    bool alumiBonus = false;
    bool stealBonus = false;
    bool petBonus = false;
    bool plaBonus = false;
    bool paperBonus = false;

    //�N�C�Y�̐������Ǘ�
    int correctAnswer = 0;

    //�N�C�Y�̕�V�Ǘ�
    int rewardPoint = 0; //��V�|�C���g
    int rewardMark = 0; //��V�̃}�[�N��ށA1�`5�ŊǗ�
    bool answered = false; //�����ς݃t���O

    //�C�x���g�����e�L�X�g
    [SerializeField] TextMeshProUGUI eventText1; //�{�[�i�X�n�C�x���g�p
    [SerializeField] TextMeshProUGUI eventText2; //�N�C�Y�n�C�x���g�p
    [SerializeField] TextMeshProUGUI answerText1; //�𓚃{�^���p1
    [SerializeField] TextMeshProUGUI answerText2; //�𓚃{�^���p2
    [SerializeField] TextMeshProUGUI answerText3; //�𓚃{�^���p3

    //�C�x���g�摜�\��
    [SerializeField] Image eventImage1; //�{�[�i�X�n�C�x���g�p
    [SerializeField] Image eventImage2; //�N�C�Y�n�C�x���g�p

    //�N�C�Y�C�x���g�𓚏���
    public void CheckAnswer(int i)
    {
        if (i == correctAnswer && answered == false && canEvent == true)
        {
            //���z�{�[�i�X���v�Z
            for (int j = 0; j < 6; j++)
            {
                if (place[j] == 2)
                {
                    eventRate = 1 +0.2f * lv[j] + 0.0001f; //�C�x���g�̕�V�ʂɉ����A.00...1�̕����͕ς��Ă��������B
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

            //�|�C���g�\���ɔ��f
            PointViewerChange();

            //�����ς݃t���O��ON
            answered = true;

            //audioSource.PlayOneShot(); //�������𗬂�
            Save(save);
            CloseEvent2();
        }
        else if(answered == false && canEvent == true)
        {
            //audioSource.PlayOneShot(); //�s�������𗬂�
        }
    }



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

    [SerializeField] GameObject MissionSuccess; //�~�b�V���������E�B���h�E
    [SerializeField] TextMeshProUGUI MSuccessText; //�~�b�V���������e�L�X�g
    [SerializeField] GameObject MissionFailed; //�~�b�V�������s�E�B���h�E
    [SerializeField] TextMeshProUGUI MFailedText; //�~�b�V�������s�e�L�X�g

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

        audioSource.PlayOneShot(valueChange); //���ʉ��Đ�
    }

    //�~�b�V�����m��
    public void SetMission()
    {
        if(setMission == false)
        {
            audioSource.PlayOneShot(enter); //���ʉ��Đ�
        }
        setMission = true;

        //�����̓��t��ۑ�
        setYear = time.Year;
        setMonth = time.Month;
        setDay = time.Day;

        Save(save);
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
                Debug.Log("markError");
                break;
        }
        Debug.Log("flag" + successMission + "count" + count + ",todayAlumi" + todayAlumi);

        if (setMission == true && count >= goal && successMission == false)
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
        switch(mark)
        {
            case 1:
                MSuccessText.text = "�~�b�V���������I\n�A���~�ʃ|�C���g��" + reward + "�l�������I";
                break;
            case 2:
                MSuccessText.text = "�~�b�V���������I\n�X�`�[���ʃ|�C���g��" + reward + "�l�������I";
                break;
            case 3:
                MSuccessText.text = "�~�b�V���������I\n�y�b�g�{�g���|�C���g��" + reward + "�l�������I";
                break;
            case 4:
                MSuccessText.text = "�~�b�V���������I\n�v���X�`�b�N�|�C���g��" + reward + "�l�������I";
                break;
            case 5:
                MSuccessText.text = "�~�b�V���������I\n���|�C���g��" + reward + "�l�������I";
                break;
            default:
                break;
        }

        Save(save);
    }

    //�~�b�V�������s����
    void FailedMission()
    {
        int reward = 0;

        //�l���|�C���g�̌v�Z
        if(count > 0)
        {
            for (int i = 0; i < count; i++)
            {
                reward += i * 5;
            }
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
        switch (mark)
        {
            case 1:
                MSuccessText.text = "�~�b�V�������s�c\n�A���~�ʃ|�C���g��" + reward + "�l�������B";
                break;
            case 2:
                MSuccessText.text = "�~�b�V�������s�c\n�X�`�[���ʃ|�C���g��" + reward + "�l�������B";
                break;
            case 3:
                MSuccessText.text = "�~�b�V�������s�c\n�y�b�g�{�g���|�C���g��" + reward + "�l�������B";
                break;
            case 4:
                MSuccessText.text = "�~�b�V�������s�c\n�v���X�`�b�N�|�C���g��" + reward + "�l�������B";
                break;
            case 5:
                MSuccessText.text = "�~�b�V�������s�c\n���|�C���g��" + reward + "�l�������B";
                break;
            default:
                break;
        }

        Save(save);
    }



    //�I�[�v�j���O�A�`���[�g���A���̐i�s�Ǘ�
    [SerializeField] GameObject opWindow0; //���݂͂̂�OP�p�E�B���h�E
    [SerializeField] TextMeshProUGUI opText;
    int opSequence = 0; //�I�[�v�j���O�E�`���[�g���A���̐i�s�x

    void ClickCheck() //�N���b�N���ɌĂяo���B�I�[�v�j���O�̐i���ɉ����āA��ʃN���b�N�Ői�s���邩���Ǘ��B���쒆
    {
        switch(opSequence)
        {
            case 0:
                audioSource.PlayOneShot(openWindow); //���ʉ��Đ�
                opSequence = 1;
                break;
            case 1:
                audioSource.PlayOneShot(openWindow); //���ʉ��Đ�
                opSequence = 2;
                break;
            case 2:
                audioSource.PlayOneShot(openWindow); //���ʉ��Đ�
                Save(save);
                opSequence = 10;
                break;
            case 10:
                break;
            case 11:
                break;
            case 12:
                audioSource.PlayOneShot(openWindow); //���ʉ��Đ�
                Save(save);
                opSequence = 31; //��ŁA�ړ����}�Ӄ`���[�g���A���ɕύX
                break;
            case 31:
                audioSource.PlayOneShot(openWindow); //���ʉ��Đ�
                opSequence = 32;
                break;
            case 32:
                opSequence = 33;
                break;
            case 33:
                break;
            case 34:
                audioSource.PlayOneShot(openWindow); //���ʉ��Đ�
                opSequence = 35;
                break;
            case 35:
                audioSource.PlayOneShot(openWindow); //���ʉ��Đ�
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
                break;
        }
    }

    void ControlOP() //�I�[�v�j���O�E�`���[�g���A���̐i�s�Ǘ��B�ǉ����₷�����₷���̂��߂ɁA�p�[�g���Ƃ�10�̈ʂ�ύX����`�ɂ��Ă܂��B
    {
        switch(opSequence)
        {
            case 0: //�I�[�v�j���O
                canAll(false); //�S�{�^���̊J���֎~

                SetOPWindow0("�v���C���肪�Ƃ��������܂��B\n\n���Ȃ��͂��̃��T�C�N���V�e�B�̎s���ł��B\n\n�����ŏo���S�~���u���T�C�N���v���Ȃ���A\n���̒��𔭓W�����Ă����̂�\n���̃Q�[���̖ړI�ł��B");
                break;
            case 1:
                canAll(false);

                SetOPWindow0("�鏑�u�s���A�A�C���߂łƂ��������܂��I�v\n�鏑�u�S�~�������Ƃ��čė��p����u���T�C�N���v�B���T�C�N����ʂ��āA���̒����L���C�ɂ��Ă����܂��傤�I�v");
                break;
            case 2:
                canAll(false);

                SetOPWindow0("�鏑�u�܂��́A���Ȃ��̐g�̉��ɂ���A\n�u���T�C�N���}�[�N�v�̂����S�~��T���Ă݂Ă��������B�v");
                break;
            case 10: //�}�[�N���̓`���[�g���A��
                canAll(false);
                canInput = true; //���̓{�^�������g�p������
                opWindow0.SetActive(false);

                SetTutorial(250f, -350f, 0.5f, "���T�C�N���}�[�N��\n��������A\n�������N���b�N���悤�I�I");
                PutArrow(350f, -550f);
                break;
            case 11:
                canAll(false);

                SetTutorial(-100f, 750f, 0.7f, "�������}�[�N�̌�����͂��āA\n�u�����Ă��v�{�^�����������B");
                PutArrow(-100f, 510f);
                break;
            case 12:
                canAll(false);

                SetTutorial(0f, 200f, 1f, "�}�[�N����͂���ƁA\n���T�C�N���|�C���g���l���ł����B");
                PutArrow(0f, 550f, 135f);
                break;
            case 20: //�}�Ӄp�[�g�̃`���[�g���A����z��
                break;
            case 31: //���z�p�[�g�̑O�U��
                canAll(false);
                tutorialWindow.SetActive(false);
                arrow.SetActive(false);

                SetOPWindow0("�鏑�u���߂łƂ��������܂��I�I�v\n�鏑�u�����������T�C�N���ł����悤�ł��ˁI�v");
                break;
            case 32:
                canAll(false);

                SetOPWindow0("�鏑�u���͎������T�C�N���ł���S�~��\n�����Ă��܂����B�v\n�鏑�u���̕��̃|�C���g�������グ�܂��ˁB�v");
                break;
            case 33:
                canAll(false);

                alumiPoint += 15;
                stealPoint += 15;
                petPoint += 15;
                plaPoint += 15;
                paperPoint += 15;
                allPoint += 75;
                PointViewerChange(); //�|�C���g�\��UI�ɔ��f
                audioSource.PlayOneShot(inputEnter); //���ʉ��Đ�
                opSequence = 34;
                break;
            case 34:
                canAll(false);

                SetOPWindow0("�e�|�C���g��15���l�������I");

                break;
            case 35:
                canAll(false);

                SetOPWindow0("�鏑�u���́A�l�������|�C���g���g���āA\n���𔭓W�����Ă݂܂��傤�I�v");
                break;
            case 40: //���z�p�[�g�̃`���[�g���A��
                canAll(false);
                canBuild = true;
                opWindow0.SetActive(false);

                if(lv[0] == 0) //(�f�o�b�O�p)���Ɍ��z�ς݂̏ꍇ�̓`���[�g���A�����I������B
                {
                    SetTutorial(-200f, 0f, 0.5f, "�y�n���^�b�v����ƁA���z��ʂɐi�ނ�B");
                    PutArrow(-200f, 180f, 90f);
                }
                else
                {
                    opSequence = 42;
                }
                break;
            case 41:
                canAll(false);

                SetTutorial(-100f, 750f, 0.7f, "�u���T�C�N����v���u��y�{�݁v��\n���Ă����B\n�D���ȕ���I��ŁA\n�u�����Ă��v�{�^�����������I");
                PutArrow(-100f, 510f);
                break;
            case 42:
                canAll(false);
                tutorialWindow.SetActive(false);
                arrow.SetActive(false);

                SetOPWindow0("�鏑�u���܂����������܂����ˁI\n���������ƁA��葽���̃|�C���g���l���ł���悤�ɂȂ�܂��B�v");
                break;
            case 50: //�C�x���g
                canAll(false);

                SetOPWindow0("�鏑�u���ɂ��A���̒��ŃS�~���o�邱�Ƃ�����܂��B�v");
                break;
            case 51:
                canAll(false);
                canEvent = true;
                opWindow0.SetActive(false);

                if(alumiBook > 0) //���͍ς݂̃}�[�N�ɉ����āA�C�x���g���e������
                {
                    eventID = 1;
                }
                else if(stealBook > 0)
                {
                    eventID = 2;
                }
                else if(petBook > 0)
                {
                    eventID = 3;
                }
                else if(plaBook > 0)
                {
                    eventID = 4;
                }
                else if(paperBook > 0)
                {
                    eventID = 5;
                }
                OpenEvent();

                SetTutorial(0f, 700f, 0.5f, "���̂悤�ɁA�S�~�ɂ��������T�C�N�����@�𓚂���A\n�N�C�Y�C�x���g���������邱�Ƃ�����܂��B");
                PutArrow(0f, 500f, -45f);
                break;
            case 52:
                canAll(false);

                SetTutorial(200f, 700f, 0.5f, "������������Ȃ��Ƃ��́A\n���T�C�N���}�ӂ����Ă݂悤�I");
                PutArrow(400f, 500f, 0f);
                break;
            case 53:
                canAll(false);
                canBook = true;

                SetTutorial(-150f, -500f, 0.5f, "������������Ȃ��Ƃ��́A\n���T�C�N���}�ӂ����Ă݂悤�I");
                PutArrow(-300f, -700f, -90f);
                break;
            case 60: //�C�x���g���}�ӂ̃`���[�g���A����z��
                break;
            case 70: //�C�x���g�p�[�g�A�񓚕�
                break;
            default:
                //�E�B���h�E�����\����
                opWindow0.SetActive(false);
                tutorialWindow.SetActive(false);
                arrow.SetActive(false);

                canAll(true);
                break;
        }
    }

    void SetOPWindow0(string text) //�I�[�v�j���O�C�x���g�p�E�B���h�E�̕��͕ύX
    {
        opWindow0.SetActive(true);
        opText.text = text;
    }

    //�`���[�g���A���֘A
    [SerializeField] GameObject arrow; //�����p�̖��
    bool blinking = false; //�_�Œ��t���O
    int blinkInterval = 25; //�_�ŊԊu�̃t���[����
    int intervalCount = 25; //�o�߃t���[���̃J�E���g�p
    int blinkTimes = 3; //�_�ł����

    [SerializeField] GameObject tutorialWindow;
    [SerializeField] TextMeshProUGUI tutorialText;

    //�`���[�g���A�����̊e�{�^������\�t���O
    bool canInput = true;
    bool canBuild = true;
    bool canMission = true;
    bool canEvent = true;
    bool canBook = true;

    void canAll(bool i) //���̃t���O���ꊇ�ύX
    {
        canInput = i;
        canBuild = i;
        canMission = i;
        canEvent = i;
        canBook = i;
    }

    void SetTutorial(float x, float y, float Scale, string text) //�`���[�g���A���E�B���h�E�̔z�u
    {
        tutorialWindow.transform.localPosition = new Vector2(x, y); //�E�B���h�E�̈ʒu����
        tutorialWindow.transform.localScale = new Vector2(Scale, Scale);
        //tutorialWindow.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height); //�E�B���h�E�̃T�C�Y����
        tutorialText.text = text; //���b�Z�[�W�ύX

        tutorialWindow.SetActive(true);
    }

    void PutArrow(float x, float y, float rotation = 0f) //�_�ł��Ȃ����̔z�u
    {
        arrow.transform.localPosition = new Vector2(x, y);
        arrow.transform.localEulerAngles = new Vector3(0, 0, rotation);
        arrow.SetActive(true);
    }

    void SetArrow(float xPos, float yPos, float rotation = 0f, int interval = 50, int times = 3) //���_�ŊJ�n�B��󂪕K�v�Ȉʒu�����Ȃ��Ȃ�A���𕡐�����������y�����B
    {
        //���̈ʒu�A�_�ŊԊu�E�񐔂�ݒ�
        arrow.transform.localPosition = new Vector2(xPos, yPos);
        arrow.transform.localEulerAngles = new Vector3(0, 0, rotation);
        blinkInterval = interval / 2;
        intervalCount = interval / 2;
        blinkTimes = times;

        arrow.SetActive(true);
        blinking = true; //�_�Ńt���O��ON
    }

    public void Debug_CallArrow() //�_�ł̃f�o�b�O�{�^���p�B�{�����ł͏����Ă����͂��B
    {
        SetArrow(-50f, -650f);
    }

    //����ł́A�N�����̃Z�[�u�f�[�^�ǂݍ��݂ɂ̂ݎg�p
    private void Awake()
    {
        //�p�X���擾(Windows�̏ꍇ�A�uC:\Users\(���[�U�[��)\AppData\LocalLow\DefaultCompany\Sotsuken�v�ɕۑ������)
        filePath = Application.persistentDataPath + "/" + fileName;

        //�t�@�C���������ꍇ�̓t�@�C�����쐬
        if(!File.Exists(filePath))
        {
            Save(save);
        }

        //�t�@�C����ǂݍ����save�Ɋi�[
        save = Load1(filePath);
        //save�̓��e���e�ϐ��ɔ��f
        Load2();
    }

    // Start is called before the first frame update
    void Start()
    {
        //���t��ۑ�
        time = DateTime.Now;

        day = time.Day;
        month = time.Month;
        year = time.Year;

        //AudioSource���擾
        audioSource = GetComponent<AudioSource>();

        Debug.Log(year + "/" + month + "/" + day);
    }

    // Update is called once per frame
    void Update()
    {
        //���ݓ������擾�B�������d���ꍇ�A�ʂ̏ꏊ(��ʐ؂�ւ�����)�Ɉڂ�����
        time = DateTime.Now;

        //�t�F�[�h�C���E�A�E�g����
        if (isFadeIn == true)
        {
            FadeIn();
        }
        else if(isFadeOut == true)
        {
            FadeOut();
        }

        //�I�[�v�j���O�E�`���[�g���A������
        if(isFadeIn == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ClickCheck();
            }
            ControlOP();
        }
    }

    void FixedUpdate()
    {
        if(blinking == true)
        {
            //���̓_�ŏ���
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
