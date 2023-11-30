using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[SerializeField]
public class Event : ScriptableObject
{
    public int id;
    public string eventName;
    public bool isEvent;
    public float bonus; //ボーナス系では1.2(倍)のように、クイズ系では200(ポイント)のように表記。
    public bool alumi;
    public bool steal;
    public bool pet;
    public bool pla;
    public bool paper;
    public int ansewr; //クイズ系でのみ使用。
    [Multiline(3)] public string intro;
    public Sprite eventImage;
}
