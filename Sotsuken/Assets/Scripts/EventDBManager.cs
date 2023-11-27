using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventDBManager : MonoBehaviour
{
    [SerializeField] EventDataBase eventDataBase;

    public void AddEventData(Event i)
    {
        eventDataBase.eventList.Add(i);
    }



    // Start is called before the first frame update
    void Start()
    {
        Event tmpEvent = ScriptableObject.CreateInstance("Event") as Event;
        tmpEvent = eventDataBase.eventList[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
