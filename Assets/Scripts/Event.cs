using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// interface IEvent
// {
//     bool IsApplicable();
//     void Execute();
//     string title;
// }
[CreateAssetMenu(fileName = "New Event", menuName = "Event")]

public class Event : ScriptableObject

{
    [SerializeField] private string title = "Something happened.";
    [TextArea]
    [SerializeField] private string copy = "You won't believe what happened.";
    [SerializeField] private List<EventEffect> eventEffects = null;
    // [SerializeField] private List<Option> options = null;

    [SerializeField] private Event nextEvent;

    public Sprite illustration;


    [System.Serializable]
    public struct EventEffect {
        public Counters key;
        public int value;
    }

    public string Title => title;
    public string Copy => copy;
    public Event NextEvent => nextEvent;
    public List<EventEffect> EventEffects => eventEffects;


    // [System.Serializable]
    // public struct Option
    // {
    //     public string copy;
    //     public Event outcomeEvent;
    // }



    // public List<Option> Options => options;



    public string ApplyEffects()
    {
        string effectsSummary = "";

        foreach (EventEffect effect in eventEffects)
        {
            switch (effect.key)
            {
                case Counters.FOOD:
                    Game.Instance.food += effect.value;
                    break;
                case Counters.MORALE:
                    Game.Instance.morale += effect.value;
                    break;
                case Counters.DISTANCE:
                    Game.Instance.distance += effect.value;
                    break;
                case Counters.PENGUINS:
                    Game.Instance.penguins += effect.value;
                    break;
                default:
                    Debug.Log("No match found for event effect " + effect.key);
                    break;
            }
            effectsSummary += FormatEffectSummary(effect.key, effect.value);
            Debug.Log($"{effect.key} adjusted by {effect.value}");

        }

        return effectsSummary;
    }

    public string FormatEffectSummary(Counters key, int value)
    {
        // no need to mention unchanged values
        if (value == 0) return "";

        string effectSummary = (value > 0) ? "<color=#074905>" : "<color=\"red\">";
        
        effectSummary += key.ToString().ToLower();
        // set +/-
        if (value > 0) 
        {
            effectSummary += " +" + value;
        } else {
            effectSummary += " " + value;

        } 
        return effectSummary + "</color> ";
    }

}

// {
//     string title = "We trudge on...";
//     string copy;
    
//     void Execute() 
//     {
//         int miles = Random.Range(3,7);
//         int eaten = Game.Instance.penguins;
//         string preCopy = $"Our party of intrepid penguins, undaunted by the bitter cold and near hopelessness of the task ahead, trudge on, advancing {miles} miles and consuming {eaten} food.";
//         Game.Instance.food -= eaten;
//         Game.Instance.distance += miles;
//     }
    
// }
