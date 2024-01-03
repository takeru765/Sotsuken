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

    //�Z�[�u�f�[�^�֘A
    [HideInInspector] public SaveData save; //�Z�[�u�f�[�^
    string filePath; //json(�Z�[�u�f�[�^)�t�@�C���̃p�X
    string fileName = "save.json"; //json�̃t�@�C����

    //�Z�[�u����
    void Save(SaveData data)
    {
        save.opSequece = opSequence;

        //save�ւ̏�񏑂�����
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

        opSequence = save.opSequece;//�`���[�g�������V�[���𒴂��ēǂݍ��߂�悤��

        //�e�퐔�l�̔��f
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

         //�e��UI�ɔ��f
         PointViewerChange();
         CheckBuildImage();*/
    }

    //�t�F�[�h�C���E�A�E�g�֘A
    float alfa = 1f; //�摜�̕s�����x�Ɏg�p
    [SerializeField] Image fade; //�t�F�[�h�C���E�A�E�g�p�̍��摜
    bool isFadeIn = true; //�t�F�[�h�C����
    bool isFadeOut = false; //�t�F�[�h�A�E�g��

    void FadeIn(float speed = 0.75f) //�t�F�[�h�C�������B�t�F�[�h���x���w��\
    {
        alfa -= speed * Time.deltaTime; //���Ԍo�߂ɉ����ĕs�����x��ቺ
        fade.color = new Color(0, 0, 0, alfa); //�s�����x�𔽉f

        if (alfa < 0)
        {
            alfa = 0;
            isFadeIn = false;

            fade.raycastTarget = false; //�t�F�[�h�摜�̃N���b�N����𖳌���(���ɂ���{�^�����N���b�N�����邽��)
        }
    }

    void FadeOut(float speed = 0.75f)  //�t�F�[�h�A�E�g�����B�t�F�[�h���x���w��\
    {
        fade.raycastTarget = true; //�t�F�[�h�摜�̃N���b�N�����L����(�e��{�^�����N���b�N�ł��Ȃ��悤�ɂ���)

        alfa += speed * Time.deltaTime; //���Ԍo�߂ɉ����ĕs�����x�𑝉�
        fade.color = new Color(0, 0, 0, alfa); //�s�����x�𔽉f

        if (alfa >= 1)
        {
            alfa = 1;
            isFadeOut = false;

            SceneManager.LoadScene("Oka_test");
        }
    }

    public void StartFadeOut() //�t�F�[�h�A�E�g�J�n
    {
        opSequence = 31;
        Save(save);
        isFadeOut = true;
    }

    //�I�[�v�j���O�A�`���[�g���A���̐i�s�Ǘ�
    [SerializeField] GameObject opWindow0; //���݂͂̂�OP�p�E�B���h�E
    [SerializeField] TextMeshProUGUI opText;

    [SerializeField] GameObject Button_Active; //���݂͂̂�OP�p�E�B���h�E

    int opSequence = 20; //�I�[�v�j���O�E�`���[�g���A���̐i�s�x(�{�X�N���v�g�͐}�Ӄp�[�g�̃f�o�b�O�p��20����X�^�[�g)

    void ClickCheck() //�N���b�N���ɌĂяo���B�I�[�v�j���O�̐i���ɉ����āA��ʃN���b�N�Ői�s���邩���Ǘ��B���쒆
    {
        switch (opSequence)
        {
            /*case 0:
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
                break;*/
        }
    }

    void ControlOP() //�I�[�v�j���O�E�`���[�g���A���̐i�s�Ǘ��B�ǉ����₷�����₷���̂��߂ɁA�p�[�g���Ƃ�10�̈ʂ�ύX����`�ɂ��Ă܂��B
    {
        
        switch (opSequence)
        {
            /*case 0: //�I�[�v�j���O
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
                break;*/

            case 20: //�}�Ӄp�[�g�̃`���[�g���A����z��//�����ΐ}�Ӄp�[�g��
                SetOPWindow0("�����͂�����p�[�g�ł�\n\n���܂܂ł��߂����T�C�N���}�[�N��\n\n�݂Ăׂ񂫂傤���邱�Ƃ��ł��܂�");
                //Debug.Log("�N���ς�");
                break;

            case 21:
                SetOPWindow0("�������T�C�N���}�[�N�̂��݂��킷��Ă��܂�����A\n\n���ł����̂�������Ђ炢��\n\n����΂��Ă��ڂ��܂��傤");
                break;
            case 22:
                SetOPWindow0("���߂��ɁA�A���~����̂�������Ђ炢�Ă݂܂��傤");
                break;
            case 23:
                opWindow0.SetActive(false);
                SetTutorial(80f, 260f, 1.0f, "�A���~����̃}�[�N���N���b�N����ƁA�����񂪂Ђ炫�܂�\n\n");
                PutArrow(-300f, 400f, -180f);
                break;

            case 24:
                //�}�ӂ̌����ߍ�Ƃɂ��Đ���
                SetTutorial(22f, -198f, 1.0f, "������̂����̓}�[�N�̂Ȃ܂��ƁA\n\n�}�[�N�ɂ�����S�~�̂���邢��������Ă��܂�");
                break;
            case 25:
                SetTutorial(16f, 604f, 1.0f, "�����ɂ͂�����̂Ȃ��悤���o���o���ɂȂ����p�Y��������܂�\n\n���Ă͂߂Ă���������񂹂������Ă����������܂��傤");
                break;
            case 26:
                SetOPWindow0("�����傤�ł�����ρ[�Ƃ̂��߂��������܂�");
                break;

            /*case 31: //���z�p�[�g�̑O�U��
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

                if (lv[0] == 0) //(�f�o�b�O�p)���Ɍ��z�ς݂̏ꍇ�̓`���[�g���A�����I������B
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

                if (alumiBook > 0) //���͍ς݂̃}�[�N�ɉ����āA�C�x���g���e������
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
                break;*/
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

    private void Awake()
    {
       //�p�X���擾(Windows�̏ꍇ�A�uC:\Users\(���[�U�[��)\AppData\LocalLow\DefaultCompany\Sotsuken�v�ɕۑ������)
        filePath = Application.persistentDataPath + "/" + fileName;

        //�t�@�C���������ꍇ�̓t�@�C�����쐬
        if (!File.Exists(filePath))
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
       
    }

    // Update is called once per frame
    void Update()
    {
        //�t�F�[�h�C���E�A�E�g�t���O��on�̎��A�t�F�[�h�������s��
        if (isFadeIn == true)
        {
            FadeIn();
        }
        else if (isFadeOut == true)
        {
            FadeOut();
        }

        if(isFadeIn == false) //�t�F�[�h�C�����̓N���b�N�Ń`���[�g���A����i�s�����Ȃ�
        {
            if (Input.GetMouseButtonDown(0))
            {
                ClickCheck();

            }
            ControlOP();
        }
    }

}
