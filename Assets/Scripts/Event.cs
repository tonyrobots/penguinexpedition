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
    [SerializeField] private List<Option> options = null;

    [SerializeField] private Event nextEvent;

    public string Title => title;
    public string Copy => copy;
    public Event NextEvent => nextEvent;

    [System.Serializable]
    public struct EventEffect {
        public string key;
        public int value;
    }

    [System.Serializable]
    public struct Option
    {
        public string copy;
        public Event outcomeEvent;
    }


    public List<EventEffect> EventEffects => eventEffects;

    public List<Option> Options => options;



    public void ApplyEffects()
    {
        foreach (EventEffect effect in eventEffects)
        {
            switch (effect.key)
            {
                case "FOOD":
                    Game.Instance.food += effect.value;
                    break;
                case "MORALE":
                    Game.Instance.morale += effect.value;
                    break;     
                default:
                    Debug.Log("No match found for event effect " + effect.key);
                    break;
            }
            Debug.Log($"{effect.key} adjusted by {effect.value}");

        }
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
