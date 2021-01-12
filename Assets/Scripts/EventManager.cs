using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{
    public List<Event> randomEvents;
    public List<Event> fixedEvents;

    public Event FindEventByName(string name)
    {
        return fixedEvents.Find(x => x.Title.Contains(name));
    }


}
