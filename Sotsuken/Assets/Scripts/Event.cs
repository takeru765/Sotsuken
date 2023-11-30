using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[SerializeField]
public class Event : ScriptableObject
{
    public int id;
    public string eventName;
    public bool isQuiz; //クイズ系イベントの場合はtrue
    public float bonus; //ボーナス系では1.2(倍)のように、クイズ系では200(ポイント)のように表記。
    public bool alumi; //クイズでは1つだけ選ぶのを想定
    public bool steal;
    public bool pet;
    public bool pla;
    public bool paper;
    public int ansewr; //クイズ系でのみ使用。正解の番号
    public string answer1; //クイズでのみ使用
    public string answer2; //クイズでのみ使用
    public string answer3; //クイズでのみ使用
    [Multiline(3)] public string intro;
    public Sprite eventImage;
}
