using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Plastic_Button : MonoBehaviour
{
     public Counter_test TrashPoint;//���݂͉��̃X�N���v�g�BGamemaneger����ʂœo�^�����������p����
    public Sprite trashSprite;
    private Image image;

    int Btrash_pla;//���p�������l������ϐ�

    void Start()
    {
        Button btn = GetComponent<Button>();
        btn.interactable = false;//�{�^���@�\�𖳌��ɂ���

        image = GetComponent<Image>();

    }



    // Update is called once per frame
    void Update()
    {
        //Btrash = TrashPoint.trash;
        Btrash_pla = TrashPoint.pla;//���p�������l������ϐ�

        if (Btrash_pla == 4)//���̐��l�B�o�^�������݂��P�ȏ�ɂȂ����ꍇ�ɁA�}�ӂ̍��ڂ��o������悤�ɂȂ�B
        {
            Button btn = GetComponent<Button>();//�C���X�y�N�^�[�̃{�^���I�u�W�F�N�g�Ŏw�肵���{�^���̉���A���̂܂܂ł͂ق��̃I�u�W�F�N�g�������ɏ������Ă��܂��̂ŁA�����ƍׂ����w�肪�K�v�B
            btn.interactable = true;

            image.sprite = trashSprite;//�摜��ς���
        }

    }
}