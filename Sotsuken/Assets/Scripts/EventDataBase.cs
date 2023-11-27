using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[SerializeField]
public class EventDataBase : ScriptableObject
{
    public List<Event> eventList = new List<Event>();
}
