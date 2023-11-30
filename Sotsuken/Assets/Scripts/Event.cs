using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[SerializeField]
public class Event : ScriptableObject
{
    public int id;
    public string eventName;
    public bool isQuiz; //�N�C�Y�n�C�x���g�̏ꍇ��true
    public float bonus; //�{�[�i�X�n�ł�1.2(�{)�̂悤�ɁA�N�C�Y�n�ł�200(�|�C���g)�̂悤�ɕ\�L�B
    public bool alumi; //�N�C�Y�ł�1�����I�Ԃ̂�z��
    public bool steal;
    public bool pet;
    public bool pla;
    public bool paper;
    public int ansewr; //�N�C�Y�n�ł̂ݎg�p�B�����̔ԍ�
    public string answer1; //�N�C�Y�ł̂ݎg�p
    public string answer2; //�N�C�Y�ł̂ݎg�p
    public string answer3; //�N�C�Y�ł̂ݎg�p
    [Multiline(3)] public string intro;
    public Sprite eventImage;
}
