using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    //�e��|�C���g
    int alumiPoint = 0; //�A���~��
    int stealPoint = 0; //�X�`�[����
    int petPoint = 0; //�y�b�g�{�g��
    int plaPoint = 0; //�v���X�`�b�N
    int paperPoint = 0; //�����e��
    int allPoint = 0; //�݌v�|�C���g

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
    [SerializeField] TextMeshProUGUI goalText;

    //�~�b�V�����̃}�[�N��ݒ肷��B
    public void SetMark(int m)
    {
        mark = m;
        Debug.Log("mark = " + mark);
    }

    //�~�b�V�����̖ڕW���𑝌�����
    public void AddReason(int i)
    {
        goal += i;

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
        if(goal != 10)
        {
            goalText.text = string.Format(" {0:G}", goal);
        }
        else
        {
            goalText.text = string.Format("{0:G}", goal);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
