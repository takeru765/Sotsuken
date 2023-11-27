using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[SerializeField]
public class Event : ScriptableObject
{
    public int id;
    public string eventName;
    public float bonus;
    public bool alumi;
    public bool steal;
    public bool pet;
    public bool pla;
    public bool paper;
    public string intro;
}
