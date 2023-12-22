using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using System.IO;

public class Counter_test : MonoBehaviour
{
    //�Z�[�u�f�[�^�֘A
    [HideInInspector] public SaveData save; //�Z�[�u�f�[�^
    string filePath; //json(�Z�[�u�f�[�^)�t�@�C���̃p�X
    string fileName = "save.json"; //json�̃t�@�C����

    //����ł͋�̃Z�[�u������Ă�L�^���Ă邾���B�ǂ����Ŏg���ꍇ�͈ꐺ�����Ă��������B
    //�X�N���v�g�����琔�l���L�^�������ꍇ�́ALoad2�̋t�ɏ�����ǉ����ĉ������B
    void Save() 
    {
        //�f�[�^���̂̕ۑ�����
        string json = JsonUtility.ToJson(save); //SaveData��json�`���ɕϊ�
        StreamWriter wr = new StreamWriter(filePath, false); //json��filePath�̈ʒu�ɏ�������
        wr.WriteLine(json);
        wr.Close();
    }

    SaveData Load1(string path) //���[�h����1�B�Z�[�u�f�[�^���̂̓ǂݍ���
    {
        StreamReader rd = new StreamReader(path); //�t�@�C���p�X���w��
        string json = rd.ReadToEnd(); //�t�@�C���p�X�ɂ���json�t�@�C����ǂݍ���
        rd.Close();

        return JsonUtility.FromJson<SaveData>(json); //SaveData�ɕϊ����ĕԂ�
    }

    void Load2() //���[�h����2�B�ǂݍ��񂾃Z�[�u�f�[�^����A�X�N���v�g���̕ϐ��ɋL�^����B�K�v�ɉ����Ēǉ����Ă��������B
    {
        //���W�p�[�g�ŋL�^�����u���܂Ń}�[�N����͂������v��ǂݍ���
        alumi = save.alumiBook;
        steal = save.stealBook;
        pet = save.petBook;
        pla = save.plaBook;
        paper = save.paperBook;
    }

    public int alumi;//���ꂼ�ꂲ�݂���͂����񐔁B�Z�[�u�f�[�^����ǂݎ��
    public int steal;
    public int pet;
    public int pla;
    public int paper;


    public Trophy_Flag flag_script;

    //�V�[���N�����ɁAStart���O�ɏ��������炵���B
    private void Awake()
    {
        //�p�X���擾(Windows�̏ꍇ�A�uC:\Users\(���[�U�[��)\AppData\LocalLow\DefaultCompany\Sotsuken�v�ɕۑ������)
        filePath = Application.persistentDataPath + "/" + fileName;

        //�t�@�C���������ꍇ�̓t�@�C�����쐬
        if (!File.Exists(filePath))
        {
            Save();
        }

        //�t�@�C����ǂݍ����save�Ɋi�[
        save = Load1(filePath);
        //save�̓��e���e�ϐ��ɔ��f
        Load2();

        Debug.Log("�A���~�F" + alumi + "�A�X�`�[���F" + steal + "�A�y�b�g�{�g���F" + pet + "�A�v���X�`�b�N�F" + pla + "���F" + paper);
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
            Debug.Log("�[������");
        }
        else if (paper == 3)
        {
            Debug.Log("3�J�E���g�I");
        }*/


    }

}
