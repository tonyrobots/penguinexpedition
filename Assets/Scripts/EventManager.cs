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

    public Event FindEventByID(string id)
    {
        Event e = fixedEvents.Find(x => x.id.Equals(id));
        if (e == null) {
            Debug.LogError("no event was found with ID: " + id);
        }
        return e; 
    }


}
