using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{
    public List<Event> travelEvents;
    public List<Event> frolicEvents;
    public List<Event> fishingEvents;

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

    public Event GetRandomEvent(Activities activity)
    {
        Event currentEvent;

        // switch (activity)
        // {
        //     case Activities.SAFE_TRAVEL:
        //         currentEvent = travelEvents[Random.Range(0, travelEvents.Count)];
        //         break;
        //     case Activities.RISKY_TRAVEL:
        //         currentEvent = travelEvents[Random.Range(0, travelEvents.Count)];
        //         break;
        //     case Activities.FROLIC:
        //         currentEvent = frolicEvents[Random.Range(0, frolicEvents.Count)];
        //         break;
        //     case Activities.FISH:
        //         currentEvent = fishingEvents[Random.Range(0, fishingEvents.Count)];
        //         break;
        //     default:
        //         currentEvent = FindEventByID("travel1");
        //         break;
        // }

        switch (activity)
        {
            case Activities.SAFE_TRAVEL:
                if (Random.Range(0, 1f) < .3f)
                {
                    // 30% chance of random event
                    currentEvent = travelEvents[Random.Range(0,travelEvents.Count)];
                }
                else
                {
                    currentEvent = FindEventByID("travel1");
                }
                break;
            case Activities.RISKY_TRAVEL:
                if (Random.Range(0, 1f) < .6f)
                {
                    // 60% chance of random event
                    currentEvent = travelEvents[Random.Range(0, travelEvents.Count)];
                }
                else
                {
                    currentEvent = FindEventByID("travel1");
                }
                break;
            case Activities.FROLIC:
                if (Random.Range(0, 1f) < .2f)
                {
                    // 20% chance of random event
                    currentEvent = frolicEvents[Random.Range(0, frolicEvents.Count)];
                }
                else
                {
                    currentEvent = FindEventByID("frolic1");
                }
                break;
            case Activities.FISH:
                if (Random.Range(0, 1f) < .2f)
                {
                    // 20% chance of random event
                    currentEvent = fishingEvents[Random.Range(0, fishingEvents.Count)];
                }
                else
                {
                    currentEvent = FindEventByID("fish1");
                }
                break;
            default:
                currentEvent = FindEventByID("travel1");
                break;
        }
        return currentEvent;

    }


}
